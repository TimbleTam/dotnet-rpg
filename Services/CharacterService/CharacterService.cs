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

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                var character = characters.First(c => c.Id == id);

                if (character is null)
                {
                    throw new Exception($"Modification of Character has Failed");
                }
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
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
        serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();
        var character = characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();

        try
        {
            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            if (character is null)
            {
                throw new Exception($"Modification of Character has Failed");
            }

            _mapper.Map(updatedCharacter, character);

            character.Name = updatedCharacter.Name;
            character.Age = updatedCharacter.Age;
            character.Class = updatedCharacter.Class;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.IsDead = updatedCharacter.IsDead;
            character.Strength = updatedCharacter.Strength;

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
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