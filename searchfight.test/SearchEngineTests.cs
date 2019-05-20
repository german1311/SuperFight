using Moq;
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
            var mock = new Mock<IEngine>();
            mock.Setup(e => e.FindAsync(It.IsAny<string>())).ReturnsAsync(new Result(94381485));
            engine = mock.Object;
        }

        public void Dispose()
        {
            engine = null;
        }

        //todo: improve or delete test
        [Theory]
        [InlineData("c#")]        
        public async Task MustAInstanceOfResult(string word)
        {
            var result = await engine.FindAsync(word);

            Assert.IsType<Result>(result);
        }
    }
}
