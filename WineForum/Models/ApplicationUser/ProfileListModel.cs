using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineForum.Models.ApplicationUser
{
    public class ProfileListModel
    {
        public IEnumerable<ProfileModel> Profiles { get; set; }
    }
}
