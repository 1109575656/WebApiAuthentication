using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebApiAuthentication.Core
{
    public class MyFormsAuthenticationTicket
    {
        public MyFormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData)
        {
            this.Version = version;
            this.Name = name;
            this.Expiration = expiration;
            this.IssueDate = issueDate;
            this.IsPersistent = isPersistent;
            this.UserData = userData;
            this.CookiePath = FormsAuthentication.FormsCookiePath;
            DtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssss");
        }

        public string CookiePath { get; set; }

        public DateTime Expiration { get; set; }

        public bool Expired { get; set; }

        public bool IsPersistent { get; set; }

        public DateTime IssueDate { get; set; }

        public string Name { get; set; }

        public string UserData { get; set; }

        public int Version { get; set; }

        public string DtNow { get; set; }
    }
}