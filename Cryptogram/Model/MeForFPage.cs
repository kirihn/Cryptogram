using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.Model
{
    public class MeForFPage
    {
        public int MyId;
        public List<Friend> Friends;

        public MeForFPage()
        {
            Friends = new List<Friend>();
        }

        public void AddNewFriend(Friend f)
        {
            Friends.Add(f);
        }
    }
}
