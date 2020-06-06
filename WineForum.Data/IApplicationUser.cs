using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineForum.Data.Models;

namespace WineForum.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task SetProfileImage(string id, Uri uri);
        Task UpdateUserRating(string id, Type type);
    }
}
