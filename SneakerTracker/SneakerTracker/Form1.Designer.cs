﻿namespace SneakerTracker
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SKU = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SKU.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(155, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SKU
            // 
            this.SKU.Controls.Add(this.textBox1);
            this.SKU.Location = new System.Drawing.Point(12, 12);
            this.SKU.Name = "SKU";
            this.SKU.Size = new System.Drawing.Size(167, 48);
            this.SKU.TabIndex = 1;
            this.SKU.TabStop = false;
            this.SKU.Text = "Search";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 66);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(370, 446);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(388, 66);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(480, 446);
            this.textBox2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 524);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.SKU);
            this.Name = "Form1";
            this.Text = "Sneaker Tracker";
            this.SKU.ResumeLayout(false);
            this.SKU.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox SKU;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
