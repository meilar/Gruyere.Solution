using Microsoft.AspNetCore.Mvc;
using Gruyere.Models;
using Gruyere.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Gruyere.Controllers
{
  public class AccountController : Controller
  {
    private readonly GruyereContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GruyereContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }
    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email,model.Password,isPersistent:true,lockoutOnFailure:false);
      if(result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();  
      }
    }
    public ActionResult LogOff()
    {
      return View();
    }

    [HttpPost, ActionName("LogOff")]
    public async Task<ActionResult> LogOffConfirmed()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}