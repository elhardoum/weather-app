
namespace WeatherApp
{
    partial class Forecast
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.location = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.PictureBox();
            this.desc = new System.Windows.Forms.Label();
            this.variants = new System.Windows.Forms.Label();
            this.hourlylabel = new System.Windows.Forms.Label();
            this.hourlypanel = new System.Windows.Forms.Panel();
            this.dailypanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // location
            // 
            this.location.AutoSize = true;
            this.location.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.location.Location = new System.Drawing.Point(40, 31);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(158, 38);
            this.location.TabIndex = 0;
            this.location.Text = "Loading...";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.date.Location = new System.Drawing.Point(40, 71);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(104, 25);
            this.date.TabIndex = 1;
            this.date.Text = "Loading...";
            // 
            // icon
            // 
            this.icon.Image = global::WeatherApp.Resources.placeholder;
            this.icon.Location = new System.Drawing.Point(771, 0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(150, 150);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.icon.TabIndex = 2;
            this.icon.TabStop = false;
            // 
            // desc
            // 
            this.desc.AutoSize = true;
            this.desc.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.desc.Location = new System.Drawing.Point(804, 112);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(82, 20);
            this.desc.TabIndex = 3;
            this.desc.Text = "Loading...";
            // 
            // variants
            // 
            this.variants.AutoSize = true;
            this.variants.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.variants.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.variants.Location = new System.Drawing.Point(43, 101);
            this.variants.Name = "variants";
            this.variants.Size = new System.Drawing.Size(72, 18);
            this.variants.TabIndex = 4;
            this.variants.Text = "Loading...";
            // 
            // hourlylabel
            // 
            this.hourlylabel.AutoSize = true;
            this.hourlylabel.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.hourlylabel.Location = new System.Drawing.Point(40, 235);
            this.hourlylabel.Name = "hourlylabel";
            this.hourlylabel.Size = new System.Drawing.Size(61, 22);
            this.hourlylabel.TabIndex = 5;
            this.hourlylabel.Text = "Today";
            // 
            // hourlypanel
            // 
            this.hourlypanel.AutoScroll = true;
            this.hourlypanel.Location = new System.Drawing.Point(40, 272);
            this.hourlypanel.Name = "hourlypanel";
            this.hourlypanel.Size = new System.Drawing.Size(663, 197);
            this.hourlypanel.TabIndex = 7;
            // 
            // dailypanel
            // 
            this.dailypanel.AutoScroll = true;
            this.dailypanel.Location = new System.Drawing.Point(41, 522);
            this.dailypanel.Name = "dailypanel";
            this.dailypanel.Size = new System.Drawing.Size(663, 197);
            this.dailypanel.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(41, 485);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Next 7 days";
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.White;
            this.delete.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.delete.ForeColor = System.Drawing.Color.Red;
            this.delete.Location = new System.Drawing.Point(933, 685);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(92, 34);
            this.delete.TabIndex = 10;
            this.delete.Text = "REMOVE";
            this.delete.UseVisualStyleBackColor = false;
            // 
            // Forecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.delete);
            this.Controls.Add(this.dailypanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hourlypanel);
            this.Controls.Add(this.hourlylabel);
            this.Controls.Add(this.variants);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.date);
            this.Controls.Add(this.location);
            this.Name = "Forecast";
            this.Size = new System.Drawing.Size(1034, 739);
            this.Load += new System.EventHandler(this.Forecast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label location;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label desc;
        private System.Windows.Forms.Label variants;
        private System.Windows.Forms.Label hourlylabel;
        private System.Windows.Forms.Panel hourlypanel;
        private System.Windows.Forms.Panel dailypanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delete;
    }
}
