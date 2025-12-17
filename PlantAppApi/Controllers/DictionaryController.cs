using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;

namespace PlantAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController(PlantAppDbContext context) : ControllerBase
    {
        
        [HttpGet("species")]
        public async Task<IActionResult> GetSpecies()
        {
            return Ok(await context.PlantSpecies.ToListAsync());
        }

       
        [HttpGet("species/{id}/diseases")]
        public async Task<IActionResult> GetDiseases(int id)
        {
            var diseases = await context.PlantSpeciesHasDiseasPests
                .Where(x => x.PLANTSPECIESIdPlantSpecies == id)
                .Select(x => x.DISEASESPESTSIdDiseasesPestsNavigation)
                .ToListAsync();
            return Ok(diseases);
        }
    }
}