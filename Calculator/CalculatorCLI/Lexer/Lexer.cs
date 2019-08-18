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
            this.position = 0;
        }

        public Token NextToken()
        {
            this.SkipWhitespace();
            if (this.position >= this.input.Length)
            {
                return new Token { Type = TokenType.EOF, Literal=""};
            }
            Token token = null;
            char character = this.input[this.position];
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
                    token = new Token { Type = TokenType.RPAREN, Literal = ")"};
                    break;
                default:
                    if(IsDigit(character))
                    {
                        token = new Token { Type = TokenType.INT };
                        token.Literal = this.ReadNumber();
                        
                    }
                    else
                    {
                        token = new Token { Type = TokenType.ILLEGAL, Literal=""};
                    }
                    break;
            }
            this.position++;
            return token;

        }

        private void SkipWhitespace()
        {
            while(this.position < this.input.Length && IsWhitespace(this.input[this.position]))
            {
                this.position++;
            }
        }


        private string ReadNumber()
        {
            int position = this.position;
            while(this.position < this.input.Length && IsDigit(this.input[this.position]))
            {
                this.position++;
            }
            string number = this.input.Substring(position, this.position - position);
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
