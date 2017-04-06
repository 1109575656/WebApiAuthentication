using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebApiAuthentication.Core;

namespace WebApiAuthentication.Controllers
{
    [RoutePrefix("Test")]
    public class TestController : ApiController
    {
        [Route("SignIn")]
        [HttpGet]
        public IHttpActionResult SignIn()
        {
            MyFormsAuthenticationTicket ticket = new MyFormsAuthenticationTicket(
         2, new Random().Next().ToString(), DateTime.Now, DateTime.Now.AddDays(1), true, "admin");
            string encryptStr = MyFormsAuthentication.EncryptDES(JsonConvert.SerializeObject(ticket), "11111111");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptStr);
            //cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();
            //  写入Cookie
            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
            return Ok("登陆成功");
        }
        [Route("SignOut")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult SignOut()
        {
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            return Ok("退出成功");
        }

        /// <summary>
        /// 测试是否登陆后可以访问
        /// </summary>
        /// <returns></returns>
        [Route("TestSuccess")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult TestSuccess()
        {
            return Ok("访问成功");
        }
    }
}
