using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Domain
{
    public class Calculator : ICalculator
    {
        private readonly IOperations mathOperations;
        private readonly IExpression expressionFinder;

        public Calculator(IOperations mathOperations, IExpression expressionFinder)
        {
            this.mathOperations = mathOperations;
            this.expressionFinder = expressionFinder;
        }

        public string Calculate(string expression)
        {
            return CalculateExpression(expression);
        }

        /// <summary>
        /// Рекурсивно считает результат выражения.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string CalculateExpression(string expression)
        {
            string subExp = expressionFinder.GetFirstSubExpInBrackets(expression);
            if (subExp != expression)
            {
                string result = CalculateSubExpression(subExp);
                expression = expressionFinder.ReplaceFirstExpInBrackets(expression, result);

                expression = CalculateExpression(expression);
            }
            else
            {
                expression = CalculateSubExpression(expression);
            }

            return expression;           
        }

        /// <summary>
        /// Рекурсивно считает результат выражения без скобок (за исключением отрицательных чисел).
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string CalculateSubExpression(string expression)
        {
            OperationInfo simpleExp = expressionFinder.FindFirstOperationByPriority(expression);
            if (simpleExp.OperationSymbol != default(char))
            {
                double result = mathOperations
                    .Calculate(simpleExp.OperandLeft, simpleExp.OperandRight, simpleExp.OperationSymbol);
                expression = expressionFinder.ReplaceFirstOperationByPriority(expression, result.ToString());

                expression = CalculateSubExpression(expression);
            }
            return expression;
        }
    }
}
