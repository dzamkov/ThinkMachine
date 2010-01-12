using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace ThinkMachine
{
    /// <summary>
    /// A streaming source for images.
    /// </summary>
    public interface ImageSource
    {
        /// <summary>
        /// Gets the next image in the source. This method is thread-safe.
        /// </summary>
        /// <returns>A new image or null if all images are expended.</returns>
        Image Next();
    }

    /// <summary>
    /// A source for images based on keywords.
    /// </summary>
    public interface ImageSearch : ImageSource
    {
        /// <summary>
        /// Gets or sets the keywords to search for. After setting this, the images obtained
        /// with next will relate to the keywords.
        /// </summary>
        string Keywords { get; set; }

    }
}
