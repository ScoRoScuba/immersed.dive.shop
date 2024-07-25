using System.Threading.Tasks;
using immersed.dive.shop.repository;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly DiveShopDBContext _context;

    public DatabaseController(DiveShopDBContext context)
    {
        _context = context;
    }
    
    [HttpGet("reseed")]
    public async Task<IActionResult> Get()
    {
        await Seed.SeedData(_context);
        return Ok();
    }
    
}