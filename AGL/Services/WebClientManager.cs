using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AGL.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AGL.Services
{
    public class WebClientManager
    {
        public static async Task<List<RootObject>> GetItemList()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("http://agl-developer-test.azurewebsites.net/people.json", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {

                        var petList = response.Content.ReadAsAsync<List<RootObject>>().Result;

                        return petList;

                    }

                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            throw new HttpResponseException(HttpStatusCode.NoContent);

        }
    }
}