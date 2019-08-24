namespace CalculatorCLI.AST
{
    using Token;
    public class PrefixExpression : Expression
    {
        public Token Token { get; set; }

        public Expression Right { get; set; }

        public string Operator { get; set; }

        public override string TokenLiteral()
        {
            return Token.Literal;
        }

        public override string ToString()
        {
            return $"({Token.Literal} {Right.ToString()})";
        }
    }
}
