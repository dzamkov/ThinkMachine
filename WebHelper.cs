using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ThinkMachine
{
    /// <summary>
    /// Helper for downloading and http.
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// Gets an http read stream for the page at the specified url.
        /// </summary>
        /// <param name="Url">Location of the page or content.</param>
        /// <returns>A newly created read-only stream to read the page contents.</returns>
        public static Stream Get(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.16) Gecko/2009120208 Firefox/3.0.16 (.NET CLR 3.5.30729)";
            request.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
            request.Referer = Url;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }
    }
}
