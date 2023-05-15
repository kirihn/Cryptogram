using Cryptogram.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cryptogram.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyHelp.xaml
    /// </summary>
    public partial class MyHelp : Page
    {
        int MyId { get; set; }
        public MyHelp(int myId)
        {
            InitializeComponent();
            MyId = myId;
            DataContext = new MyHelpVM(this, MyId);
        }
    }
}
