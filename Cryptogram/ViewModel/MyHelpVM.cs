using Cryptogram.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.ViewModel
{
    internal class MyHelpVM
    {
        public string MyId { get; set; }
        public MyHelp Owner;
        public MyHelpVM(MyHelp owner, int myId)
        {
            MyId = myId.ToString();
            Owner = owner;

        }
    }
}
