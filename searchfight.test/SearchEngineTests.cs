using searchfight.presentation;
using searchfight.presentation.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace searchfight.test
{
    public class SearchEngineTests : IDisposable
    {
        private IEngine engine;
        public SearchEngineTests()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData("c#")]        
        public void MustAInstanceOfResult(string word)
        {
            var result = engine.Find(word);

            Assert.IsType<Result>(result);
        }
    }
}
