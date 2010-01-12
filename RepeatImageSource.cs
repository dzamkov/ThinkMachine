using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ThinkMachine
{
    /// <summary>
    /// Image source that uses another source of images but prevents underruns by
    /// reusing previous images.
    /// </summary>
    public class RepeatImageSource : ImageSource
    {
        public RepeatImageSource()
        {
            this._Images = new List<_ImageInfo>();
            this._Reserve = 10;
            this._Frame = 0;
        }

        public Image Next()
        {
            lock(this)
            {
                Image next = null;
                if (this._Source != null)
                {
                    next = this._Source.Next();
                }

                if (next == null)
                {
                    this._Resort();
                    if (this._Images.Count > 0)
                    {
                        _ImageInfo info = this._Images[0];
                        info.Usages++;
                        info.LastSeen = this._Frame;
                        next = info.Image;
                    }
                }
                else
                {
                    _ImageInfo ninfo = new _ImageInfo();
                    ninfo.Image = next;
                    ninfo.Usages = 1;
                    ninfo.LastSeen = this._Frame;
                    this._Images.Add(ninfo);
                }
                this._Frame++;
                return next;
            }
        }

        /// <summary>
        /// Gets or sets the source
        /// </summary>
        public ImageSource Source
        {
            get
            {
                lock (this)
                {
                    return this._Source;
                }
            }
            set
            {
                lock (this)
                {
                    this._Source = value;
                }
            }
        }

        /// <summary>
        /// Amount of images held to be used during underruns.
        /// </summary>
        public int Reserve
        {
            get
            {
                lock (this)
                {
                    return this._Reserve;
                }
            }
            set
            {
                lock (this)
                {
                    this._Reserve = value;
                }
            }
        }

        /// <summary>
        /// Sorts the previously seen images by usage.
        /// </summary>
        private void _Resort()
        {
            // Sort by usages
            this._Images.Sort(
                new Comparison<_ImageInfo>(
                    delegate(_ImageInfo A, _ImageInfo B)
                    {
                        if (A.Usages == B.Usages)
                        {
                            if (A.LastSeen == B.LastSeen)
                            {
                                return 0;
                            }
                            else
                            {
                                if (A.LastSeen < B.LastSeen)
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 1;
                                }
                            }
                        }
                        else
                        {
                            if (A.Usages > B.Usages)
                            {
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }));

            // Remove extra
            int diff = this._Images.Count - this._Reserve;
            if (diff > 0)
            {
                for (int t = 0; t < diff; t++)
                {
                    this._Images.RemoveAt(this._Images.Count - 1);
                }
            }
        }

        /// <summary>
        /// Struct for image information.
        /// </summary>
        private class _ImageInfo
        {
            public Image Image;
            public int Usages;
            public int LastSeen; // Frame where this image was last seen.
        }

        private List<_ImageInfo> _Images;
        private ImageSource _Source;
        private int _Reserve;
        private int _Frame; // Images given.
    }
}
