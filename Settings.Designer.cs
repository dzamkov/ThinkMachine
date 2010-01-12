namespace ThinkMachine
{
    partial class Settings
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
            this.Layout = new System.Windows.Forms.TableLayoutPanel();
            this.Filter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Query = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SafeSearch = new System.Windows.Forms.ComboBox();
            this.Layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // Layout
            // 
            this.Layout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.Layout.ColumnCount = 2;
            this.Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Layout.Controls.Add(this.Filter, 0, 1);
            this.Layout.Controls.Add(this.label1, 0, 0);
            this.Layout.Controls.Add(this.Query, 1, 0);
            this.Layout.Controls.Add(this.OKButton, 1, 2);
            this.Layout.Controls.Add(this.ExitButton, 0, 2);
            this.Layout.Controls.Add(this.SafeSearch, 1, 1);
            this.Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layout.Location = new System.Drawing.Point(0, 0);
            this.Layout.Name = "Layout";
            this.Layout.RowCount = 3;
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.Layout.Size = new System.Drawing.Size(441, 87);
            this.Layout.TabIndex = 0;
            // 
            // Filter
            // 
            this.Filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Filter.FormattingEnabled = true;
            this.Filter.Items.AddRange(new object[] {
            "Do not filter",
            "Filter similar images"});
            this.Filter.Location = new System.Drawing.Point(4, 32);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(213, 21);
            this.Filter.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query";
            // 
            // Query
            // 
            this.Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Query.Location = new System.Drawing.Point(224, 4);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(213, 20);
            this.Query.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OKButton.Location = new System.Drawing.Point(224, 60);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(213, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExitButton.Location = new System.Drawing.Point(4, 60);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(213, 23);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // SafeSearch
            // 
            this.SafeSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SafeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SafeSearch.FormattingEnabled = true;
            this.SafeSearch.Items.AddRange(new object[] {
            "SafeSearch Off",
            "SafeSearch Moderate",
            "SafeSearch Strict"});
            this.SafeSearch.Location = new System.Drawing.Point(224, 32);
            this.SafeSearch.Name = "SafeSearch";
            this.SafeSearch.Size = new System.Drawing.Size(213, 21);
            this.SafeSearch.TabIndex = 6;
            // 
            // Settings
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ExitButton;
            this.ClientSize = new System.Drawing.Size(441, 87);
            this.Controls.Add(this.Layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Settings";
            this.Text = "Search Settings";
            this.Layout.ResumeLayout(false);
            this.Layout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Layout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Query;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox Filter;
        private System.Windows.Forms.ComboBox SafeSearch;
    }
}