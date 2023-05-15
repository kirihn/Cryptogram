using Cryptogram.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.ViewModel
{
    internal class MySettingsVM
    {
        public string MyId { get; set; }
        public MySettings Owner;
        public MySettingsVM(MySettings owner, int myId)
        {
            MyId = myId.ToString();
            Owner = owner;

        }
    }
}
