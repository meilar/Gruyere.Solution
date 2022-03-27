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
  public class FlavorController : Controller
  {
    private readonly GruyereContext _db;

    public FlavorController(GruyereContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Flavors = _db.Flavors.ToList();
      return View();
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
      ViewBag.Flavors = _db.Flavors.ToList();
      ViewBag.Treats = _db.Treats.ToList();
      var flav = _db.Flavors
          .Include(flav => flav.JoinEntities)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flav => flav.FlavorId == id);
      return View(flav);
    }
    public ActionResult Edit(int id)
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      ViewBag.Treats = _db.Treats.ToList();
      var flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      return View(flav);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flav, int TreatId)
    {
      _db.Entry(flav).State = EntityState.Modified;
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flav.FlavorId });
        _db.SaveChanges();
      }
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