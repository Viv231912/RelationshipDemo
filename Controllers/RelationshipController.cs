using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationshipDemo.Data;
using RelationshipDemo.Data.Models;
using RelationshipDemo.DTOs;

namespace RelationshipDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        //DataContext  and inject datacontext
        private readonly DataContext _context;
        
        //Create constructor
        public RelationshipController(DataContext context)
        {
            _context = context;
        }

        //Get single character
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>>  GetCharacterById(int id)
        {
            var character = await _context.Characters
                            .Include(c => c.Backpack)
                            .Include(c => c.Weapons)
                            .Include(c => c.Factions)
                            .FirstOrDefaultAsync(c => c.Id == id);
            
            return Ok(character);
        }

        //Inject
        //Post method
        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDto request)
        {
            var newCharacter = new Character
            {
                Name = request.Name,
            };
            var backpack = new Backpack
            {
                Description = request.Backpack.Description,
                Character = newCharacter
            };
            var weapons = request.Weapons.Select(w => new Weapon {Name = w.Name, 
                                                                  Character = newCharacter })
                                                                  .ToList();
            var factions = request.Factions.Select(f => new Faction { Name = f.Name,
                                                                      Characters = new List<Character> { newCharacter }
                                                                      }).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _context.Characters.Add(newCharacter);

            await _context.SaveChangesAsync();

            return Ok(await _context.Characters.Include(c=>c.Backpack)
                                               .Include(c=>c.Weapons)
                                               .ToListAsync());

        }
    }
}
