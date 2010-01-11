using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ThinkMachine
{
    /// <summary>
    /// A source for images based on keywords.
    /// </summary>
    public abstract class ImageLoader
    {
        /// <summary>
        /// Gets the next image that relates to the keywords.
        /// </summary>
        /// <returns>A new image or null if all images for the keywords
        /// are expended.</returns>
        public abstract Image Next();

        /// <summary>
        /// Loads the keywords to use for image search from now on.
        /// </summary>
        /// <param name="Keywords">The keywords to use.</param>
        public abstract void LoadKeywords(IEnumerable<string> Keywords);
    }
}
