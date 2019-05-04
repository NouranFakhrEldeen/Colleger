using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Data;
using GraduationProject.Data.Entities;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using GraduationProject.MVC.Models;
using GraduationProject.MVC.Services;

namespace GraduationProject.MVC.Controllers
{

    public class AccountController : Controller
    {

        private DatabaseContext db = new DatabaseContext();
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = db.Users.FirstOrDefault(s => s.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            Authenticate(user, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Tasks");
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.SpecializationId = new SelectList(db.Specializations, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Register(CreateUserModel user)
        {
            if(user != null)
            {
                Student entity = new Student()
                {
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Password = user.Password,
                    City = user.City,
                    Governorate = user.Governorate,
                    Street=  user.Street,
                    Grade = user.Grade ,
                    SpecializationId = user.SpecializationId
                    
                    

                };
                db.Users.Add(entity);
                db.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        private List<Claim> GetClaims(User user)
        {
            var userCms = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim (ClaimTypes.Name , user.Firstname),
                new Claim(ClaimTypes.Email, user.Email),
                

        };
            return userCms;

        }
        private void Authenticate(User user, bool rememberMe)
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            var identity = new ClaimsIdentity(GetClaims(user), DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn
                (new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                }, identity);


        }

        public ActionResult SavedRecommendations()
        {
            var recs = db.Recommendations.Where(r => r.UserId == Int32.Parse(User.Identity.GetUserId())).Select(r => r.Id).ToList();
            var recsRVM = RecommendationExtractorService.getRecommendationVMListByIdList(recs, true);
            ViewBag.recommendationList = recsRVM;
            return View();
        }

    }
}
