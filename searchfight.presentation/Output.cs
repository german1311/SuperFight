using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using searchfight.presentation.Engines;

namespace searchfight.presentation
{
    public class Output
    {
        private IEnumerable<IEngine> _engines;

        private Dictionary<string, Result> _results;

        public Output(IEngine[] engines)
        {
            _engines = engines;
            _results = new Dictionary<string, Result>();
        }

        public string[] PrintResultsByWord()
        {
            throw new NotImplementedException();
        }

        public Task find(string[] words)
        {
            throw new NotImplementedException();
        }

        public string[] PrintWinnerByEngine()
        {
            throw new NotImplementedException();
        }

        public string PrintTotalWinner()
        {
            throw new NotImplementedException();
        }
    }
}
