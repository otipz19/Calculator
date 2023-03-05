namespace CalculatorTests
{
    [TestClass]
    public class EvaluatorTests
    {
        private Evaluator evaluator;

        [TestInitialize]
        public void Initialize()
        {
            evaluator = new Evaluator();
        }

        [TestMethod]
        public void InfixToPostfix()
        {
            string infix = "1 + 2 - 3";
            string postfix = "1 2 + 3 -";
            Assert.AreEqual(postfix, string.Join(' ', evaluator.InfixToPostfix(infix)));
        }

        [TestMethod]
        public void Evaluate()
        {
            string expression = "1 + 2 - 3";
            Assert.AreEqual(0, evaluator.Evaluate(expression));
        }

        [TestMethod]
        public void Evaluate1()
        {
            string expression = "1 + 2 * 3";
            Assert.AreEqual(7, evaluator.Evaluate(expression));
        }

        [TestMethod]
        public void Evaluate2()
        {
            string expression = "1 + 4 / 2";
            Assert.AreEqual(3, evaluator.Evaluate(expression));
        }
    }
}
