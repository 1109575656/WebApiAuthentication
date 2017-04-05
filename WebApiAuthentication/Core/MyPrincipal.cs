using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiAuthentication.Core
{
    public class MyPrincipal : IPrincipal
    {
        public MyPrincipal(string name)
        {
            _identity = new MyIdentity(name);
        }

        private IIdentity _identity;

        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}