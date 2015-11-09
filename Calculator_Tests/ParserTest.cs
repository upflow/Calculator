using System;
using Calculator_BLL.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Calculator_BLL;
using Calculator_BLL.Domain;
using Calculator_BLL.Infrastructure;

namespace Calculator_Tests
{
    [TestClass]
    public class ParserTest
    {
        private Mock<IMathSymbols> mathOperations;

        [TestInitialize]
        public void Initialize()
        {
            var operatinoSymbolList = new[] {'+', '-', '*', '/'};

            mathOperations = new Mock<IMathSymbols>();
            mathOperations.Setup(op => op.GetOperationSymbolList()).Returns(operatinoSymbolList); 
        }

        [TestMethod]
        public void IsSplitCorrect()
        {
            string exp = "-10 + 10 - (-4 + (-8))";
            string exp2 = "-223423";
            Parser target = new Parser(mathOperations.Object);

            string[] result = target.Split(exp).ToArray();
            string[] result2 = target.Split(exp2).ToArray();

            Assert.AreEqual(result[1], "+");
            Assert.AreEqual(result[5], "-4");
            Assert.AreEqual(result[7], "-8");
            Assert.AreEqual(result.Length, 9);
            Assert.IsTrue(result2.Length == 1);
            Assert.AreEqual(result2[0], exp2);
        }

        [TestMethod]
        public void IsJoinCorrect()
        {
            string exp = "-10+10-(-4+(-8))";
            string[] expArr = new[] {"-10", "+", "10", "-", "(", "-4", "+", "-8", ")" };          
            Parser target = new Parser(mathOperations.Object);

            string result = target.Join(expArr);

            Assert.AreEqual(exp, result);
        }

        [TestMethod]
        public void IsParseSimleExpressionCorrect()
        {
            string exp = "256 * (-2)";
            Parser target = new Parser(mathOperations.Object);

            OperationInfo result = target.ParseSimpleExp(exp);

            Assert.AreEqual(result.OperandLeft, 256);
            Assert.AreEqual(result.OperandRight, -2);
            Assert.AreEqual(result.OperationSymbol, '*');
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void IfSimpleExpressionNotCorrectThenException()
        {
            string exp = "256 # (-2)";
            Parser target = new Parser(mathOperations.Object);

            target.ParseSimpleExp(exp);
        }   
    }
}
