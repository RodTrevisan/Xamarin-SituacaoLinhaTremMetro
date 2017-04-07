using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using SituacaoMetroTrem.Interfaces;

namespace SituacaoMetroTrem.Services
{
    public class RestClient<T>: IRestClient<T> where T : class
    {
        public async Task<IEnumerable<T>> HttpGetMethod(string url)
        {
            var result = Enumerable.Empty<T>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(json))
                    {
                        result = await Task.Run(() =>
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        });

                    }
                };
            }

            return result;
        }
    }
}
