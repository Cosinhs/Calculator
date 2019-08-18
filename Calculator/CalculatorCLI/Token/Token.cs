namespace CalculatorCLI.Token
{
    /// <summary>
    /// TokenType
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Integer
        /// </summary>
        INT,

        /// <summary>
        /// Plus +
        /// </summary>
        PLUS,

        /// <summary>
        /// Minus -
        /// </summary>
        MINUS,

        /// <summary>
        /// Multiply *
        /// </summary>
        MULTIPLY,

        /// <summary>
        /// Divide /
        /// </summary>
        DIVIDE,

        /// <summary>
        /// Power ^
        /// </summary>
        POWER,

        /// <summary>
        /// left parenthesis "("
        /// </summary>
        LPAREN,

        /// <summary>
        /// right parenthesis ")"
        /// </summary>
        RPAREN,

        /// <summary>
        /// Eof (end of file)
        /// </summary>
        EOF,

        /// <summary>
        /// Illegal token
        /// </summary>
        ILLEGAL,
    }

    /// <summary>
    /// Token 
    /// </summary>
    public class Token
    {
        /// <summary>
        /// TokenType.
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        /// Token's literal value.
        /// </summary>
        public string Literal { get; set; }
    }
}
