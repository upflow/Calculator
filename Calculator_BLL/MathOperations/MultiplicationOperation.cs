using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.MathOperations
{
    public class MultiplicationOperation : IMathOperation
    {
        public char OperationSymbol { get; private set; }
        public PriorityEnum OperationPriority { get; private set; }

        public MultiplicationOperation()
        {
            this.OperationSymbol = '*';
            this.OperationPriority = PriorityEnum.Mult;
        }

        public double Calculate(double operandLeft, double operandRight)
        {
            return operandLeft * operandRight;
        }
    }
}
