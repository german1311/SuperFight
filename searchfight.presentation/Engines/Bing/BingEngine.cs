using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using System;
using searchfight.presentation.Engines;

namespace Searchfight.Service.Engines
{
    /// <summary>
    /// Bing search engine
    /// doc: https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/search-the-web
    /// trial: from 31/08/2017
    /// </summary>
    public class BingEngine : IEngine
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string Name
        {
            get { return "Bing"; }
        }

        public BingEngine(string apiUrl, string apiKey)
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException("apiUrl required");

            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey required");

            _apiUrl = apiUrl;
            _apiKey = apiKey;
        }

        public async Task<Result> FindAsync(string word)
        {
            var queryString = $"?q={word}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

                var result = await client.GetAsync(_apiUrl + queryString);

                if (result != null)
                    logger.Info(result.ToString());

                if (!result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(result.StatusCode + await result.Content.ReadAsStringAsync());
                }

                var content = await result.Content.ReadAsStringAsync();
                logger.Trace(content);

                var model = JsonConvert.DeserializeObject<BingModel>(content);
                return Result.Create(Name, word, model.WebPages.TotalEstimatedMatches);
            }
        }
    }
}
