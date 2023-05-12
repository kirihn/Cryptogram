using Cryptogram.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cryptogram.UI
{
    internal class ExitCommand
    {
        static ExitCommand()
        {
            Exit = new RoutedCommand("Exit", typeof(Registration));
        }
        public static RoutedCommand Exit { get; set; }
        
    }
}
