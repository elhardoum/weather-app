﻿
namespace WeatherApp
{
    partial class ResultItem
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
            this.label = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(-3, 8);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(59, 25);
            this.label.TabIndex = 0;
            this.label.Text = "label1";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(518, 3);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(116, 34);
            this.button.TabIndex = 1;
            this.button.Text = "SELECT";
            this.button.UseVisualStyleBackColor = true;
            // 
            // ResultItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button);
            this.Controls.Add(this.label);
            this.Name = "ResultItem";
            this.Size = new System.Drawing.Size(637, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button;
    }
}
