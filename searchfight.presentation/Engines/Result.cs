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
            this.Total = total;
        }

        public long Total { get; set; }
    }
}
