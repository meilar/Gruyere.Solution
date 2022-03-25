using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gruyere.Models;
using System.Collections.Generic;
using System.Linq;

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
      List<Treat> model = _db.Treats.ToList();
      return View(model);
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
      var thisTreat = _db.Treats
          .Include(thisTreat => thisTreat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(thisTreat => thisTreat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat thisTreat)
    {
      _db.Entry(thisTreat).State = EntityState.Modified;
      _db.SaveChanges();
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