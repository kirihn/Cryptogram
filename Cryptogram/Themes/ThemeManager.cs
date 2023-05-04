using Cryptogram.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace Cryptogram.Themes
{
    public class ThemeManager
    {
        private static object _temp0;
        private static object _temp1;

        public static ThemeManager? Instance { get; private set; }

        private Application _app;

        static ThemeManager()
        {
            _temp0 = new object();
            _temp1 = new object();

            Instance = null;
        }

        public ThemeManager(Application app)
        {
            _app = app;

            foreach (var item in typeof(DarkTheme).GetProperties())
            {
                if (item.Name.StartsWith("Theme"))
                {
                    _app.Resources.Add(item.Name, "INIT");
                }
            }


            SetLang("Dark");
        }

        public void SetLang(string TypeTheme)
        {
            PropertyInfo[] properties;

            switch (TypeTheme)
            {
                case "Dark":
                    properties = typeof(DarkTheme).GetProperties();
                    break;
                case "Light":
                    properties = typeof(LightTheme).GetProperties();
                    break;
                default:
                    MessageBox.Show("Ошибка смены цвета в менеджере");
                    properties = typeof(DarkTheme).GetProperties();
                    break;
            }

            foreach (var item in properties)
            {
                if (item.Name.StartsWith("Theme"))
                {
                    _app.Resources[item.Name] = (string)item.GetValue(typeof(DarkTheme));
                }
            }
        }

        public static void CreateInstance(Application app)
        {
            lock (_temp0)
            {
                if (Instance == null)
                {
                    lock (_temp1)
                    {
                        Instance = new ThemeManager(app);
                    }
                }
            }
        }
    }
}
