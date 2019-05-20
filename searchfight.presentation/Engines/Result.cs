using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchfight.presentation.Engines
{
    public class Result
    {
        public Result(long total)
        {
            //EngineName = engineName;
            //Word = word;
            Total = total;
        }

        public string EngineName { get; set; }
        public string Word { get; set; }
        public long Total { get; set; }

        internal static Result Create(string engineName, string word, long totalEstimatedMatches)
        {
            return new Result(totalEstimatedMatches)
            {
                EngineName = engineName,
                Word = word,
                Total = totalEstimatedMatches
            };
        }
    }
}
