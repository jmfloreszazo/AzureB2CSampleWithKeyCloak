using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIB2CSample.Models;

namespace WebApp.Services;

public interface ISampleService
{
    Task<IEnumerable<Sample>> GetAsync();
}