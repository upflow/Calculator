using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;

namespace Calculator_BLL.Infrastructure
{
    /// <summary>
    /// Структура. Хранит данные мат. операции
    /// </summary>
    public struct OperationInfo
    {
        public double OperandLeft;
        public double OperandRight;
        public char OperationSymbol;

        public OperationInfo(double operandLeft, double operandRight, char operationSymbol)
        {
            OperandLeft = operandLeft;
            OperandRight = operandRight;
            OperationSymbol = operationSymbol;
        }
    }
}
