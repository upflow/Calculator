using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_BLL.Abstract
{
    public interface IOperations
    {
        double Calculate(double firstOperand, double secondOperand, char operationSymbol);
    }
}
