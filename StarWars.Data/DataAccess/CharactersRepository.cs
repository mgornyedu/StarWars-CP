using Microsoft.EntityFrameworkCore;
using StarWars.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Data.DataAccess
{
    public class CharactersRepository : ICharactersRepository
    {
        public CharactersRepository(SWContext dataContext)
        {
            DataContext = dataContext;
        }
        public SWContext DataContext { get; }

        public async Task<int> DeleteCharacter(int id)
        {
            var episode = await GetCharacter(id);
            if (episode == null)
                return 0;
            DataContext.Remove(episode);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRelationship(int characterId, int friendId)
        {
            var _relationship = await GetRelationship(characterId, friendId);
            if (_relationship == null)
                return 0;
            DataContext.Remove(_relationship);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<List<Cast>> GetCasts(int characterId)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Casts.Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Characters
                .Include(x => x.Casts)
                    .ThenInclude(x => x.Episode)
                .Include(x => x.Relationships)
                    .ThenInclude(x => x.Friend)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Character>> GetCharacters()
        {
            if (DataContext == null)
                return null;
            return await DataContext.Characters
                .Include(x=>x.Casts)
                    .ThenInclude(x=>x.Episode)
                .Include(x=>x.Relationships)
                    .ThenInclude(x=>x.Friend)
                .ToListAsync();
        }

        public async Task<List<V_Character>> GetCharactersView()
        {
            if (DataContext == null)
                return null;
            return await DataContext.V_Characters.ToListAsync();
        }

        public async Task<Relationship> GetRelationship(int characterId, int friendId)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Relationships
                .Include(x=>x.Character)
                .Include(x=>x.Friend)
                .FirstOrDefaultAsync(x => x.CharacterId == characterId && x.FriendId == friendId);
        }

        public async Task<List<Relationship>> GetRelationships(int characterId)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Relationships
                .Include(x => x.Character)
                .Include(x => x.Friend)
                .Where(x => x.CharacterId == characterId).ToListAsync();
        }

        public async Task<int> InsertCharacter(Character character)
        {
            if (DataContext == null || character == null)
                return 0;
            await DataContext.AddAsync(character);
            await DataContext.SaveChangesAsync();
            return character.Id;
        }

        public async Task<int> InsertRelationship(Relationship relationship)
        {
            if (DataContext == null || relationship == null)
                return 0;
            await DataContext.AddAsync(relationship);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> UpdateCharacter(Character character)
        {
            if (DataContext == null)
                return 0;
            DataContext.Update(character);
            return await DataContext.SaveChangesAsync();
        }
    }
}
