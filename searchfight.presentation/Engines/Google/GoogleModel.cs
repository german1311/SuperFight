namespace Searchfight.Service.Engines
{
    public class GoogleModel
    {
        public string Kind { get; set; }

        public SearchInformationModel SearchInformation { get; set; }


        public class SearchInformationModel
        {
            public double SearchTime { get; set; }
            public long TotalResults { get; set; }
        }
    }
}
