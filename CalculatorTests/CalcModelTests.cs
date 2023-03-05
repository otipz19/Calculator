namespace CalculatorTests
{
    [TestClass]
    public class CalcModelTests
    {
        private CalcModel calcModel;
        private string expression;
        private string curOperand;

        [TestInitialize]
        public void Initialize()
        {
            calcModel = new CalcModel();
            calcModel.ExpressionChanged += (str) => expression = str;
            calcModel.CurOperandChanged += (str) => curOperand = str;
        }

        private void TestCurOperandAndExpression(string curOperand, string expression)
        {
            Assert.AreEqual(expression, calcModel.Expression);
            Assert.AreEqual(curOperand, calcModel.CurOperand);
        }

        [TestMethod]
        public void AddSymbolToEmptyCurOperandWithNoExpression()
        {
            calcModel.AddSymbol("1");
            TestCurOperandAndExpression("1", "");
        }

        [TestMethod]
        public void AddSymbolToNotEmptyCurOperandWithNoExpression()
        {
            calcModel.AddSymbol("12345");
            TestCurOperandAndExpression("12345", "");
            calcModel.AddSymbol("a");
            TestCurOperandAndExpression("12345a", "");
        }

        [TestMethod]
        public void AddBinaryOperatorWithNoExpressionAndNoCurOperand()
        {
            calcModel.AddBinaryOperator("+");
            TestCurOperandAndExpression("0", "0");
        }

        [TestMethod]
        public void AddBinaryOperatorWithNotEmptyCurOperand()
        {
            for (int i = 1; i <= 5; i++)
            {
                calcModel.AddSymbol(i.ToString());
            }
            calcModel.AddBinaryOperator("+");
            for (int i = 0; i <= 5; i++)
            {
                calcModel.AddSymbol(i.ToString());
            }
            calcModel.GetResult();
            TestCurOperandAndExpression($"{12345 * 2}", "");
        }

        [TestMethod]
        public void ChangeOperator()
        {
            for (int i = 1; i <= 5; i++)
            {
                calcModel.AddSymbol(i.ToString());
            }
            calcModel.AddBinaryOperator("*");
            calcModel.AddBinaryOperator("/");
            calcModel.AddBinaryOperator("+");
            calcModel.AddBinaryOperator("-");
            for (int i = 0; i <= 5; i++)
            {
                calcModel.AddSymbol(i.ToString());
            }
            calcModel.GetResult();
            TestCurOperandAndExpression("0", "");
        }

        [TestMethod]
        public void ClearOneSymbol()
        {
            calcModel.AddSymbol("1234");
            calcModel.ClearOneSymbol();
            Assert.AreEqual("123", calcModel.CurOperand);
        }
    }
}