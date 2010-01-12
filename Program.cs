using System;
using System.Collections.Generic;

using System.Windows.Forms;

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

            GoogleImageSearch.SearchSettings settings = new GoogleImageSearch.SearchSettings();
            settings.Keywords = Config.Default.Query;
            settings.Filter = Config.Default.Filter;
            settings.SafeSearch = Config.Default.SafeSearch;

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
                            Config.Default.Query = settings.Keywords;
                            Config.Default.Filter = settings.Filter;
                            Config.Default.SafeSearch = settings.SafeSearch;
                            Config.Default.Save();
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
