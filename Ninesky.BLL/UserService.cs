using Ninesky.DAL.Repositories;
using Ninesky.IBLL;
using Ninesky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ninesky.BLL
{
    public class UserService:BaseService<User>,IUserService
    {
        public UserService() : base(RepositoryFactory.UserRepository) { }

        public bool Exist(string userName) { return CurrentRepository.Exists(u => u.UserName == userName); }

        public User Find(int userID) { return CurrentRepository.Find(u => u.UserID == userID); }

        public User Find(string userName) { return CurrentRepository.Find(u => u.UserName == userName); }

        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            bool _isAsc = true;
            string _orderName = string.Empty;
            switch (order)
            {
                case 0:
                    _isAsc = true;
                    _orderName = "UserID";
                    break;
                case 1:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
                case 2:
                    _isAsc = true;
                    _orderName = "RegistrationTime";
                    break;
                case 3:
                    _isAsc = false;
                    _orderName = "RegistrationTime";
                    break;
                case 4:
                    _isAsc = true;
                    _orderName = "LoginTime";
                    break;
                case 5:
                    _isAsc = false;
                    _orderName = "LoginTime";
                    break;
                default:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
            }
            return CurrentRepository.FindPageList<User>(pageIndex, pageSize, out totalRecord, u => true, _orderName, _isAsc);

        }

        public ClaimsIdentity CreateIdentity(User user, string authenticationType)
        {
            var ci = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            ci.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            ci.AddClaim(new Claim("DisplayName", user.DisplayName));                   
            return ci;
        }
    }
}
