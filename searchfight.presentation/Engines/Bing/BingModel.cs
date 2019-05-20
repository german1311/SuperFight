namespace Searchfight.Service.Engines
{
    public class BingModel
    {
        public string _Type { get; set; }

        public WebPageModel WebPages { get; set; }

        public class WebPageModel
        {
            public string WebSearchUrl { get; set; }

            public long TotalEstimatedMatches { get; set; }
        }
    }
}
