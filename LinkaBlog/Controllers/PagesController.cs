using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LinkaBlog.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult TermsConditions()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}