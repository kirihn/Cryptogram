using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.Model
{
    public class Friend
    {
        public int FriendId { get; set; }
        public string FName { get; set; }
        public string FLasName { get; set; }
        public string FStatus { get; set; }
        public string FUserName { get; set; }
        public long FEncryptKey { get; set; }
        public byte[] FAvatarImage { get; set; }

    }
}
