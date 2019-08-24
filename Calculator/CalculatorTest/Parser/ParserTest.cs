namespace CalculatorTest.Parser
{

    using NUnit.Framework;

    using CalculatorCLI.Parser;
    using CalculatorCLI.Lexer;

    [TestFixture]
    public class ParserTest
    {
        class ParserTestCase
        {
            public string Input { get; set; }

            public string Expected { get; set; }

            public ParserTestCase(string input, string expected)
            {
                Input = input;
                Expected = expected;
            }
        }

        [Test]
        public void TestParser()
        {
            var tests = new []
            {
                new ParserTestCase("1+ 2", "(1 + 2)"),
                new ParserTestCase("1 + 2 - 3", "((1 + 2) - 3)"),
                new ParserTestCase("10 - 12 * 12", "(10 - (12 * 12))"),
                new ParserTestCase("16 / 4 ^ 2", "(16 / (4 ^ 2))"),
                new ParserTestCase("(5 - 2) * 3", "((5 - 2) * 3)"),
                new ParserTestCase(" 5 +((12 + 8) / 4))", "(5 + ((12 + 8) / 4))")
            };
            foreach(var tt in tests)
            {
                var parser = new Parser(new Lexer(tt.Input));
                var expression = parser.Parse();
                Assert.AreEqual(tt.Expected, expression.ToString(), $"Expression want = {tt.Expected}, but got {expression.ToString()}");
            }
        }
    }
}
