using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gruyere.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gruyere.Controllers
{
  public class FlavorController : Controller
  {
    private readonly GruyereContext _db;

    public FlavorController(GruyereContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flav)
    {
      _db.Flavors.Add(flav);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var flav = _db.Flavors
          .Include(flav => flav.JoinEntities)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flav => flav.FlavorId == id);
      return View(flav);
    }
    public ActionResult Edit(int id)
    {
      var flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      return View(flav);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flav)
    {
      _db.Entry(flav).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      return View(flav);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      _db.Flavors.Remove(flav);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}