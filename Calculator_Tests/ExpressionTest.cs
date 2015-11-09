using System;
using System.Linq;
using Calculator_BLL;
using Calculator_BLL.Abstract;
using Calculator_BLL.Domain;
using Calculator_BLL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator_Tests
{
    [TestClass]
    public class OperationTest
    {
        private Mock<IPriorityOperations> operationsMock;
        private Mock<IParser> parserMock;
        private string exp;
        private string[] splitExp;
        private string subExp;
        private string[] splitSubExp;
        private string simpleExp;
        private string[] splitSimpleExp;

        [TestInitialize]
        public void Initialize()
        {
            var operatinoSymbolList = new[] { '+', '-', '*', '/' };

            operationsMock = new Mock<IPriorityOperations>();
            operationsMock.Setup(op => op.GetOperationSymbolList()).Returns(operatinoSymbolList);
            operationsMock.Setup(n => n.GetOperationSymbolListByPriority(10)).Returns(new[] { '+', '-' });
            operationsMock.Setup(n => n.GetOperationSymbolListByPriority(20)).Returns(new[] { '*', '/' });
            operationsMock.Setup(m => m.GetPriorityCollections()).Returns(new byte[] {10, 20});

            exp = "-10+10-(-4*(-8)+12)";
            splitExp = new[] { "-10", "+", "10", "-", "(", "-4", "*", "-8", "+", "12", ")" };
            subExp = "-4*(-8)+12";
            splitSubExp = new[] { "-4", "*", "-8", "+", "12" };
            simpleExp = "-4*(-8)";
            splitSimpleExp = new[] {"-4", "*", "-8"};

            parserMock = new Mock<IParser>();
            parserMock.Setup(s => s.Split(exp)).Returns(splitExp);
            parserMock.Setup(s => s.Split(subExp)).Returns(splitSubExp);
            parserMock.Setup(s => s.Split(simpleExp)).Returns(splitSimpleExp);
            parserMock.Setup(j => j.Join(splitExp)).Returns(exp);
            parserMock.Setup(j => j.Join(splitSubExp)).Returns(subExp);
            parserMock.Setup(j => j.Join(splitSimpleExp)).Returns(simpleExp);

            parserMock.Setup(p => p.ParseSimpleExp(simpleExp)).Returns(
                new OperationInfo(
                    double.Parse(splitSimpleExp[0]), 
                    double.Parse(splitSimpleExp[2]), 
                    splitSimpleExp[1][0]));
        }

        [TestMethod]
        public void IsFirstSubExpInBracketsCorrect()
        {
            Expression target = new Expression(parserMock.Object, operationsMock.Object);

            string result = target.GetFirstSubExpInBrackets(exp);

            Assert.AreEqual(result, subExp);
        }

        [TestMethod]
        public void FindFirstOperationByPriorityCorrect()
        {
            Expression target = new Expression(parserMock.Object, operationsMock.Object);

            OperationInfo result = target.FindFirstOperationByPriority(subExp);

            Assert.AreEqual(result.OperandLeft, -4);
            Assert.AreEqual(result.OperandRight, -8);
            Assert.IsTrue(result.OperationSymbol == '*');
        }


        [TestMethod]
        public void IsReplaceFirstOperationByPriorityCorrect()
        {
            string subExpFirstResult = "32+12";
            string[] splitSubExpFirstResult = new[] { "32", "+", "12" };
            parserMock.Setup(j => j.Join(splitSubExpFirstResult)).Returns(subExpFirstResult);

            Expression target = new Expression(parserMock.Object, operationsMock.Object);

            string result = target.ReplaceFirstOperationByPriority(subExp, "32");

            Assert.AreEqual(result, subExpFirstResult);
        }

        [TestMethod]
        public void IsReplaceFirstExpInBracketsCorrect()
        {
            string expFirsResult = "-10+10-44";
            string[] splitExpFirstResult = new[] { "-10", "+", "10", "-", "44" };
            parserMock.Setup(j => j.Join(splitExpFirstResult)).Returns(expFirsResult);
            Expression target = new Expression(parserMock.Object, operationsMock.Object);

            string result = target.ReplaceFirstExpInBrackets(exp, "44");

            Assert.AreEqual(result, expFirsResult);
        }
    }
}
