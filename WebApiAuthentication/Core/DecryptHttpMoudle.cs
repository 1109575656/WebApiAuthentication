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
            if (app == null)
            {
                app.Context.User = null;
                return;
            }
            var cookie = HttpContext.Current.Request.Headers.GetValues("Cookie");
            if (cookie == null)
            {
                app.Context.User = new MyPrincipal("",null);
                return;
            }
            if (cookie.Count() < 1)
            {
                app.Context.User = new MyPrincipal("", null);
                return;
            }
            MyFormsAuthenticationTicket ticket;
            try
            {
                //获取cookie值的另一种方式，特殊符号会被省略
                //var sdd = Request.Headers.GetCookies(FormsAuthentication.FormsCookieName).FirstOrDefault();
                //var value=sdd[FormsAuthentication.FormsCookieName].Value;
                string decryptStr = MyFormsAuthentication.DecryptDES(cookie[0].Substring(cookie[0].IndexOf('=') + 1), "11111111");
                ticket = JsonConvert.DeserializeObject<MyFormsAuthenticationTicket>(decryptStr);
            }
            catch (Exception)
            {
                app.Context.User = new MyPrincipal("", null);
                return;
            }
            if (ticket == null)
            {
                app.Context.User = new MyPrincipal("", null);
                return;
            }
            app.Context.User = new MyPrincipal(ticket.Name,ticket.UserData);
        }
    }
}