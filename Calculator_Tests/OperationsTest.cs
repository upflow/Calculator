using System;
using System.Collections.Generic;
using Calculator_BLL;
using Calculator_BLL.Abstract;
using Calculator_BLL.MathOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Calculator_BLL.Domain;

namespace Calculator_Tests
{
    [TestClass]
    public class OperationsTest
    {
        private IList<IMathOperation> operationsList;

        [TestInitialize]
        public void Initialize()
        {
            operationsList = new List<IMathOperation>()
            {
                new SumOperation(),
                new DifferentOperation(),
                new MultiplicationOperation(),
                new DivisionOperation()
            };
        }

        [TestMethod]
        public void IsOperationSymbolListCorrect()
        {
            var symbols = new[] {'+', '-', '*', '/'};
            Operations target = new Operations(operationsList);

            var result = target.GetOperationSymbolList();           

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Except(symbols).Any());
        }

        [TestMethod]
        public void IsOperationSymbolsListByPriorityCorrect()
        {
            var symbols = new[] { '*', '/' };
            Operations target = new Operations(operationsList);

            var result = target.GetOperationSymbolListByPriority(20);
            var result2 = target.GetOperationSymbolListByPriority(211);

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Except(symbols).Any());
            Assert.IsTrue(!result2.Any());
        }

        [TestMethod]
        public void IsCalculateCorrect()
        {
            Operations target = new Operations(operationsList);

            var result1 = target.Calculate(2.5, 4, '*');
            var result2 = target.Calculate(12, 4, '/');
            var result3 = target.Calculate(2.5, 4.3, '+');
            var result4 = target.Calculate(2.5, 4, '-');

            Assert.AreEqual(result1, 10.0);
            Assert.AreEqual(result2, 3.0);
            Assert.AreEqual(result3, 6.8);
            Assert.AreEqual(result4, -1.5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfOperationSymbolNotExistThenException()
        {
            Operations target = new Operations(operationsList);

            target.Calculate(2.5, 4, '#'); 
        }
    }
}
