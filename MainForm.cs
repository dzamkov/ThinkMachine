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
            this._ImageStream = new GoogleImageLoader();
            this._ImageStream.LoadKeywords(new string[] { "computer" });
            this._MaxImageQueueSize = 100;
            this._ImageQueue = new LinkedList<Image>();

            this._QueueImage();

            Timer mtimer = new Timer();
            mtimer.Interval = 200;
            mtimer.Tick += new EventHandler(delegate
            {
                lock (this._ImageQueue)
                {
                    if (this._ImageQueue.Count > 0)
                    {
                        Image next = this._ImageQueue.Last.Value;
                        this._ImageQueue.RemoveLast();
                        this.ShownImage = next;
                        if (this._ImageQueue.Count < this._MaxImageQueueSize)
                        {
                            this._ImageQueue.AddFirst(next);
                        }
                    }
                }
            });
            mtimer.Start();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate
            {
                while(true) {
                    bool nc = false;
                    lock (this._ImageQueue)
                    {
                        if (this._ImageQueue.Count < this._MaxImageQueueSize)
                        {
                            nc = true;
                        }
                    }
                    if (nc)
                    {
                        this._QueueImage();
                    }
                }
            });
            bw.RunWorkerAsync();
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

        /// <summary>
        /// Takes an image from the image stream and puts it on the
        /// image queue.
        /// </summary>
        private void _QueueImage()
        {
            Image next = this._ImageStream.Next();
            lock(this._ImageQueue)
            {
                this._ImageQueue.AddLast(next);
            }
        }

        private LinkedList<Image> _ImageQueue;
        private int _MaxImageQueueSize;
        private ImageLoader _ImageStream;
    }
}
