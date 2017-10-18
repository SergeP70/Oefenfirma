using Mvc.OefenfirmaCMS.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/* Our view model contains a property that stores a User, 
 * a property that stores the Role(s) associated to the stored User as a List<int> of Role's Id, 
 * and finally a property that store all available JobTag as a IEnumerable fo SelectListItem ( to bind to a ListBox ) */
namespace Mvc.Oefenfirma.Web.ViewModels
{
    public class UserRolesVM
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> AllUserRoles { get; set; }

        private List<int> _selectedUserRoles;
        public List<int> SelectedUserRoles
        {
            get
            {
                if (_selectedUserRoles == null)
                {
                    _selectedUserRoles = User.Roles.Select(m => m.RoleId).ToList();
                }
                return _selectedUserRoles;
            }
            set { _selectedUserRoles = value; }
        }
    }
}


