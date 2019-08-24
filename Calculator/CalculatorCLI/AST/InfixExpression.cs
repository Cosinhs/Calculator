namespace CalculatorCLI.AST
{
    using Token;
    public class InfixExpression : Expression
    {
        public Expression Left { get; set; }

        public Expression Right { get; set; }

        public Token Token { get; set; }

        public string Operator { get; set; }

        public override string TokenLiteral()
        {
            return Token.Literal;
        }


        public override string ToString()
        {
            return $"({Left.ToString()} {Token.Literal} {Right.ToString()})";
        }
    }
}
