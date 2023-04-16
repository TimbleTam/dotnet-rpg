using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;


namespace dotnet_rpg.Controllers
{
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
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddSingle(Character newCharacter) {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpGet("GetRandom")]
        public ActionResult<Character> GetRandom()
        {
            return Ok(ran);
        }
    }
}