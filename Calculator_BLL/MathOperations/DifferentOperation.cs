using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.MathOperations
{
    public class DifferentOperation : IMathOperation
    {
        public char OperationSymbol { get; private set; }
        public PriorityEnum OperationPriority { get; private set; }

        public DifferentOperation()
        {
            this.OperationSymbol = '-';
            this.OperationPriority = PriorityEnum.Dif;
        }

        public double Calculate(double operandLeft, double operandRight)
        {
            return operandLeft - operandRight;
        }
    }
}
