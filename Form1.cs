using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System.Net.Http;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        private List<City> cities;
        private City activeCity;
        private bool addNew = false;
        private DB db = new DB();
        private static readonly int WEATHER_TTL = 60 * 10; // 10 minutes
        public Form1()
        {
            InitializeComponent();

            // init from db
            cities = db.LoadSaved();

            setContentPanel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            navpanel.AutoScroll = false;
            navpanel.HorizontalScroll.Enabled = false;
            navpanel.HorizontalScroll.Visible = false;
            navpanel.HorizontalScroll.Maximum = 0;
            navpanel.AutoScroll = true;

            setupNav();
        }

        private void setupNav()
        {
            var logo = getNavLabel("Weather App", true);
            navpanel.Controls.Add(logo);

            foreach ( var city in cities )
            {
                addNavPanel(city);
            }

            addNewCityNavPanel();
        }

        private NavPanel getNavLabel( string text, bool logo=false )
        {
            var label = new Label();
            label.Visible = true;
            label.Text = text;
            label.Font = new Font("Microsoft JhengHei UI", logo ? 12f : 8.5f, true ? FontStyle.Bold : FontStyle.Regular);
            label.Width = navpanel.Width;
            label.Name = "label";

            NavPanel panel = new NavPanel();
            panel.Height = 60;
            panel.Location = new Point(0, navpanel.Controls.Count * panel.Height);
            panel.Width = navpanel.Width;

            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(" " + label.Text + " ", label.Font, navpanel.Width);
                label.Height = (int) Math.Ceiling(size.Height);
                label.Width = (int) Math.Ceiling(size.Width);
                label.Text = text;
            }

            label.Location = new Point((panel.Width - label.Width) / 2, (panel.Height - label.Height) / 2);

            panel.Controls.Add(label);

            if ( ! logo)
            {
                panel.Cursor = System.Windows.Forms.Cursors.Hand;
            }

            return panel;
        }

        private void addNavPanel(City city)
        {
            var panel = getNavLabel(city.Name);
            var click = new EventHandler((s, e) =>
            {
                activeCity = city;
                addNew = false;
                setContentPanel();
            });

            panel.Click += click;
            panel.Controls["label"].Click += click;
            panel.Name = "city-" + city.Id.ToString();

            navpanel.Controls.Add(panel);
        }

        private void addNewCityNavPanel()
        {
            var add = getNavLabel("+ Add location");
            var click2 = new EventHandler((s, e) =>
            {
                activeCity = null;
                addNew = true;
                setContentPanel();
            });
            add.Click += click2;
            add.Controls["label"].Click += click2;
            add.Name = "addloc";
            navpanel.Controls.Add(add);
        }

        private void setContentPanel()
        {
            highlightMenuItem();
            contentpanel.Controls.Clear();

            if ( null != activeCity )
            { // city forecast
                var forecast = new Forecast();
                forecast.Location = new Point(0, 0);
                forecast.Size = contentpanel.Size;
                forecast.Controls["location"].Text = activeCity.Name.ToUpper();

                forecast.Load += new EventHandler(async (s, e) =>
                {
                    var cityData = db.GetCityById(activeCity.Id);

                    if ( null == cityData )
                    {
                        MessageBox.Show("Error occurred, could not load city data.");
                        return;
                    }

                    activeCity = cityData;

                    if ( (DateTime.Now - activeCity.Updated).TotalSeconds > WEATHER_TTL )
                    {
                        string apiUrl = $"https://api.openweathermap.org/data/2.5/onecall?lat={activeCity.Lat}&lon={activeCity.Lon}&exclude=minutely&appid={Resources.apikey}";

                        bool done = await Task.Run(async () =>
                        {
                            string raw = await getUrl(apiUrl);
                            dynamic data;

                            try
                            {
                                data = JsonConvert.DeserializeObject(raw);
                            } catch (Exception)
                            {
                                return false;
                            }

                            try
                            {
                                var _ = data.daily;
                            } catch (Exception e)
                            {
                                return false;
                            }

                            if (null == activeCity)
                                return false;

                            activeCity.Weather = data;
                            activeCity.Updated = DateTime.Now;

                            return db.UpdateFavorite(activeCity);
                        });

                        if ( done )
                        {
                            loadCityWeatherUI(forecast);
                        }
                        else
                        {
                            MessageBox.Show("API call error.");
                        }
                    }
                    else
                    {
                        loadCityWeatherUI(forecast);
                    }
                });

                forecast.Controls["delete"].Click += new EventHandler((s, e) =>
                {
                    if ( db.DeleteCityFromFavorites(activeCity) )
                    {
                        navpanel.Controls.Clear();

                        cities.RemoveAll(x => x.Id == activeCity.Id);

                        activeCity = null;
                        addNew = false;

                        setupNav();
                        setContentPanel();
                    } else
                    {
                        MessageBox.Show("Error occurred, please try again.");
                    }
                });

                contentpanel.Controls.Add(forecast);
            } else if ( addNew )
            { // add new form
                var cityform = new NewCity();
                cityform.Location = new Point(0, 0);
                cityform.Size = contentpanel.Size;
                cityform.BackColor = Color.White;
                contentpanel.Controls.Add(cityform);

                cityform.Controls["search"].KeyDown += new KeyEventHandler((s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        var keyword = cityform.Controls["search"].Text.Trim();

                        if (keyword.Length < 3)
                        {
                            MessageBox.Show("Please enter a search keyword.");
                            return;
                        }

                        cityform.Controls["resultscont"].Controls.Clear();

                        var results = db.Search(keyword);

                        if ( results.Count > 0 )
                        {
                            int i = -1;
                            foreach ( var city in results )
                            {
                                var resultitem = new ResultItem();
                                resultitem.Controls["label"].Text = city.Name + ", " + city.Country;
                                resultitem.Location = new Point(
                                    cityform.Location.X,
                                    cityform.Location.Y + 35 * ++i
                                );

                                resultitem.Controls["button"].Enabled = -1 == cities.FindIndex(x => x.Id == city.Id);

                                resultitem.Controls["button"].Click += new EventHandler((s, e) =>
                                {
                                    if (db.AddCityToFavorites(city))
                                    {
                                        activeCity = city;
                                        cities.Add(city);
                                        navpanel.Controls.Remove(navpanel.Controls["addloc"]);
                                        addNavPanel(city);
                                        addNewCityNavPanel();
                                        setContentPanel();
                                    } else
                                    {
                                        MessageBox.Show("Error occurred, please try again.");
                                    }
                                });

                                cityform.Controls["resultscont"].Controls.Add(resultitem);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No cities matched.");
                        }
                    }
                });
            }
            else
            { // nothing selected
                var notice = new UserControl();
                notice.Location = new Point(0, 0);
                notice.Size = contentpanel.Size;
                notice.BackColor = Color.White;

                var label = new Label();
                label.Visible = true;
                label.Text = "  Welcome to Weather App! Select or add a new location.  ";
                label.Font = new Font("Microsoft JhengHei UI", 8f, FontStyle.Regular);
                label.ForeColor = Color.FromArgb(85, 85, 85);

                using (Graphics g = CreateGraphics()) {
                    SizeF size = g.MeasureString(label.Text, label.Font, 495);
                    label.Location = new Point(
                        notice.Width/2 - (int) Math.Ceiling(size.Width/2),
                        notice.Height/2 - (int) Math.Ceiling(size.Height/2)
                    );
                    label.Text = label.Text;
                    label.Width = (int) size.Width;
                    label.Height = (int) size.Height;
                }

                notice.Controls.Add(label);

                contentpanel.Controls.Add(notice);
            }
        }

        private void highlightMenuItem()
        {
            foreach ( Control c in navpanel.Controls )
            {
                bool active = false;
                c.BackColor = Color.Transparent;
                c.Controls["label"].ForeColor = Color.Black;

                if ( null != activeCity )
                {
                    active = c.Name == $"city-{activeCity.Id}";
                } else if ( addNew )
                {
                    active = c.Name == "addloc";
                }

                if (active)
                {
                    c.BackColor = Color.DarkGray;
                    c.Controls["label"].ForeColor = Color.White;
                }
            }
        }

        private void loadCityWeatherUI(Forecast control)
        {
            var icon = (PictureBox)control.Controls["icon"];
            var desc = (Label) control.Controls["desc"];
            var variants = (Label) control.Controls["variants"];

            control.Controls["date"].Text = DateTime.Now.DayOfWeek.ToString();

            icon.ImageLocation = $"http://openweathermap.org/img/wn/{activeCity.Weather.current.weather[0].icon}@2x.png";

            desc.Text = activeCity.Weather.current.weather[0].description;

            using(Graphics g = CreateGraphics()) {
                SizeF size = g.MeasureString(desc.Text, desc.Font, 495);
                desc.Location = new Point(icon.Location.X + icon.Width/2 - (int) Math.Ceiling(size.Width/2), desc.Location.Y);
                desc.Text = desc.Text;
            }

            variants.Text = String.Format("Temp: {0} °C\nHumidity: {1} %\nWind: {2} m/s",
                Math.Round(decimal.Parse(activeCity.Weather.current.temp.ToString()) - 273.15M, 1),
                activeCity.Weather.current.humidity.ToString(),
                activeCity.Weather.current.wind_speed.ToString());

            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(variants.Text, variants.Font, 495);
                //variants.Location = new Point(control.Width / 2 - (int)Math.Ceiling(size.Width / 2), variants.Location.Y);
                variants.Text = variants.Text;
                variants.Height = (int)Math.Ceiling(size.Height);
            }

            control.Controls["hourlypanel"].Height = 250;
            control.Controls["hourlypanel"].Width = control.Width - control.Location.X - 10;

            foreach (var obj in activeCity.Weather.hourly)
            {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(double.Parse(obj.dt.ToString()))
                    .AddSeconds( double.Parse(activeCity.Weather.timezone_offset.ToString()) );

                var now = DateTime.UtcNow.AddSeconds(double.Parse(activeCity.Weather.timezone_offset.ToString()));

                // only show today's hours
                if (date.ToString("d") != now.ToString("d"))
                    break;

                // skip past hours if cache outdated
                if (date.Hour < now.Hour )
                    continue;
                
                var unit = new WeatherUnit();
                unit.Location = new Point(0, 0);
                unit.Size = contentpanel.Size;

                var uheader = (Label) unit.Controls["header"];
                var udesc = (Label) unit.Controls["desc"];
                var uicon = (PictureBox) unit.Controls["icon"];

                uheader.Text = string.Format("{0:hh tt}", date);
                udesc.Text = String.Format("{0}°c", Math.Round(decimal.Parse(obj.temp.ToString()) - 273.15M, 1));

                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(uheader.Text, uheader.Font, 495);
                    uheader.Location = new Point(uicon.Width / 2 - (int)Math.Ceiling(size.Width/2), uheader.Location.Y+10);
                    uheader.Text = uheader.Text;
                }

                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(udesc.Text, udesc.Font, 495);
                    udesc.Location = new Point(uicon.Width / 2 - (int)Math.Ceiling(size.Width / 2), udesc.Location.Y);
                    udesc.Text = udesc.Text;
                }

                uicon.Image = null;
                uicon.ImageLocation = $"http://openweathermap.org/img/wn/{obj.weather[0].icon}@2x.png";
                unit.Width = 120;
                unit.Height = 200;
                unit.Location = new Point(control.Controls["hourlypanel"].Controls.Count * unit.Width, 0);

                control.Controls["hourlypanel"].Controls.Add(unit);

                if (control.Controls["hourlypanel"].Controls.Count >= 7) // keep 7 units tops
                    break;
            }

            control.Controls["dailypanel"].Height = 250;
            control.Controls["dailypanel"].Width = control.Width - control.Location.X - 10;

            foreach (var obj in activeCity.Weather.daily)
            {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(double.Parse(obj.dt.ToString()))
                    .AddSeconds( double.Parse(activeCity.Weather.timezone_offset.ToString()) );

                var now = DateTime.UtcNow.AddSeconds(double.Parse(activeCity.Weather.timezone_offset.ToString()));

                // reset to midnight for comparison
                date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

                // skip past dates, including today, if cache outdated
                if (date <= now)
                    continue;

                var unit = new WeatherUnit();
                unit.Location = new Point(0, 0);
                unit.Size = contentpanel.Size;

                var uheader = (Label) unit.Controls["header"];
                var udesc = (Label) unit.Controls["desc"];
                var uicon = (PictureBox) unit.Controls["icon"];

                uheader.Text = date.ToString("ddd").ToUpper();
                udesc.Text = String.Format("{0}/{1} °C",
                    Math.Round(decimal.Parse(obj.temp.max.ToString()) - 273.15M, 1),
                    Math.Round(decimal.Parse(obj.temp.min.ToString()) - 273.15M, 1));

                udesc.Font = new Font("Microsoft JhengHei UI", 5.5f, FontStyle.Regular);

                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(uheader.Text, uheader.Font, 495);
                    uheader.Location = new Point(uicon.Width / 2 - (int)Math.Ceiling(size.Width/2), uheader.Location.Y+10);
                    uheader.Text = uheader.Text;
                }

                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(udesc.Text, udesc.Font, 495);
                    udesc.Location = new Point(uicon.Width / 2 - (int)Math.Ceiling(size.Width / 2), udesc.Location.Y);
                    udesc.Text = udesc.Text;
                }

                uicon.Image = null;
                uicon.ImageLocation = $"http://openweathermap.org/img/wn/{obj.weather[0].icon}@2x.png";
                unit.Width = 120;
                unit.Height = 200;
                unit.Location = new Point(control.Controls["dailypanel"].Controls.Count * unit.Width, 0);

                control.Controls["dailypanel"].Controls.Add(unit);

                if (control.Controls["dailypanel"].Controls.Count >= 7) // keep 7 days tops
                    break;
            }
        }

        private async Task<string> getUrl(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine("Util.JsonRequest error: {0}, {1}", e.ToString(), e.Message);
                return null;
            }
        }
    }

    public class City
    {
        public int Id;
        public string Name;
        public string Country;
        public double Lat;
        public double Lon;
        public dynamic Weather;
        public DateTime Updated;
    }

    public class DB
    {
        private readonly static string DB_FILE = "WeatherApp.db";
        private readonly static string DB_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
          + @"\" + DB_FILE;
        private readonly static string CONN_STR = $"Data Source={DB_PATH};";

        public DB()
        {
            if (File.Exists(DB_PATH))
                return;

            StreamWriter writer = new StreamWriter(DB_PATH);
            writer.BaseStream.Write(Resources.weather, 0, Resources.weather.Length);
            writer.Close();
            writer.Dispose();

            // init mycities table
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = @"create table if not exists mycities (
                id bigint unsigned primary key,
                name varchar(150) not null,
                countrycode varchar(2) not null,
                lat decimal not null,
                lon decimal not null,
                weather text,
                updated datetime
            )";
            var cmd = new SqliteCommand(stm, con);
            cmd.ExecuteReader();
        }

        public List<City> Search(string input)
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = "SELECT * FROM cities where name like @search order by name, countrycode desc limit 40";
            var cmd = new SqliteCommand(stm, con);
            cmd.Parameters.AddWithValue("@search", $"%{input}%");
            SqliteDataReader reader = cmd.ExecuteReader();

            var items = new List<City>();

            while (reader.Read())
            {
                items.Add(new City
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["name"].ToString(),
                    Country = reader["countrycode"].ToString(),
                    Lat = double.Parse(reader["lat"].ToString()),
                    Lon = double.Parse(reader["lon"].ToString()),
                });
            }

            return items;
        }

        public bool AddCityToFavorites(City city)
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = "insert into mycities (Id, name, countrycode, lat, lon, updated) values (@id, @name, @country, @lat, @lon, @updated)";
            var cmd = new SqliteCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", city.Id);
            cmd.Parameters.AddWithValue("@name", city.Name);
            cmd.Parameters.AddWithValue("@country", city.Country);
            cmd.Parameters.AddWithValue("@lat", city.Lat);
            cmd.Parameters.AddWithValue("@lon", city.Lon);
            cmd.Parameters.AddWithValue("@updated", city.Updated);
            
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            } catch(Exception)
            {
                return false;
            }
        }

        public List<City> LoadSaved()
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = "SELECT * FROM mycities order by name, countrycode";
            var cmd = new SqliteCommand(stm, con);
            SqliteDataReader reader = cmd.ExecuteReader();

            var items = new List<City>();

            while (reader.Read())
            {
                items.Add(new City
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["name"].ToString(),
                    Country = reader["countrycode"].ToString(),
                    Lat = double.Parse(reader["lat"].ToString()),
                    Lon = double.Parse(reader["lon"].ToString()),
                    Weather = JsonConvert.DeserializeObject(reader["weather"].ToString()),
                    Updated = DateTime.Parse(reader["updated"].ToString()),
                });
            }

            return items;
        }

        public City GetCityById(int Id)
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = "SELECT * FROM mycities where id = @Id limit 1";
            var cmd = new SqliteCommand(stm, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqliteDataReader reader = cmd.ExecuteReader();

            var items = new List<City>();

            while (reader.Read())
            {
                items.Add(new City
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["name"].ToString(),
                    Country = reader["countrycode"].ToString(),
                    Lat = double.Parse(reader["lat"].ToString()),
                    Lon = double.Parse(reader["lon"].ToString()),
                    Weather = JsonConvert.DeserializeObject(reader["weather"].ToString()),
                    Updated = DateTime.Parse(reader["updated"].ToString()),
            });
            }

            return items.Count > 0 ? items.First() : null;
        }

        public bool UpdateFavorite(City city)
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = @"update mycities set weather = @weather, updated = @updated where id = @Id";
            var cmd = new SqliteCommand(stm, con);
            cmd.Parameters.AddWithValue("@Id", city.Id);
            cmd.Parameters.AddWithValue("@weather", JsonConvert.SerializeObject(city.Weather));
            cmd.Parameters.AddWithValue("@updated", city.Updated);
            
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            } catch(Exception)
            {
                return false;
            }
        }

        public bool DeleteCityFromFavorites(City city)
        {
            var con = new SqliteConnection(CONN_STR);
            con.Open();
            string stm = @"delete from mycities where id = @Id";
            var cmd = new SqliteCommand(stm, con);
            cmd.Parameters.AddWithValue("@Id", city.Id);

            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    public class NavPanel : Panel
    {
        public Color borderColor = Color.DarkGray;
        public int borderWidth = 10;
        public NavPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Pen p = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, ClientSize.Height, ClientSize.Width, borderWidth));
            }
        }
    }
}
