using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThinkMachine
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this._GoogleImageSearch = new GoogleImageSearch();
            this._GoogleImageSearch.Keywords = new string[] { "computer" };

            this._AsyncImageSource = new AsyncImageSource();
            this._AsyncImageSource.Source = this._GoogleImageSearch;
            this._AsyncImageSource.ThreadAmount = 3;

            Timer mtimer = new Timer();
            mtimer.Interval = 200;
            mtimer.Tick += new EventHandler(delegate
            {
                Image next = this._AsyncImageSource.Next();
                if (next != null)
                {
                    this.ShownImage = next;
                }
            });
            mtimer.Start();
        }

        /// <summary>
        /// The image currently shown by the form.
        /// </summary>
        public Image ShownImage
        {
            get
            {
                return this.ImageContainer.BackgroundImage;
            }
            set
            {
                this.ImageContainer.BackgroundImage = value;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Escape key exit
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private GoogleImageSearch _GoogleImageSearch;
        private AsyncImageSource _AsyncImageSource;
    }
}
