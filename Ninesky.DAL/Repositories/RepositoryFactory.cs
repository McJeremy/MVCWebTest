using Ninesky.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL.Repositories
{
    public static class RepositoryFactory
    {
        public static IUserRepository UserRepository
        {
            get { return new UserRepository(); }
        }
        public static IUserConfigRepository UserConfigRepository
        {
            get { return new UserConfigRepository(); }
        }
        public static IRoleRepository RoleRepository
        {
            get { return new RoleRepository(); }
        }
    }
}
