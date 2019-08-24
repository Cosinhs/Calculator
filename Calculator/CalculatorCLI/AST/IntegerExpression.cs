using System;
namespace CalculatorCLI.AST
{
    using Token;
    public class IntegerExpression : Expression
    {

        public Token Token { get; set; }

        public long Value { get; set; }

        public override string TokenLiteral()
        {
            return this.Token.Literal;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
