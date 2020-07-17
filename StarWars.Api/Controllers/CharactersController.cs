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
    public class CharactersController : AsyncController
    {
        public ICharactersRepository CharactersRepository { get; }

        public CharactersController(ICharactersRepository charactersRepository)
        {
            CharactersRepository = charactersRepository;
        }
        /// <summary>
        /// Pobranie wszystkich postaci
        /// </summary>
        /// <returns>Resultat akcji</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await AsyncGet(CharactersRepository.GetCharacters());
        }

        /// <summary>
        /// Pobranie agregowanego widoku wszystkich postaci
        /// </summary>
        /// <returns>Resultat akcji</returns>
        [HttpGet]
        [Route("View")]
        public async Task<IActionResult> GetView()
        {
            return await AsyncGet(CharactersRepository.GetCharactersView());
        }

        /// <summary>
        /// Pobranie postaci za pomocą ID
        /// </summary>
        /// <param name="id">Identyfikator postaci</param>
        /// <returns>Resultat akcji</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            return await AsyncGet(CharactersRepository.GetCharacter(id.Value));
        }

        [HttpPost]
        [Route("character")]
        public async Task<IActionResult> Post([FromBody] Character character)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (character == null)
                return BadRequest();
            return await AsyncSave(CharactersRepository.InsertCharacter(character));
        }

        [HttpPost]
        [Route("relationship")]
        public async Task<IActionResult> Post([FromBody] Relationship relationship)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (relationship == null)
                return BadRequest();
            return await AsyncSave(CharactersRepository.InsertRelationship(relationship));
        }

        [HttpPut()]
        [Route("character")]
        public async Task<IActionResult> Put([FromBody] Character character)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (character == null)
                return BadRequest();
            return await AsyncSave(CharactersRepository.UpdateCharacter(character));
        }

        [HttpDelete("{id:int}")]
        [Route("character/{id:int}")]
        public async Task<IActionResult> DeleteCharacter(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            return await AsyncSave(CharactersRepository.DeleteCharacter(id.Value));
        }
        [HttpDelete()]
        [Route("relationship/{characterId:int}/{friendId:int}")]
        public async Task<IActionResult> DeleteCast(int? characterId, int? friendId)
        {
            if (!characterId.HasValue || !friendId.HasValue)
                return BadRequest();
            return await AsyncSave(CharactersRepository.DeleteRelationship(characterId.Value, friendId.Value));
        }
    }
}
