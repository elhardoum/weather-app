
namespace WeatherApp
{
    partial class WeatherUnit
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
            this.header = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.PictureBox();
            this.desc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.header.Location = new System.Drawing.Point(0, 5);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(50, 20);
            this.header.TabIndex = 0;
            this.header.Text = "label1";
            // 
            // icon
            // 
            this.icon.Image = global::WeatherApp.Resources.placeholder;
            this.icon.Location = new System.Drawing.Point(0, 28);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(75, 75);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.icon.TabIndex = 1;
            this.icon.TabStop = false;
            // 
            // desc
            // 
            this.desc.AutoSize = true;
            this.desc.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.desc.Location = new System.Drawing.Point(0, 96);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(50, 20);
            this.desc.TabIndex = 2;
            this.desc.Text = "label1";
            // 
            // WeatherUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.desc);
            this.Controls.Add(this.header);
            this.Controls.Add(this.icon);
            this.Name = "WeatherUnit";
            this.Size = new System.Drawing.Size(150, 173);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label desc;
    }
}
