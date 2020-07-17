using Newtonsoft.Json;
using StarWars.Data.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Tests
{
    public class EpisodesTests : TestBase
    {
        [Fact]
        public async Task OK_InsertEpisodeTest()
        {
            var episode = new Episode()
            {
                Id = 7,
                Name = "The Force Awakens"
            };
            var content = JsonConvert.SerializeObject(episode);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/episodes/episode", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_UpdateEpisodeTest()
        {
            var episode = new Episode()
            {
                Id = 6,
                Name = "Episode VI - Return of the Jedi"
            };
            var content = JsonConvert.SerializeObject(episode);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/episodes/episode", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_DeleteEpisodeTest()
        {
            var response = await client.DeleteAsync("/api/episodes/episode/6");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_GetSingleEpisodeTest()
        {
            var response = await client.GetAsync("/api/episodes/6");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void OK_GetEpisodeTest()
        {
            var response = await client.GetAsync("/api/episodes");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task OK_InsertCastTest()
        {
            var episode = new Cast()
            {
                CharacterId = 5,
                EpisodeId = 1
            };
            var content = JsonConvert.SerializeObject(episode);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/episodes/cast", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task OK_DeleteCastTest()
        {
            var response = await client.DeleteAsync("/api/episodes/cast/1/4");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task NotFound_DeleteCastTest()
        {
            var response = await client.DeleteAsync("/api/episodes/cast/1/3");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task NotFound_DeleteEpisodeTest()
        {
            var response = await client.DeleteAsync("/api/episodes/cast/7");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BadRequest_InsertEpisodeTest()
        {
            var episode = new Episode()
            {
                Id = 1,
                Name = "The Force Awakens"
            };
            var content = JsonConvert.SerializeObject(episode);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/episodes/episode", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void BadRequest_UpdateEpisodeTest()
        {
            var episode = new Episode()
            {
                Id = 7,
                Name = "Episode VI - Return of the Jedi"
            };
            var content = JsonConvert.SerializeObject(episode);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/episodes/episode", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
