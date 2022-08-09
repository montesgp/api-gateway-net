using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using apex_apigateway_ocelot.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace apex_apigateway_ocelot.Aggregators
{
    public class UsersAlbumsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var userResponseContent = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var postResponseContent = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<User>>(userResponseContent);
            var albums = JsonConvert.DeserializeObject<List<Album>>(postResponseContent);

            foreach (var user in users)
            {
                var userAlbums = albums.Where(p => p.UserId == user.Id).ToList();
                user.Albums.AddRange(userAlbums);
            }

            var postByUserString = JsonConvert.SerializeObject(users);

            var stringContent = new StringContent(postByUserString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
