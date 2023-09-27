using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        static float RandomisedAge() {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (100 - 1) * 1);
            return (float)val;
        }

        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Name = "Sam", Id = 1},
            new Character { Name = "Pippin", Id = 2}
        };

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        private Character ran = new Character {
            Name = "Ted Lasso",
            Age = RandomisedAge(),
        };


        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddSingle(AddCharacterDto newCharacter) {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _characterService.AddCharacter(newCharacter, userId));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpGet("GetRandom")]
        public ActionResult<Character> GetRandom()
        {
            return Ok(ran);
        }

        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter) {
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(await _characterService.UpdateCharacter(updatedCharacter));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(await _characterService.DeleteCharacter(id));
        }
    }
}