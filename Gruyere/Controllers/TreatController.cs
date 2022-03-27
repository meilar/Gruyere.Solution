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
  public class TreatController : Controller
  {
    private readonly GruyereContext _db;

    public TreatController(GruyereContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Treats = _db.Treats.ToList();
      return View();
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat newTreat)
    {
      _db.Treats.Add(newTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      ViewBag.Flavors = _db.Flavors.ToList();
      ViewBag.Treats = _db.Treats.ToList();
      var thisTreat = _db.Treats
          .Include(thisTreat => thisTreat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult Edit(int id)
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      ViewBag.Flavors = _db.Flavors.ToList();
      var thisTreat = _db.Treats.FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat thisTreat, int FlavorId)
    {
      _db.Entry(thisTreat).State = EntityState.Modified;
      _db.SaveChanges();
      if(FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = thisTreat.TreatId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}