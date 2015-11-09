using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Abstract
{
    public interface IMathOperation
    {
        char OperationSymbol { get; }
        PriorityEnum OperationPriority { get; }
        double Calculate(double operandLeft, double operandRight);
    }
}
