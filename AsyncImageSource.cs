using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Threading;

namespace ThinkMachine
{
    /// <summary>
    /// An image source that takes images from another source using multiple threads
    /// and queues them in this image source. If the next method returns null, either the
    /// source is out of images or the queue is expended.
    /// </summary>
    public class AsyncImageSource : ImageSource
    {
        public AsyncImageSource()
        {
            this._Queue = new Queue<Image>();
            this._Threads = new List<Thread>();
            this._MaxQueueSize = 20;
        }

        public Image Next()
        {
            lock (this)
            {
                if (this._Queue.Count > 0)
                {
                    return this._Queue.Dequeue();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the source to originally pull images from. This does
        /// not remove any images from the current image queue.
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
        /// Gets or sets the amount of threads used. The image source will not work with less than 1.
        /// </summary>
        public int ThreadAmount
        {
            get
            {
                lock (this)
                {
                    return this._Threads.Count;
                }
            }
            set
            {
                lock (this)
                {
                    if (this._Threads.Count > value)
                    {
                        int diff = this._Threads.Count - value;
                        for (int t = 0; t < diff; t++)
                        {
                            this._Threads.RemoveAt(0);
                        }
                    }
                    if (this._Threads.Count < value)
                    {
                        int diff = value - this._Threads.Count;
                        for (int t = 0; t < diff; t++)
                        {
                            Thread newthread = new Thread(new ThreadStart(this._DoWork));
                            this._Threads.Add(newthread);
                            newthread.IsBackground = true;
                            newthread.Start();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum size of the image queue.
        /// </summary>
        public int MaxQueueSize
        {
            get
            {
                lock (this)
                {
                    return this._MaxQueueSize;
                }
            }
            set
            {
                lock (this)
                {
                    this._MaxQueueSize = value;
                }
            }
        }

        /// <summary>
        /// Gets the current size of the image queue.
        /// </summary>
        public int QueueSize
        {
            get
            {
                lock (this)
                {
                    return this._Queue.Count;
                }
            }
        }

        /// <summary>
        /// Entry-point of additional threads.
        /// </summary>
        private void _DoWork()
        {
            Thread current = Thread.CurrentThread;
            ImageSource source = null;
            Image leftover = null;
            while (true)
            {
                Image next = null;
                if (leftover == null)
                {
                    if (source != null)
                    {
                        next = source.Next();
                    }
                }
                else
                {
                    next = leftover;
                    leftover = null;
                }

                lock (this)
                {
                    source = this._Source;
                    if (this._Queue.Count < this._MaxQueueSize)
                    {
                        this._Queue.Enqueue(next);
                    }
                    else
                    {
                        leftover = next;
                    }
                    if (!this._Threads.Contains(current))
                    {
                        // Thread removed
                        break;
                    }
                }
            }
        }

        private ImageSource _Source;
        private Queue<Image> _Queue;
        private List<Thread> _Threads;
        private int _MaxQueueSize;
        private int _QueueSize;
    }
}
