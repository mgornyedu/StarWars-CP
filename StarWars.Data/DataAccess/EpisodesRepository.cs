using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StarWars.Data.DataAccess
{
    public class EpisodesRepository : IEpisodesRepository
    {
        public EpisodesRepository(SWContext dataContext)
        {
            DataContext = dataContext;
        }
        public SWContext DataContext { get; }

        public async Task<Episode> GetEpisode(int id)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Episodes
                .Include(x=>x.Casts)
                    .ThenInclude(x=>x.Character)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<Episode>> GetEpisodes()
        {
            if (DataContext == null)
                return null;
            return await DataContext.Episodes
                .Include(x => x.Casts)
                    .ThenInclude(x => x.Character)
                .ToListAsync();
        }

        public async Task<int> DeleteEpisode(int id)
        {
            var episode = await GetEpisode(id);
            if (episode == null)
                return 0;
            DataContext.Remove(episode);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> InsertEpisode(Episode episode)
        {
            if (DataContext == null || episode == null)
                return 0;
            await DataContext.AddAsync(episode);
            await DataContext.SaveChangesAsync();
            return episode.Id;
        }

        public async Task<int> UpdateEpisode(Episode episode)
        {
            if (DataContext == null)
                return 0;
            DataContext.Update(episode);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> InsertCast(Cast cast)
        {
            if (DataContext == null || cast == null)
                return 0;
            await DataContext.AddAsync(cast);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCast(int characterId, int episodeId)
        {
            var _cast = await GetCast(characterId, episodeId);
            if (_cast == null)
                return 0;
            DataContext.Remove(_cast);
            return await DataContext.SaveChangesAsync();
        }

        public async Task<Cast> GetCast(int characterId, int episodeId)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Casts
                .Include(x=>x.Character)
                .Include(x=>x.Episode)
                .FirstOrDefaultAsync(x => x.CharacterId == characterId && x.EpisodeId == episodeId);

        }

        public async Task<List<Cast>> GetCasts(int episodeId)
        {
            if (DataContext == null)
                return null;
            return await DataContext.Casts
                .Include(x => x.Character)
                .Include(x => x.Episode)
                .Where(x => x.EpisodeId == episodeId).ToListAsync();
        }
    }
}
