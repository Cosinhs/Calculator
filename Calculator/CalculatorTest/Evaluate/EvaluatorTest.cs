using System;
namespace CalculatorTest.Evaluate
{
    using NUnit.Framework;
    using CalculatorCLI.Evaluate;
    using CalculatorCLI.Lexer;
    using CalculatorCLI.Object;
    using CalculatorCLI.Parser;

    [TestFixture]
    public class EvaluatorTest
    {
        class EvalutorTestCase
        {
            public string Input { get; set; }

            public long Expected { get; set; }

            public EvalutorTestCase(string input, long expected)
            {
                Input = input;
                Expected = expected;
            }
        }

        [Test]
        public void TestEval()
        {
            var tests = new[]
            {
                    new EvalutorTestCase("1 + 2", 3),
                    new EvalutorTestCase("1 - 2 * 3", -5),
                    new EvalutorTestCase("4 * 3 / 2  + 1", 7),
                    new EvalutorTestCase("5 + 10 ^ 2 / 5", 25),
                    new EvalutorTestCase("5 * (3 + 2) / 2", 12),
                };
            foreach (var tt in tests)
            {
                var evaluted = Evaluator.Eval(new Parser(new Lexer(tt.Input)).Parse());
                var result = evaluted as IntegerObject;
                Assert.IsNotNull(result, "evalued is not IntegerObject");
                Assert.AreEqual(result.Value, tt.Expected, $"wrong evalued result. want {tt.Expected}, got {result.Value}");
            }
        }
    }
}
