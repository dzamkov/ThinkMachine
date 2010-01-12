using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ThinkMachine
{
    public partial class MainForm : Form
    {
        public MainForm(bool ScreenSaver, GoogleImageSearch.SearchSettings Settings)
        {
            InitializeComponent();
            this._GoogleImageSearch = new GoogleImageSearch();
            this._GoogleImageSearch.Settings = Settings;

            this._AsyncImageSource = new AsyncImageSource();
            this._AsyncImageSource.Source = this._GoogleImageSearch;
            this._AsyncImageSource.MaxQueueSize = 5;
            this._AsyncImageSource.ThreadAmount = 3;

            RepeatImageSource ris = new RepeatImageSource();
            this._FinalImageSource = ris;
            ris.Source = this._AsyncImageSource;
            ris.Reserve = 40;

            Cursor.Hide();

            Timer mtimer = new Timer();
            mtimer.Interval = 250;
            mtimer.Tick += new EventHandler(delegate
            {
                Image next = this._FinalImageSource.Next();
                if (next != null)
                {
                    this.ShownImage = next;
                }
            });
            this._ScreenSaver = ScreenSaver;
            this._Start = DateTime.Now;
            mtimer.Start();
        }

        /// <summary>
        /// The image currently shown by the form.
        /// </summary>
        public Image ShownImage
        {
            get
            {
                return this._ShownImage;
            }
            set
            {
                this._ShownImage = value;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(Brushes.Black, e.ClipRectangle);
            
            if (this._ShownImage != null)
            {
                int padding = 20;

                Rectangle screenrect = e.ClipRectangle;
                Rectangle imagerect = new Rectangle(0, 0, this._ShownImage.Width, this._ShownImage.Height);
                screenrect = new Rectangle(screenrect.X + padding, screenrect.Y + padding,
                    screenrect.Width - (2 * padding), screenrect.Height - (2 * padding));
                if (imagerect.Width * screenrect.Height > screenrect.Width * imagerect.Height)
                {
                    imagerect.Height = screenrect.Width * imagerect.Height / imagerect.Width;
                    imagerect.Width = screenrect.Width;
                    imagerect.X = screenrect.X;
                    imagerect.Y = screenrect.Y + (screenrect.Height / 2) - (imagerect.Height / 2);
                }
                else
                {
                    imagerect.Width = screenrect.Height * imagerect.Width / imagerect.Height;
                    imagerect.Height = screenrect.Height;
                    imagerect.X = screenrect.X + (screenrect.Width / 2) - (imagerect.Width / 2);
                    imagerect.Y = screenrect.Y;
                }

                e.Graphics.DrawImage(this._ShownImage, imagerect);
            }

#if DEBUG
            // Debug information
            Font f = new Font(FontFamily.GenericMonospace, 12);
            e.Graphics.DrawString(
                "Downloaded Images: " + this._GoogleImageSearch.DownloadedImages, 
                f, Brushes.White, new Point(0, 0));
#endif
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Escape key exit
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._ScreenSaver && (DateTime.Now - this._Start) > TimeSpan.FromMilliseconds(500))
            {
                this.Close();
            }
        }

        private Image _ShownImage;
        private ImageSource _FinalImageSource;
        private GoogleImageSearch _GoogleImageSearch;
        private AsyncImageSource _AsyncImageSource;
        private bool _ScreenSaver;
        private DateTime _Start;
    }
}
