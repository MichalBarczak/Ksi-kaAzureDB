using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSklep.Helpers;
using Interfejsy;

namespace eSklep.Controllers
{
    public abstract class BaseController : Controller
    {
        protected internal readonly BaseProvider _shopProvider;
        protected internal  readonly UserManager<IdentityUser> _userManager;
        public BaseController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager)
        {
            _shopProvider = baseShopProvider;
            _userManager = userManager;
        }
       
    }
}
