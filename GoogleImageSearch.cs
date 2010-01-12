using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Sockets;

namespace ThinkMachine
{
    /// <summary>
    /// An image loader that uses Google image search.
    /// </summary>
    public class GoogleImageSearch : ImageSearch
    {
        public string Keywords
        {
            get
            {
                return this._Settings.Keywords;
            }
            set
            {
                SearchSettings ns = this._Settings;
                ns.Keywords = value;
                this.Settings = ns;
            }
        }

        /// <summary>
        /// Gets or sets the search settings for the image search.
        /// </summary>
        public SearchSettings Settings
        {
            get
            {
                return this._Settings;
            }
            set
            {
                lock (this)
                {
                    this._Settings = value;
                    this._Done = false;
                    this._CurrentLocation = 0;
                    this._ImageQueue = new Queue<ImageInfo>();
                }
            }
        }

        public Image Next()
        {
            Image im = null;
            while (im == null)
            {
                ImageInfo ne;
                lock (this)
                {
                    if (this._ImageQueue.Count == 0)
                    {
                        this._QueueImages();
                    }
                    if (this._Done)
                    {
                        break;
                    }
                    ne = this._ImageQueue.Dequeue();
                }
                try
                {
                    im = this._DownloadImage(ne.Url);
                }
                catch (Exception)
                {
                    im = null;
                }
            }
            return im;
        }

        /// <summary>
        /// Queues up some more images relating to the keywords.
        /// </summary>
        private void _QueueImages()
        {
            string url = this._CreateUrl(
                this._Settings.Keywords, 
                this._CurrentLocation, 
                this._Settings.Filter, 
                this._Settings.SafeSearch);
            IEnumerable<ImageInfo> newimages = this._MinePage(url);
            bool d = false;
            foreach (ImageInfo im in newimages)
            {
                d = true;
                this._CurrentLocation++;
                this._ImageQueue.Enqueue(im);
            }
            this._Done = !d;
        }

        /// <summary>
        /// Creates a url to retreive images from Google.
        /// </summary>
        /// <param name="Keywords">The keywords to search for.</param>
        /// <param name="Start">0-Indexed image to start with.</param>
        /// <param name="Filter">Should filter similar results?</param>
        /// <param name="SafeSearch">SafeSearch level 0(None) - 2(Strict).</param>
        /// <returns>The url where the image can be searched for.</returns>
        private string _CreateUrl(string Keywords, int Start, bool Filter, int SafeSearch)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://images.google.com/images?q=");
            sb.Append(HttpUtility.UrlEncode(Keywords.Replace(' ', '+')));

            // Start
            sb.Append("&start=");
            sb.Append(Start);

            // Filter
            sb.Append("&filter=");
            sb.Append(Filter ? '1' : '0');

            // SafeSearch
            sb.Append("&safe=");
            switch (SafeSearch)
            {
                case 1: sb.Append("moderate"); break;
                case 2: sb.Append("active"); break;
                default: sb.Append("off"); break;
            }

            // Return
            return sb.ToString();
        }

        /// <summary>
        /// Looks through Google image search for images.
        /// </summary>
        /// <param name="Url">The Google image result url.</param>
        /// <returns>A list of available images from the image results page.</returns>
        private IEnumerable<ImageInfo> _MinePage(string Url)
        {
            try
            {
                List<ImageInfo> info = new List<ImageInfo>();
                Stream response = WebHelper.Get(Url);
                string content = new StreamReader(response).ReadToEnd();
                Regex rg = new Regex(@"imgurl\\x3d(?<imgurl>.*?)\\x26imgrefurl\\x3d(?<imgrefurl>.*?)\\x26");
                foreach (Match m in rg.Matches(content))
                {
                    info.Add(new ImageInfo()
                    {
                        Url = m.Groups["imgurl"].Value,
                        Source = m.Groups["imgrefurl"].Value
                    });
                }
                return info;
            }
            catch (Exception)
            {
                return new List<ImageInfo>();
            }
        }

        /// <summary>
        /// Downloads an image from the specified url.
        /// </summary>
        /// <param name="Url">The url to download from.</param>
        /// <returns>The newly downloaded image</returns>
        private Image _DownloadImage(string Url)
        {
            Stream rs = WebHelper.Get(Url);
            return Image.FromStream(rs);
        }

        private SearchSettings _Settings;
        private int _CurrentLocation;
        private Queue<ImageInfo> _ImageQueue;
        private bool _Done;

        /// <summary>
        /// Information about a single image.
        /// </summary>
        public struct ImageInfo
        {
            /// <summary>
            ///  Url of the image.
            /// </summary>
            public string Url;

            /// <summary>
            /// Url of the source of the image.
            /// </summary>
            public string Source;
        }

        /// <summary>
        /// Enumeration of search settings that can be for image searching.
        /// </summary>
        public struct SearchSettings
        {
            public string Keywords;
            public bool Filter;
            public int SafeSearch;
        }
    }
}
