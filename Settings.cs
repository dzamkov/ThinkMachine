using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ThinkMachine
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the search settings.
        /// </summary>
        public GoogleImageSearch.SearchSettings SearchSettings
        {
            get
            {
                GoogleImageSearch.SearchSettings ss = new GoogleImageSearch.SearchSettings();
                ss.Keywords = this.Query.Text;
                ss.SafeSearch = this.SafeSearch.SelectedIndex;
                ss.Filter = this.Filter.SelectedIndex == 0 ? false : true;
                return ss;
            }
            set
            {
                this.Query.Text = value.Keywords;
                this.Filter.SelectedIndex = value.Filter ? 1 : 0;
                this.SafeSearch.SelectedIndex = value.SafeSearch;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
