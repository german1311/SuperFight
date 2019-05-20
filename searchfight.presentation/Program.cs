using NLog;
using searchfight.presentation.Engines;
using Searchfight.Service.Engines;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace searchfight.presentation
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            if (!args.Any())
                return;

            try
            {
                MainAsync(args).Wait();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                var engines = Engines();
                var output = new Output(engines);
                await output.Find(args);
                foreach (var result in output.PrintResultsByWord())
                {
                    Console.WriteLine(result);
                }
                foreach (var result in output.PrintWinnerByEngine())
                {
                    Console.WriteLine(result);
                }

                Console.WriteLine(output.PrintTotalWinner());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

        }

        static IEngine[] Engines()
        {
            var googleApiUrl = ConfigurationManager.AppSettings["GoogleApiUrl"];
            var googleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"];
            var googleApiContext = ConfigurationManager.AppSettings["GoogleApiContext"];
            var googleEngine = new GoogleEngine(googleApiUrl, googleApiKey, googleApiContext);

            var bingApiUrl = ConfigurationManager.AppSettings["BingApiUrl"];
            var bingApiKey = ConfigurationManager.AppSettings["BingApiKey"];
            //todo validate keys
            var bingEngine = new BingEngine(bingApiUrl, bingApiKey);

            return new IEngine[] { googleEngine };
        }
    }
}
