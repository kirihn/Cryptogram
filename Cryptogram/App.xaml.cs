using Cryptogram.Localization;
using Cryptogram.Themes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cryptogram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            LocalizationManager.CreateInstance(this);
            ThemeManager.CreateInstance(this);
            
        }
    }
}
