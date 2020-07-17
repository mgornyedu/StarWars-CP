using StarWars.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Data.DataAccess
{
    public interface IEpisodesRepository
    {
        Task<List<Episode>> GetEpisodes();
        Task<Episode> GetEpisode(int id);
        Task<Cast> GetCast(int characterId, int episodeId);
        Task<List<Cast>> GetCasts(int episodeId);
        Task<int> InsertEpisode(Episode episode);
        Task<int> InsertCast(Cast episode);
        Task<int> UpdateEpisode(Episode episode);
        Task<int> DeleteEpisode(int id);
        Task<int> DeleteCast(int characterId, int episodeId);
    }
}
