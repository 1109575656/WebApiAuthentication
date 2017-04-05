using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiAuthentication.Core
{
    public class MyIdentity : IIdentity
    {
        public MyIdentity(string name) {
            this.m_name = name;
            this.m_type = "";
        }
        private string m_type;
        private string m_name;
        public  string Name
        {
            get
            {
                return this.m_name;
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return !this.m_name.Equals("");
            }
        }
        public string AuthenticationType
        {
            get
            {
                return this.m_type;
            }
        }
        
    }
}