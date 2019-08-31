using System;
namespace CalculatorCLI.Evaluate
{
    using AST;
    public class Evaluator
    {
        public static Object.Object Eval(Expression expression)
        {
            if(expression is IntegerExpression)
            {
                return new Object.IntegerObject { Value = ((IntegerExpression)expression).Value };
            }
            if(expression is PrefixExpression)
            {
                return EvalPrefixExpression(expression as PrefixExpression);
            }
            if(expression is InfixExpression)
            {
                return EvalInfixExpression(expression as InfixExpression);
            }
            throw new Exception($"Unsupported expression {expression.TokenLiteral()}");
        }

        public static Object.Object EvalPrefixExpression(PrefixExpression expression)
        {
            Object.Object right = Eval(expression.Right);
            Object.IntegerObject rightInteger = right as Object.IntegerObject;
            if(rightInteger == null)
            {
                throw new Exception("Prefix expression right is not Object.IntegerObject");
            }
            return new Object.IntegerObject { Value = -1 * rightInteger.Value };
        }

        public static Object.Object EvalInfixExpression(InfixExpression expression)
        {
            Object.Object left = Eval(expression.Left);
            Object.IntegerObject leftInteger = left as Object.IntegerObject;
            if(leftInteger == null)
            {
                throw new Exception("Infix expression left branch is not Object.IntegerObject");
            }
            Object.Object right = Eval(expression.Right);
            Object.IntegerObject rightInteger = right as Object.IntegerObject;
            if(rightInteger == null)
            {
                throw new Exception("Infix expression right branch is not Object.IntegerObject");
            }
            return InfixExpression(leftInteger, expression.Operator, rightInteger);
        }
        public static Object.Object InfixExpression(Object.IntegerObject left, string op, Object.IntegerObject right)
        {
            if(op == "+")
            {
                return new Object.IntegerObject { Value = left.Value + right.Value };
            }
            if(op == "-")
            {
                return new Object.IntegerObject { Value = left.Value - right.Value };
            }
            if(op == "*")
            {
                return new Object.IntegerObject { Value = left.Value * right.Value };
            }
            if(op == "/")
            {
                return new Object.IntegerObject { Value = left.Value / right.Value };
            }
            if(op == "^")
            {
                return new Object.IntegerObject { Value = (long)Math.Pow((double)left.Value, (double)right.Value) };
            }
            throw new Exception($"Unsupported arithmetic operator {op}");
        }
    }
}
