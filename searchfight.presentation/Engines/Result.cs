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
    }
}
