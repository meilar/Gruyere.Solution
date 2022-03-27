using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gruyere.Models;
using Gruyere.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Gruyere.Controllers
{
  public class HomeController : Controller
  {
    private readonly GruyereContext _db;

    public HomeController(GruyereContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Details()
    {
      ViewBag.Flavors = _db.Flavors.ToList();
      ViewBag.Treats = _db.Treats.ToList();
      return View();
    }
  }
}