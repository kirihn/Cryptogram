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
            var uri = new Uri("../Themes/DarkTheme.xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            //SetLang("../Themes/LightTheme.xaml");
        }

        public void SetLang(string TypeTheme)
        {

            var uri = new Uri(TypeTheme, UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // добавляем загруженный словарь ресурсов
            for (int i = 0; i < Application.Current.Resources.MergedDictionaries.Count; i++)
            {
                if (Application.Current.Resources.MergedDictionaries[i].Contains("Theme_MainColor"))
                {
                    Application.Current.Resources.MergedDictionaries[i] = resourceDict;
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
