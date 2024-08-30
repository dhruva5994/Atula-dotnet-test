using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticalDemo.Models;
using System.Threading.Tasks;
using PracticalDemo.Models;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    // private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
   // public string validatorobject = new LoginViewModelValidator().RuleFor;
    public AccountController(ApplicationDbContext context)
    {
        _context = context;
       
    }
    
    //public AccountController(SignInManager<ApplicationUser> signInManager)
    //{
    //    _signInManager = signInManager;
    //}

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // var result = _context.loginViewModels.Include(x => x.Email).Any(x =>x.Email == model.Email).
            var user = await _context.loginViewModels
                 .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                var passwordcorrect = await _context.loginViewModels.FirstOrDefaultAsync(p=>p.Password == model.Password);
                if (passwordcorrect != null)
                {
                    ViewBag.Email = model.Email;
                    TempData["Success"] = "Successfully Loggedin";
                    return RedirectToAction("ProductList", "Product");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return View(model);
    }
}
