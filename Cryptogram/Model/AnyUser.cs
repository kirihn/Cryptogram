using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cryptogram.Model
{
    public class AnyUser
    {
        public int Id { get; set; }
        public string Name { get; set; } //
        public string Lastname { get; set; } //
        public string Status { get; set; }
        public string Email { get; set; } //
        public byte[] AvatarImage { get; set; }
        public string Login { get; set; } //
        public string Password { get; set; } //
        public long EncryptKey { get; set; }//

        public string UserName { get; set; }
        public AnyUser()
        {
            AvatarImage = new byte[0];
            Name = "";
            Lastname = "";
            Status = "";
            Email = "";
            Login = "";
            Password = "";
            UserName = "";

        }
        public AnyUser(string name, string lastname, string status,string email, byte[] avatarimage, string login, string password, string usernamee)
        {
            Name = name;
            Lastname = lastname;
            Status = status;
            Email = email;
            AvatarImage = avatarimage;
            Login = login;
            Password = password;
            UserName = usernamee;
        }
        

    }
}