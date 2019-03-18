using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThinCode.Models;
using ThinCode.Models.Tools;

namespace ThinCode.Controllers
{
    public class UserController : Controller
    {
        private CodeliteEntities1 db = new CodeliteEntities1();
        

        [HttpGet]
        public ActionResult UserLogin(string returnURL)
        {
            var userinfo = new LogInVM();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.ReturnURL = returnURL;
                return View(userinfo);
            }
            catch
            {
                throw;
            }
        }

        //POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LogInVM entity)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[GenerateSaltKey._SaltLengthLimit];

            try
            {
                using (db = new CodeliteEntities1())
                {
                    // Ensure we have a valid viewModel to work with
                    if (!ModelState.IsValid)
                        return View(entity);

                    //Retrive Stored HASH Value From Database According To Username (one unique field)
                    var userInfo = db.user_Credential.Where(s => s.user_Name == entity.UserName.Trim()).FirstOrDefault();

                    //Assign HASH Value
                    if (userInfo != null)
                    {
                        OldHASHValue = userInfo.HASH;
                        SALT = userInfo.SALT;
                    }

                    bool isLogin = LogInVM.CompareHashValue(entity.Password, entity.UserName, OldHASHValue, SALT);

                    if (isLogin)
                    {
                        //Login Success
                        //For Set Authentication in Cookie (Remeber ME Option)
                        SignInRemember(entity.UserName, entity.IsRememberMe);

                        //Set A Unique ID in session
                        Session["UserID"] = userInfo.user_Name;

                        // If we got this far, something failed, redisplay form
                        // return RedirectToAction("Index", "Dashboard");
                        return RedirectToLocal(entity.ReturnURL);
                    }
                    else
                    {
                        //Login Fail
                        ModelState.AddModelError("","Access Denied! Wrong Credential");
                        return View(entity);
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        #region ================Importent Utils===============
        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "AnswareTable");
            }
            catch
            {
                throw;
            }
        }

        //GET: SignInAsync   
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }
        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }
        #endregion

        //Refer(SALT): http://codereview.stackexchange.com/questions/93614/salt-generation-in-c`  
        //Refer(SHA512): http://zurb.com/forrst/posts/C_SHA512_Encryption_Hashing-UaL
        //Refer 1:  https://www.mssqltips.com/sqlservertip/4037/storing-passwords-in-a-secure-way-in-a-sql-server-database/
        //Refer 2: https://crackstation.net/hashing-security.htm
    }
}
