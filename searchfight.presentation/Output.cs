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

        //word - result search
        //private Dictionary<string, Result> _results;
        private List<Result> _results;
        private string[] _words;
        public Output(IEngine[] engines)
        {
            _engines = engines;
            //_results = new Dictionary<string, Result>();
            _results = new List<Result>();
        }

        public async Task Find(string[] words)
        {
            this._words = words;
            var searchs = words.ToList().SelectMany
                (w => _engines.Select(async e => await e.Find(w)));

            var results = await Task.WhenAll(searchs);
            _results = results.ToList();
            //foreach (var result in results)
            //{
            //    _results.Add(result.Word, result);
            //}
        }

        public string[] PrintResultsByWord()
        {
            var outputs = new List<string>();

            foreach (var word in _words)
            {
                var resultByWord = _results.Where(r => r.Word == word);
                var output = $"{word}:";
                foreach (var engine in _engines)
                {
                    resultByWord
                        .Where(r => r.EngineName == engine.Name)
                        .ToList()
                        .ForEach(r =>
                        {
                            output += $" {r.EngineName}: {r.Total}";                            
                        });
                }
                outputs.Add(output);
            }

            return outputs.ToArray();
        }

        public string[] PrintWinnerByEngine()
        {
            var outputs = new string[_engines.Count()];

            int index = 0;
            foreach (var engine in _engines)
            {
                var winner = _results.Where(r => r.EngineName == engine.Name).OrderByDescending(r => r.Total).FirstOrDefault();

                outputs[index] = $"{winner.EngineName} winner: {winner.Word}";
                index++;
            }
            return outputs;
        }

        public string PrintTotalWinner()
        {
            var totalWinner = _results
                .GroupBy(r => new { r.Word, r.Total },
                (g, r) => new
                {
                    Word = g.Word,
                    Total = r.Sum(q => q.Total)
                })
                .OrderByDescending(o => o.Total)
                .FirstOrDefault();

            return $"Total winner: {totalWinner.Word}";
        }
    }
}
