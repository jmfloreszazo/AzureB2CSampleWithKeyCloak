using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebAPIB2CSample.Models;

namespace WebAPIB2CSample.Controllers;

[Authorize]
[Route("api/[controller]")]
[RequiredScope("task.read")]
public class SampleController : Controller
{
    private static readonly Dictionary<int, Sample> sampleDictionary = new()
    {
        { 1, new Sample { Id = 1, Description = "Description 1" } },
        { 2, new Sample { Id = 2, Description = "Description 2" } },
        { 3, new Sample { Id = 3, Description = "Description 3" } }
    };

    [HttpGet]
    public IEnumerable<Sample> Get()
    {
        return new List<Sample>(sampleDictionary.Values);
    }
}