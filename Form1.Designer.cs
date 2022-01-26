
namespace WeatherApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.navpanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contentpanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // navpanel
            // 
            this.navpanel.AutoScroll = true;
            this.navpanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.navpanel.Location = new System.Drawing.Point(0, 0);
            this.navpanel.Name = "navpanel";
            this.navpanel.Size = new System.Drawing.Size(205, 729);
            this.navpanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(205, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(6, 729);
            this.panel1.TabIndex = 2;
            // 
            // contentpanel
            // 
            this.contentpanel.AutoScroll = true;
            this.contentpanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.contentpanel.Location = new System.Drawing.Point(211, 0);
            this.contentpanel.Name = "contentpanel";
            this.contentpanel.Size = new System.Drawing.Size(1035, 729);
            this.contentpanel.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 728);
            this.Controls.Add(this.contentpanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.navpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Weather App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel navpanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel contentpanel;
    }
}

