using Newtonsoft.Json;
using StarWars.Data.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Tests
{
    public class CharactersTests : TestBase
    {
        [Fact]
        public async Task OK_InsertCharacterTest()
        {
            var character = new Character()
            {
                Name = "Obi-Wan Kenobi"
            };
            var content = JsonConvert.SerializeObject(character);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/characters/character", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_UpdateCharacterTest()
        {
            var character = new Character()
            {
                Id = 2,
                Name = "Anakin Skywalker"
            };
            var content = JsonConvert.SerializeObject(character);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/characters/character", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_DeleteCharacterTest()
        {
            var response = await client.DeleteAsync("/api/characters/character/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_GetSingleCharacterTest()
        {
            var response = await client.GetAsync("/api/characters/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_GetCharacterTest()
        {
            var response = await client.GetAsync("/api/characters");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_GetCharacterViewTest()
        {
            var response = await client.GetAsync("/api/characters/view");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task OK_InsertRelationshipTest()
        {
            var character = new Relationship()
            {
                CharacterId = 2,
                FriendId = 1
            };
            var content = JsonConvert.SerializeObject(character);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/characters/relationship", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task OK_DeleteRelationshipTest()
        {
            var response = await client.DeleteAsync("/api/characters/relationship/1/3");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task NotFound_DeleteRelationshipTest()
        {
            var response = await client.DeleteAsync("/api/characters/relationship/1/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task NotFound_DeleteCharacterTest()
        {
            var response = await client.DeleteAsync("/api/characters/relationship/7");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BadRequest_InsertCharacterTest()
        {
            var character = new Character()
            {
                Id = 1,
                Name = "The Force Awakens"
            };
            var content = JsonConvert.SerializeObject(character);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/characters/character", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void BadRequest_UpdateCharacterTest()
        {
            var character = new Character()
            {
                Id = 9,
                Name = ""
            };
            var content = JsonConvert.SerializeObject(character);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/characters/character", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
