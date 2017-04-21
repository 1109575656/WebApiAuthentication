using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiAuthentication.Core
{
    public class MyPrincipal : IPrincipal
    {
        private string[] m_roles;
        public MyPrincipal(string name,string roles)
        {
            if (!string.IsNullOrEmpty(roles)) {
                string[] rolesArr = roles.Split(',');
                m_roles = new string[rolesArr.Count()];
                for (int i = 0; i < rolesArr.Count(); i++)
                {
                    m_roles[i] = rolesArr[i];
                }
            }
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
            if ((role == null) || (this.m_roles == null))
            {
                return false;
            }
            for (int i = 0; i < this.m_roles.Length; i++)
            {
                if ((this.m_roles[i] != null) && (string.Compare(this.m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    return true;
                }
            }
            return false;
        }



        
        

    }
}