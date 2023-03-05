using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Evaluator
    {
        private Dictionary<string, int> operatorsToPriority = 
            new Dictionary<string, int>() { { "+", 0 }, { "-", 0 }, { "*", 1}, { "/", 1} };

        private Dictionary<string, Func<double, double, double>> operatorsToFunc =
            new Dictionary<string, Func<double, double, double>>()
            {
                {"+", (x, y) => x + y},
                {"-", (x, y) => y - x},
                {"*", (x, y) => x * y },
                {"/", (x, y) => y / x },
            };

        public double Evaluate(string expression)
        {
            List<string> postfixEspression = InfixToPostfix(expression);
            var stack = new Stack<double>();
            foreach(var token in postfixEspression)
            {
                double num = 0;
                if (double.TryParse(token, out num))
                    stack.Push(num);
                else
                    stack.Push(operatorsToFunc[token](stack.Pop(), stack.Pop()));
            }
            return stack.Pop();
        }

        public List<string> InfixToPostfix(string infixExpression)
        {
            string[] tokens = infixExpression.Split(" ");
            var result = new List<string>();
            var stack = new Stack<string>();
            foreach(string token in tokens)
            {
                //if is number
                if (double.TryParse(token, out _))
                    result.Add(token);
                else if (operatorsToPriority.ContainsKey(token))
                {
                    while(stack.Count > 0 && operatorsToPriority[stack.Peek()] >= operatorsToPriority[token])
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
                else
                    throw new ArgumentException("Invalid infix expression");
            }
            foreach (var item in stack)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
