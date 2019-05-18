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
    public class OutputTest
    {
        private const string wordDotnet = ".net";
        private const string wordJava = "java";

        public OutputTest()
        { }

        [Fact]
        public async Task MustMatchOutpuResultForOneEngineAndOneWord()
        {
            var googleEngine = BuildGoogleEngine();

            var sut = new Output(new[] { googleEngine });
            await sut.Find(new[] { wordDotnet });

            var result = sut.PrintResultsByWord();

            var expected = $"{wordDotnet}: {googleEngine.Name}: 4450000000";
            Assert.Equal(expected, result[0]);
        }

        [Fact]
        public async Task MustMatchOutputResultWithManySearchEngines()
        {
            var bingEngine = BuildBingEngine();
            var googleEngine = BuildGoogleEngine();

            var sut = new Output(new[] { googleEngine, bingEngine });
            await sut.Find(new[] { wordJava });

            var result = sut.PrintResultsByWord();
            var expected = $"{wordJava}: {googleEngine.Name}: 966000000 {bingEngine.Name}: 94381485";
            Assert.Equal(expected, result[0]);
        }


        [Fact]
        public async Task MustMatchResultForManyWordsAndManyBrownsers()
        {
            var bingEngine = BuildBingEngine();
            var googleEngine = BuildGoogleEngine();

            var sut = new Output(new[] { googleEngine, bingEngine });
            await sut.Find(new[] { wordDotnet, wordJava });

            var results = sut.PrintResultsByWord();
            var expectedFirstLine = $"{wordDotnet}: {googleEngine.Name}: 4450000000 {bingEngine.Name}: 12354420";
            var expectedSecondLine = $"{wordJava}: {googleEngine.Name}: 966000000 {bingEngine.Name}: 94381485";

            Assert.Equal(expectedFirstLine, results[0]);
            Assert.Equal(expectedSecondLine, results[1]);
        }

        [Fact]
        public async Task MustMatchDashBoard()
        {
            var bingEngine = BuildBingEngine();
            var googleEngine = BuildGoogleEngine();

            var sut = new Output(new[] { googleEngine, bingEngine });
            await sut.Find(new[] { wordDotnet, wordJava });
            var results = sut.PrintWinnerByEngine();


            var expectedFirstLine = $"{googleEngine.Name} winner: {wordDotnet}";
            var expectedSecondLine = $"{bingEngine.Name} winner: {wordJava}";

            Assert.Equal(expectedFirstLine, results[0]);
            Assert.Equal(expectedSecondLine, results[1]);
        }

        [Fact]
        public async Task MustMatchFinalResult()
        {
            var bingEngine = BuildBingEngine();
            var googleEngine = BuildGoogleEngine();

            var sut = new Output(new[] { googleEngine, bingEngine });
            await sut.Find(new[] { wordDotnet, wordJava });
            var results = sut.PrintTotalWinner();

            var expectedFirstLine = $"Total winner: {wordDotnet}";

            Assert.Equal(expectedFirstLine, results);
        }


        private IEngine BuildGoogleEngine()
        {
            var mockGoogle = new Mock<IEngine>();
            mockGoogle.Setup(e => e.Name).Returns("Google");
            mockGoogle.Setup(e => e.Find(wordJava)).ReturnsAsync(new Result(966000000)
            {
                EngineName = "Google",
                Word = wordJava
            });
            mockGoogle.Setup(e => e.Find(wordDotnet)).ReturnsAsync(new Result(4450000000)
            {
                EngineName = "Google",
                Word = wordDotnet
            });

            return mockGoogle.Object;
        }

        private IEngine BuildBingEngine()
        {

            var mock = new Mock<IEngine>();
            mock.Setup(e => e.Name).Returns("Bing");
            mock.Setup(e => e.Find(wordJava)).ReturnsAsync(new Result(94381485)
            {
                EngineName = "Bing",
                Word = wordJava
            });
            mock.Setup(e => e.Find(wordDotnet)).ReturnsAsync(new Result(12354420)
            {
                EngineName = "Bing",
                Word = wordDotnet
            });

            return mock.Object;
        }
    }
}
