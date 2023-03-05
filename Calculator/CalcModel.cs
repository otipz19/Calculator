using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalcModel
    {
        private Evaluator evaluator = new Evaluator();

        public string Expression { get; private set; } = "";
        private string lastOperator;
        private bool isLastInputOperator;

        public string CurOperand { get; private set; } = "0";
        private bool isCurOperandResult = true;

        public event Action<string> ExpressionChanged;
        public event Action<string> CurOperandChanged;

        public void AddSymbol(string symbol)
        {
            isLastInputOperator = false;
            if (isCurOperandResult)
            {
                isCurOperandResult = false;
                CurOperand = symbol;
            }
            else
            {
                CurOperand += symbol;
            }
            CurOperandChanged(CurOperand);
        }

        public void AddBinaryOperator(string op)
        {
            if(!isLastInputOperator)
            {
                isLastInputOperator = true;
                if (Expression == "")
                {
                    Expression = $"{CurOperand}";
                }
                else
                {
                    Expression = CombineExpression();
                }
                TryEvaluateExpression();
                CurOperandChanged(CurOperand);
                isCurOperandResult = true;
            }
            lastOperator = op;
            ExpressionChanged($"{Expression} {lastOperator}");
        }

        private void TryEvaluateExpression()
        {
            try
            {
                CurOperand = evaluator.Evaluate(Expression).ToString();
            }
            catch (Exception e)
            {
                Expression = "";
                CurOperand = "Invalid Expression";
            }
        }

        private string CombineExpression()
        {
            return $"{Expression} {lastOperator} {CurOperand}";
        }

        public void ClearCurOperand()
        {
            CurOperand = "0";
            CurOperandChanged(CurOperand);
        }

        public void ClearAll()
        {
            ClearCurOperand();
            Expression = "";
            ExpressionChanged(Expression);
            isLastInputOperator = false;
            lastOperator = null;
        }

        public void ClearOneSymbol()
        {
            if (CurOperand.Length > 1)
                CurOperand = CurOperand.Substring(0, CurOperand.Length - 1);
            else if (CurOperand != "0")
                CurOperand = "0";
            CurOperandChanged(CurOperand);
        }

        public void GetResult()
        {
            if(Expression != null && Expression.Length > 0)
            {
                isCurOperandResult = true;
                Expression = CombineExpression();
                TryEvaluateExpression();
                CurOperandChanged(CurOperand);
                Expression = "";
                ExpressionChanged(Expression);
            }
        }
    }
}
