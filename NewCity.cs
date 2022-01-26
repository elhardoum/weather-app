using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class NewCity : UserControl
    {
        public NewCity()
        {
            InitializeComponent();
        }

        private void NewCity_Load(object sender, EventArgs e)
        {
            search.Focus();
        }
    }
}
