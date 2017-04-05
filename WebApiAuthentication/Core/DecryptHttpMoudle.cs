using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebApiAuthentication.Core
{
    public class DecryptHttpMoudle : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Context_AuthenticateRequest;
        }
        /// <summary>
        /// 替换HttpContext.Current.User实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            if (app == null) {
                app.Context.User = null;
                return;
            }
            var cookie = HttpContext.Current.Request.Headers.GetValues("Cookie"); //初使化并设置Cookie的名称
            if (cookie == null )
            {
                app.Context.User = new MyPrincipal("");
                return;
            }
            if (cookie.Count() <1 )
            {
                app.Context.User = new MyPrincipal("");
                return;
            }
            MyFormsAuthenticationTicket ticket;
            try
            {
                 string decryptStr= MyFormsAuthentication.DecryptDES(cookie[0].Substring(cookie[0].IndexOf('=')+1),"11111111");
                 ticket = JsonConvert.DeserializeObject<MyFormsAuthenticationTicket>(decryptStr);
            }
            catch (Exception)
            {
                app.Context.User = new MyPrincipal("");
                return;
            }
            if (ticket == null)
            {
                app.Context.User = new MyPrincipal("");
                return;
            }
            app.Context.User = new MyPrincipal(ticket.Name);
        }
    }
}