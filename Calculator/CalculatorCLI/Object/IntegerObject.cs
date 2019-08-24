namespace CalculatorCLI.Object
{
    public class IntegerObject : Object
    {

        public long Value { get; set; }



        public override string Inspect()
        {
            return $"{Value}";
        }

        public override ObjectType Type()
        {
            return ObjectType.INTEGER;
        }
    }
}
