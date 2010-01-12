using System;
using System.Collections.Generic;

using System.Windows.Forms;

using Microsoft.Win32;

namespace ThinkMachine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Settings se;

            // Configuration information
            string key = "Software\\ThinkMachine\\Search";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(key, true);
            GoogleImageSearch.SearchSettings settings = new GoogleImageSearch.SearchSettings();
            if(rk != null)
            {
                try
                {
                    settings.Keywords = (string)rk.GetValue("Keywords");
                    settings.Filter = (int)rk.GetValue("Filter") == 0 ? false : true;
                    settings.SafeSearch = (int)rk.GetValue("SafeSearch");
                }
                catch (InvalidCastException)
                {
                }
            }

            // Screensaver arguments
            if (Args.Length >= 1)
            {
                switch (Args[0].ToLower().Trim().Substring(0, 2))
                {
                    case "/c":
                        // Screensaver configuration
                        se = new Settings();
                        se.SearchSettings = settings;
                        if (se.ShowDialog() == DialogResult.OK)
                        {
                            settings = se.SearchSettings;
                            if (rk == null)
                            {
                                rk = Registry.CurrentUser.CreateSubKey(key);
                            }

                            rk.SetValue("Keywords", settings.Keywords);
                            rk.SetValue("Filter", (int)(settings.Filter ? 1 : 0));
                            rk.SetValue("SafeSearch", settings.SafeSearch);
                        }
                        return;

                    case "/p":
                        // Screensaver preview

                        return;

                    case "/s":
                        // Fullscreen mode
                        Application.Run(new MainForm(true, settings));
                        return;

                }

            }

            se = new Settings();
            se.SearchSettings = settings;
            if (se.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm(false, se.SearchSettings));
            }
        }
    }
}
