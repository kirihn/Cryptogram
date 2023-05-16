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
        public string FLastName { get; set; }
        public string FStatus { get; set; }
        public string FUserName { get; set; }
        public string FEmail { get; set; }
        public long FEncryptKey { get; set; }
        public byte[] FAvatarImage { get; set; }

        public Friend (int friendId, string fName, string fLastName, string fStatus, string fUserName, string fEmail, long fEncryptKey, byte[] fAvatarImage)
        {
            FriendId = friendId;
            FName = fName;
            FLastName = fLastName;
            FStatus = fStatus;
            FUserName = fUserName;
            FEmail = fEmail;
            FEncryptKey = fEncryptKey;
            FAvatarImage = fAvatarImage;
        }
    }
}
