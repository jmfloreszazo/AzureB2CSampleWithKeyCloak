using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using WebAppB2CSample.Models;

namespace WebAppB2CSample.Controllers;

[Authorize]
public class HomeController : Controller
{
    public HomeController(ITokenAcquisition tokenAcquisition)
    {
        _ = tokenAcquisition;
    }

    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}