using Mvc.OefenfirmaCMS.Lib.Data;
using Mvc.OefenfirmaCMS.Lib.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace Mvc.OefenfirmaCMS.Lib.Repositories
{
    /// <summary>
    /// Repository klasse die communicatie met databron regelt. 
    /// Merk op! -> In dit voorbeeld zit de data in het geheugen (geen db nodig)
    /// </summary>
    public class UsersRepository
    {
        private OefenfirmaContext context;

        public UsersRepository (OefenfirmaContext c)
	    {
            this.context = c;
	    }

        public ICollection<User> GetAllUsersWithRoles() 
        {
            return context.Users
                .Include("Roles")
                .OrderBy(u => u.UserName)
                //.ThenBy(u => u.Voornaam)
                .ToList();
        }

        public User GetUserByUsernameAndPassword(string username, string password) 
        {
            //wachtwoord omzetten naar md5
            string hashedpass = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            User user = context.Users
                .Include("Roles")
                .Where(u => u.UserName.ToUpper() == username.ToUpper() &&
                            u.PasswordHash == hashedpass)
                .FirstOrDefault();
            return user;
        }

    }
}
