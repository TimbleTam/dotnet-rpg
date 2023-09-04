using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using AutoMapper;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Name = "Sam", Id = 1},
            new Character { Name = "Pippin", Id = 2}
        };

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                var dbcharacter = _context.Characters.First(c => c.Id == id);

                if (dbcharacter is null)
                {
                    throw new Exception($"Modification of Character has Failed");
                }
                _context.Characters.Remove(dbcharacter);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


    public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        var dbCharacter = await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();
        var dbcharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbcharacter);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();

        try
        {
            var dbcharacter = _context.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            if (dbcharacter is null)
            {
                throw new Exception($"Modification of Character has Failed");
            }

            _mapper.Map(updatedCharacter, dbcharacter);

            dbcharacter.Name = updatedCharacter.Name;
            dbcharacter.Age = updatedCharacter.Age;
            dbcharacter.Class = updatedCharacter.Class;
            dbcharacter.Defense = updatedCharacter.Defense;
            dbcharacter.HitPoints = updatedCharacter.HitPoints;
            dbcharacter.Intelligence = updatedCharacter.Intelligence;
            dbcharacter.IsDead = updatedCharacter.IsDead;
            dbcharacter.Strength = updatedCharacter.Strength;

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbcharacter);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
}