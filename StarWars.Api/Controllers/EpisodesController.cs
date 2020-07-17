using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWars.Data.DataAccess;
using StarWars.Data.Models;

namespace StarWars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : AsyncController
    {
        public IEpisodesRepository EpisodesRepository { get; }

        public EpisodesController(IEpisodesRepository episodesRepository)
        {
            EpisodesRepository = episodesRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await AsyncGet(EpisodesRepository.GetEpisodes());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            return await AsyncGet(EpisodesRepository.GetEpisode(id.Value));
        }

        [HttpPost]
        [Route("episode")]
        public async Task<IActionResult> Post([FromBody] Episode episode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (episode == null)
                return BadRequest();
            return await AsyncSave(EpisodesRepository.InsertEpisode(episode));
        }
        [HttpPost]
        [Route("cast")]
        public async Task<IActionResult> Post([FromBody] Cast cast)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (cast == null)
                return BadRequest();
            return await AsyncSave(EpisodesRepository.InsertCast(cast));
        }

        [HttpPut()]
        [Route("episode")]
        public async Task<IActionResult> Put([FromBody] Episode episode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (episode == null)
                return BadRequest();
            return await AsyncSave(EpisodesRepository.UpdateEpisode(episode));
        }

        [HttpDelete()]
        [Route("episode/{id:int}")]
        public async Task<IActionResult> DeleteEpisode(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            return await AsyncSave(EpisodesRepository.DeleteEpisode(id.Value));
        }
        [HttpDelete()]
        [Route("cast/{characterId:int}/{episodeId:int}")]
        public async Task<IActionResult> DeleteCast(int? characterId, int? episodeId)
        {
            if (!characterId.HasValue || !episodeId.HasValue)
                return BadRequest();
            return await AsyncSave(EpisodesRepository.DeleteCast(characterId.Value, episodeId.Value));
        }
    }
}
