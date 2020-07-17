using StarWars.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Data.DataAccess
{
    public interface ICharactersRepository
    {
        Task<List<V_Character>> GetCharactersView();
        Task<List<Character>> GetCharacters();
        Task<Character> GetCharacter(int id);
        Task<Relationship> GetRelationship(int characterId, int friendId);
        Task<List<Relationship>> GetRelationships(int characterId);
        Task<List<Cast>> GetCasts(int characterId);
        Task<int> InsertCharacter(Character character);
        Task<int> InsertRelationship(Relationship relationship);
        Task<int> UpdateCharacter(Character character);
        Task<int> DeleteCharacter(int id);
        Task<int> DeleteRelationship(int characterId, int friendId);
    }
}
