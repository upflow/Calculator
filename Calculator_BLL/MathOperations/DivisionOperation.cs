using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.MathOperations
{
    public class DivisionOperation : IMathOperation
    {
        public char OperationSymbol { get; private set; }
        public PriorityEnum OperationPriority { get; private set; }

        public DivisionOperation()
        {
            this.OperationSymbol = '/';
            this.OperationPriority = PriorityEnum.Div;
        }

        public double Calculate(double operandLeft, double operandRight)
        {
            if (operandRight == 0)
                throw new DivideByZeroException();

            return operandLeft / operandRight;
        }
    }
}
