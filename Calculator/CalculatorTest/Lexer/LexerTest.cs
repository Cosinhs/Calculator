namespace CalculatorTest.Lexer
{
    using NUnit.Framework;

    using CalculatorCLI.Lexer;

    using CalculatorCLI.Token;

    [TestFixture]
    public class LexerTest
    {
       [Test]
       public void TestLexerNextToken()
       {
            string input = @"1 + 2 * (12 - 6) / 3 + 2 ^ 3
+ ((4+3) *   4)
/ 4
";

            var tests = new[]
            {
                new Token {Type = TokenType.INT, Literal="1"},
                new Token {Type = TokenType.PLUS, Literal="+"},
                new Token {Type = TokenType.INT, Literal="2"},
                new Token {Type = TokenType.MULTIPLY, Literal="*"},
                new Token {Type = TokenType.LPAREN, Literal="("},
                new Token {Type = TokenType.INT, Literal="12"},
                new Token {Type = TokenType.MINUS, Literal="-"},
                new Token {Type = TokenType.INT, Literal="6"},
                new Token {Type = TokenType.RPAREN, Literal=")"},
                new Token {Type = TokenType.DIVIDE, Literal="/"},
                new Token {Type = TokenType.INT, Literal="3"},
                new Token {Type = TokenType.PLUS, Literal="+"},
                new Token {Type = TokenType.INT, Literal="2"},
                new Token {Type = TokenType.POWER, Literal="^"},
                new Token {Type = TokenType.INT, Literal="3"},
                new Token {Type = TokenType.PLUS, Literal="+"},
                new Token {Type = TokenType.LPAREN, Literal="("},
                new Token {Type = TokenType.LPAREN, Literal="("},
                new Token {Type = TokenType.INT, Literal="4"},
                new Token {Type = TokenType.PLUS, Literal="+"},
                new Token {Type = TokenType.INT, Literal="3"},
                new Token {Type = TokenType.RPAREN, Literal=")"},
                new Token {Type = TokenType.MULTIPLY, Literal="*"},
                new Token {Type = TokenType.INT, Literal="4"},
                new Token {Type = TokenType.RPAREN, Literal=")"},
                new Token {Type = TokenType.DIVIDE, Literal="/"},
                new Token {Type = TokenType.INT, Literal="4"},
                new Token {Type = TokenType.EOF, Literal=""},
            };
            Lexer lexer = new Lexer(input);
            foreach (var tt in tests)
            {
                var actualToken = lexer.NextToken();
                Assert.AreEqual(tt.Type, actualToken.Type, $"expect TokenType {tt.Type}, got {actualToken.Type}");
                Assert.AreEqual(tt.Literal, actualToken.Literal, $"expect Token's Literal {tt.Literal}, got {actualToken.Literal}");
            }
       }
    }
}
