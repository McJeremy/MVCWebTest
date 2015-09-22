using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninesky.BLL;
using Ninesky.Common;
using Ninesky.Web.Areas.Member.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ninesky.Web.Areas.Member.Controllers
{
    public class UserController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {                
                return HttpContext.GetOwinContext().Authentication;
                
            }
        }

        private UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        // GET: Member/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VerificationCode()
        {
            var code = Security.CreateVerificationText(6);
            var img = Security.CreateVerificationImage(code, 160, 30);
            img.Save(Response.OutputStream, ImageFormat.Jpeg);
            TempData["VerificationCode"] = code.ToUpper();
            return null;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel  register)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(register);
            }
            if (ModelState.IsValid)
            {
                if (userService.Exist(register.UserName))
                {
                    ModelState.AddModelError("UserName", "用户名已存在");
                }
                else
                {
                    Ninesky.Models.User _user = new Ninesky.Models.User()
                    {
                        UserName = register.UserName,
                        //默认用户组代码写这里
                        DisplayName = register.DisplayName,
                        Password = Security.Sha256(register.Password),
                        //邮箱验证与邮箱唯一性问题
                        Email = register.Email,
                        //用户状态问题
                        Status = 0,
                        RegistrationTime = System.DateTime.Now,
                        LoginIP="",
                        LoginTime=DateTime.Now
                    };

                    _user = userService.Add(_user);
                    if (_user.UserID > 0)
                    {
                        //return Content("注册成功！");
                        ////AuthenticationManager.SignIn();
                        var ci = userService.CreateIdentity(_user);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(ci);

                        return RedirectToAction("Index", "Home");
                    }
                    else { ModelState.AddModelError("", "注册失败！"); }

                }
            }
            return View(register);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(loginViewModel.UserName);
                if (null == _user)
                {
                    ModelState.AddModelError("UserName", "用户不存在");
                }
                else if (_user.Password == Security.Sha256(loginViewModel.Password))
                {
                    var ci = userService.CreateIdentity(_user);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = loginViewModel.RememberMe }, ci);

                    _user.LoginIP = Request.UserHostAddress;
                    _user.LoginTime = DateTime.Now;
                    userService.Update(_user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "密码错误");
                }
               
            }
            return View();
        }

        [HttpGet]
        public ActionResult Loginout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Url.Content("~/"));
        }
    }
}