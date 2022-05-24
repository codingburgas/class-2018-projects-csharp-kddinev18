using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Operations
    {
        public static int Register(string userName, string email, string password)
        {
            return UserLogic.Register(userName, email, password);
        }
    }
}
