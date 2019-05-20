using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using System;
using searchfight.presentation.Engines;

namespace Searchfight.Service.Engines
{
    /// <summary>
    /// Google custom search engine API
    /// doc: https://developers.google.com/custom-search/json-api/v1/using_rest
    /// 100 requests/day since 31/08/2017
    /// </summary>
    public class GoogleEngine : IEngine
    {

        private readonly string _apiUrl;
        private readonly string _apiKey;

        /// <summary>
        /// public URL https://cse.google.com/cse/publicurl?cx=017576662512468239146:omuauf_lfve
        /// </summary>
        private readonly string _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GoogleEngine(string apiUrl, string apiKey, string context)
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException("apiUrl required");

            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey required");

            if (string.IsNullOrEmpty(context))
                throw new ArgumentNullException("context required");

            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _context = context;
        }

        public string Name
        {
            get { return "Google"; }
        }

        public async Task<Result> FindAsync(string word)
        {
            var queryString = $"{_apiUrl}?cx={_context}&key={_apiKey}&q={word}";

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(queryString);
                if (result != null)
                    logger.Info(result.ToString());

                if (!result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(result.StatusCode + await result.Content.ReadAsStringAsync());
                }

                var content = await result.Content.ReadAsStringAsync();
                logger.Trace(content);

                var model = JsonConvert.DeserializeObject<GoogleModel>(content);
                return Result.Create(Name, word, model.SearchInformation.TotalResults);
            }
        }
    }
}
