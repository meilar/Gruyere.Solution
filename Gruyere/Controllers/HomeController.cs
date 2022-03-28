using Microsoft.AspNetCore.Mvc;
using Gruyere.Models;
using System.Linq;

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