using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using WebApp.Services;

namespace TodoListClient.Controllers;

public class SampleController : Controller
{
    private readonly ISampleService _sampleService;

    public SampleController(ISampleService sampleService)
    {
        _sampleService = sampleService;
    }

    [AuthorizeForScopes(ScopeKeySection = "SampleList:SampleListScope")]
    public async Task<ActionResult> Index()
    {
        return View(await _sampleService.GetAsync());
    }
}