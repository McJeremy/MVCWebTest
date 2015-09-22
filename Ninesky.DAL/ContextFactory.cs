using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public static class ContextFactory
    {
        public static NineskyDbContext GetCurrentContext()
        {
            NineskyDbContext dbContext = CallContext.GetData("NineskyContext") as NineskyDbContext;

            if (null == dbContext)
            {
                dbContext = new NineskyDbContext();
                CallContext.SetData("NineskyContext", dbContext);
            }
            return dbContext;
        }
    }
}

