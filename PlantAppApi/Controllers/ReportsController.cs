using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;

namespace PlantAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController(PlantAppDbContext context) : ControllerBase
    {
        
        [HttpGet("smart-schedule/{userId}")]
        public IActionResult GetSchedule(int userId)
        {
            var tasks = context.Schedules
                .Include(s => s.USERPLANTIdUserPlantNavigation)
                .Where(s => s.USERPLANTIdUserPlantNavigation.USERSIdUsers == userId
                         && s.IsActive == true
                         && s.USERPLANTIdUserPlantNavigation.IsAlive == true
                         && s.NextDueDate < DateTime.Now) 
                .Select(s => new
                {
                    Roslina = s.USERPLANTIdUserPlantNavigation.Nickname,
                    Czynnosc = s.ActionType,
                    DniOpoznienia = (DateTime.Now - s.NextDueDate.Value).Days
                })
                .ToList();
            return Ok(tasks);
        }

        
        [HttpGet("graveyard/{userId}")]
        public IActionResult GetGraveyard(int userId)
        {
            var stats = context.UserPlants
                .Where(p => p.USERSIdUsers == userId && p.IsAlive == false)
                .GroupBy(p => p.PLANTSPECIESIdPlantSpeciesNavigation.Name)
                .Select(g => new { Gatunek = g.Key, Zgony = g.Count() })
                .ToList();
            return Ok(stats);
        }

        
        [HttpGet("achievements/{userId}")]
        public IActionResult GetAchievements(int userId)
        {
            var earned = context.UsersHasAchievements
                .Where(ua => ua.USERSIdUsers == userId)
                .Select(ua => ua.ACHIEVEMENTSIdAchievementsNavigation)
                .ToList();
            return Ok(earned);
        }
    }
}