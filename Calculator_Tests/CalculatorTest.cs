using System;
using Calculator_BLL.Abstract;
using Calculator_BLL.Domain;
using Calculator_BLL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator_Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void IsCalculateSubExpressionCorrect()
        {
            string exp = "-10 + 10 * (-3) / 2";
            string exp2 = "-10 + (-30) / 2";
            string exp3 = "-10 + (-15)";
            string expResult = "-25";

            Mock<IOperations> operationsMock = new Mock<IOperations>();

            Mock<IExpression> expressionMock = new Mock<IExpression>();
            expressionMock.Setup(e => e.FindFirstOperationByPriority(exp)).Returns(new OperationInfo(10, -3, '*'));
            expressionMock.Setup(e => e.FindFirstOperationByPriority(exp2)).Returns(new OperationInfo(-30, 2, '/'));
            expressionMock.Setup(e => e.FindFirstOperationByPriority(exp3)).Returns(new OperationInfo(-10, -15, '+'));

            expressionMock.Setup(e => e.ReplaceFirstOperationByPriority(exp, It.IsAny<string>())).Returns(exp2);
            expressionMock.Setup(e => e.ReplaceFirstOperationByPriority(exp2, It.IsAny<string>())).Returns(exp3);
            expressionMock.Setup(e => e.ReplaceFirstOperationByPriority(exp3, It.IsAny<string>())).Returns(expResult);

            Calculator target = new Calculator(operationsMock.Object, expressionMock.Object);
            string result = target.CalculateSubExpression(exp);

            Assert.AreEqual(result, expResult);
        }
    }
}
