namespace CalculatorCLI.Parser
{
    public enum Precedence
    {
        /// <summary>
        /// the lowest 
        /// </summary>
        LOWEST = 0,

        /// <summary>
        /// precedence for +, -
        /// </summary>
        SUM = 1,

        /// <summary>
        /// precedence for * /
        /// </summary>
        PRODUCT = 2,

        /// <summary>
        /// precedence for - (as prefix)
        /// </summary>
        PREFIX = 3,

        /// <summary>
        /// precedence for power.
        /// </summary>
        POWER = 4,

        /// <summary>
        /// precedence for (
        /// </summary>
        GROUP = 5,
    }
}
