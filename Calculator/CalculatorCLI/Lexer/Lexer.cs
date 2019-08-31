namespace CalculatorCLI.Lexer
{
    using Token;
    public class Lexer
    {
        private readonly string input;
        private int position;

        public Lexer(string input)
        {
            this.input = input;
            position = 0;
        }

        public Token NextToken()
        {
            SkipWhitespace();
            if (position >= input.Length)
            {
                return new Token { Type = TokenType.EOF, Literal = "" };
            }

            char character = input[position];
            Token token;
            switch (character)
            {
                case '+':
                    token = new Token { Type = TokenType.PLUS, Literal = "+" };
                    break;
                case '-':
                    token = new Token { Type = TokenType.MINUS, Literal = "-" };
                    break;
                case '*':
                    token = new Token { Type = TokenType.MULTIPLY, Literal = "*" };
                    break;
                case '/':
                    token = new Token { Type = TokenType.DIVIDE, Literal = "/" };
                    break;
                case '^':
                    token = new Token { Type = TokenType.POWER, Literal = "^" };
                    break;
                case '(':
                    token = new Token { Type = TokenType.LPAREN, Literal = "(" };
                    break;
                case ')':
                    token = new Token { Type = TokenType.RPAREN, Literal = ")" };
                    break;
                default:
                    if (IsDigit(character))
                    {
                        token = new Token { Type = TokenType.INT };
                        token.Literal = ReadNumber();

                    }
                    else
                    {
                        token = new Token { Type = TokenType.ILLEGAL, Literal = $"{character}" };
                    }
                    break;
            }
            position++;
            return token;

        }

        private void SkipWhitespace()
        {
            while(position < input.Length && IsWhitespace(input[position]))
            {
                position++;
            }
        }


        private string ReadNumber()
        {
            int position = this.position;
            while(this.position < input.Length && IsDigit(input[this.position]))
            {
                this.position++;
            }
            string number = input.Substring(position, this.position - position);
            this.position--;
            return number;
        }


        private static bool IsDigit(char character)
        {
            return character >= '0' && character <= '9';
        }


        private static bool IsWhitespace(char character)
        {
            return ' ' == character || '\t' == character || '\n' == character || '\r' == character;
        }

    }
}
