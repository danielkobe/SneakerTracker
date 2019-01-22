namespace SneakerTracker
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SKU = new System.Windows.Forms.GroupBox();
            this.ResultsListView = new System.Windows.Forms.ListView();
            this.PricesGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResellCheckBox = new System.Windows.Forms.CheckBox();
            this.UndercutModeCheckBox = new System.Windows.Forms.CheckBox();
            this.NumberOfResultsComboBox = new System.Windows.Forms.ComboBox();
            this.NumberOfResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.SKU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricesGridView)).BeginInit();
            this.NumberOfResultsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(6, 19);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(155, 20);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // SKU
            // 
            this.SKU.Controls.Add(this.SearchTextBox);
            this.SKU.Location = new System.Drawing.Point(12, 12);
            this.SKU.Name = "SKU";
            this.SKU.Size = new System.Drawing.Size(167, 48);
            this.SKU.TabIndex = 1;
            this.SKU.TabStop = false;
            this.SKU.Text = "Search";
            // 
            // ResultsListView
            // 
            this.ResultsListView.Location = new System.Drawing.Point(12, 66);
            this.ResultsListView.MultiSelect = false;
            this.ResultsListView.Name = "ResultsListView";
            this.ResultsListView.Size = new System.Drawing.Size(370, 446);
            this.ResultsListView.TabIndex = 10;
            this.ResultsListView.UseCompatibleStateImageBehavior = false;
            ////////////////////////////this.ResultsListView.SelectedIndexChanged += new System.EventHandler(this.Select);
            // 
            // PricesGridView
            // 
            this.PricesGridView.AllowUserToAddRows = false;
            this.PricesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PricesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.PricesGridView.Location = new System.Drawing.Point(388, 66);
            this.PricesGridView.Name = "PricesGridView";
            this.PricesGridView.RowHeadersWidth = 60;
            this.PricesGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PricesGridView.Size = new System.Drawing.Size(462, 446);
            this.PricesGridView.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle5.Format = "n2";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.HeaderText = "Goat";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle6.Format = "n2";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.HeaderText = "StockX";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle7.Format = "n2";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn3.HeaderText = "Flight";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle8.Format = "n2";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.HeaderText = "Stadium";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // ResellCheckBox
            // 
            this.ResellCheckBox.AutoSize = true;
            this.ResellCheckBox.Location = new System.Drawing.Point(388, 33);
            this.ResellCheckBox.Name = "ResellCheckBox";
            this.ResellCheckBox.Size = new System.Drawing.Size(74, 17);
            this.ResellCheckBox.TabIndex = 12;
            this.ResellCheckBox.Text = "After Fees";
            this.ResellCheckBox.UseVisualStyleBackColor = true;
            ////////////////////this.ResellCheckBox.CheckedChanged += new System.EventHandler(this.resellCheckBox_CheckedChanged);
            // 
            // UndercutModeCheckBox
            // 
            this.UndercutModeCheckBox.AutoSize = true;
            this.UndercutModeCheckBox.Location = new System.Drawing.Point(468, 34);
            this.UndercutModeCheckBox.Name = "UndercutModeCheckBox";
            this.UndercutModeCheckBox.Size = new System.Drawing.Size(100, 17);
            this.UndercutModeCheckBox.TabIndex = 13;
            this.UndercutModeCheckBox.Text = "Undercut Mode";
            this.UndercutModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // NumberOfResultsComboBox
            // 
            this.NumberOfResultsComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.NumberOfResultsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumberOfResultsComboBox.FormattingEnabled = true;
            this.NumberOfResultsComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.NumberOfResultsComboBox.Location = new System.Drawing.Point(6, 17);
            this.NumberOfResultsComboBox.Name = "NumberOfResultsComboBox";
            this.NumberOfResultsComboBox.Size = new System.Drawing.Size(99, 21);
            this.NumberOfResultsComboBox.TabIndex = 14;
            this.NumberOfResultsComboBox.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // NumberOfResultsGroupBox
            // 
            this.NumberOfResultsGroupBox.Controls.Add(this.NumberOfResultsComboBox);
            this.NumberOfResultsGroupBox.Location = new System.Drawing.Point(185, 12);
            this.NumberOfResultsGroupBox.Name = "NumberOfResultsGroupBox";
            this.NumberOfResultsGroupBox.Size = new System.Drawing.Size(111, 48);
            this.NumberOfResultsGroupBox.TabIndex = 15;
            this.NumberOfResultsGroupBox.TabStop = false;
            this.NumberOfResultsGroupBox.Text = "Number of Results";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 519);
            this.Controls.Add(this.NumberOfResultsGroupBox);
            this.Controls.Add(this.UndercutModeCheckBox);
            this.Controls.Add(this.ResellCheckBox);
            this.Controls.Add(this.PricesGridView);
            this.Controls.Add(this.ResultsListView);
            this.Controls.Add(this.SKU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::SneakerTracker.Properties.Resources.AppIcon;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sneaker Tracker";
            this.SKU.ResumeLayout(false);
            this.SKU.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricesGridView)).EndInit();
            this.NumberOfResultsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.GroupBox SKU;
        private System.Windows.Forms.ListView ResultsListView;
        private System.Windows.Forms.DataGridView PricesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.CheckBox ResellCheckBox;
        private System.Windows.Forms.CheckBox UndercutModeCheckBox;
        private System.Windows.Forms.ComboBox NumberOfResultsComboBox;
        private System.Windows.Forms.GroupBox NumberOfResultsGroupBox;
    }
}

