using System;
using System.Collections.Generic;
using System.Linq;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Domain
{
    public class Expression : IExpression
    {
        private readonly IParser parser;
        private readonly IPriorityOperations operations;

        public Expression(IParser parser, IPriorityOperations operations)
        {
            this.parser = parser;
            this.operations = operations;
        }

        /// <summary>
        /// Находит в строке выражение в скобках (кроме отрицательных чисел) и возвращает его индекс.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private int FindFirstExpressionInBracketsIndex(IEnumerable<string> expression)
        {
            return expression.ToList().FindLastIndex(m => m == Brackets.OpenBracket.ToString());              
        }

        /// <summary>
        /// Находит в строке выражение в скобках (кроме отрицательных чисел) и возвращает его.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string GetFirstSubExpInBrackets(string expression)
        {
            List<string> splitExp = parser.Split(expression).ToList();

            int openBracketIndex = FindFirstExpressionInBracketsIndex(splitExp);
            if (openBracketIndex == -1) return expression;

            int closeBracketIndex = splitExp.FindIndex(openBracketIndex, m => m == Brackets.CloseBracket.ToString());
            if (closeBracketIndex == -1)
                throw new FormatException("Отсутствует закрывающая скобка!");

            List<string> result = splitExp.GetRange(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1);

            return parser.Join(result);
        }

        /// <summary>
        /// Заменяет выражение в скобках на указанное значение и возвращает рузультирующую строку.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ReplaceFirstExpInBrackets(string expression, string value)
        {
            List<string> splitExp = parser.Split(expression).ToList();
            int openBracketIndex = FindFirstExpressionInBracketsIndex(splitExp);
            if (openBracketIndex == -1) return expression;

            int closeBracketIndex = splitExp.FindIndex(openBracketIndex, m => m == Brackets.CloseBracket.ToString());
            if (closeBracketIndex == -1)
                throw new FormatException("Отсутствует закрывающая скобка!");

            splitExp.RemoveRange(openBracketIndex, closeBracketIndex - openBracketIndex + 1);
            splitExp.Insert(openBracketIndex, value);

            return parser.Join(splitExp);
        }

        /// <summary>
        /// Используя приоритеты мат. операций находит в строке первое выражение и вырезает его индекс.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private int FindFirstOperationIndexByPriority(IEnumerable<string> expression)
        {
            int resultIndex = -1;
            List<string> splitExp = expression.ToList();

            var priorityCollection = operations.GetPriorityCollections().OrderByDescending(p => p);
            foreach (var priority in priorityCollection)
            {
                char[] operationSymbolList = operations.GetOperationSymbolListByPriority(priority).ToArray();
                var operationIndex = splitExp.FindIndex(m => m.Length == 1 && m.IndexOfAny(operationSymbolList) != -1);
                if (operationIndex != -1)
                {
                    resultIndex = operationIndex;
                    break;
                }
            }
            return resultIndex;
        }

        /// <summary>
        /// Используя приоритеты мат. операций находит в строке первое выражение и вырезает его.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OperationInfo FindFirstOperationByPriority(string expression)
        {
            string[] splitExp = parser.Split(expression).ToArray();
            int operationIndex = FindFirstOperationIndexByPriority(splitExp);
            if (operationIndex != -1)
            {           
                var operandLeft = splitExp[operationIndex - 1];
                var operationSymbol = splitExp[operationIndex];
                var operandRight = splitExp[operationIndex + 1];
                string simpleExp = parser.Join(new[] { operandLeft, operationSymbol, operandRight });

                return parser.ParseSimpleExp(simpleExp);                
            }

            return new OperationInfo();
        }

        /// <summary>
        /// Используя приоритеты мат. операций заменяет в строке первое выражение на указанное значение
        /// и возвращает результирующую строку.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ReplaceFirstOperationByPriority(string expression, string value)
        {       
            List<string> splitExp = parser.Split(expression).ToList();
            int operationIndex = FindFirstOperationIndexByPriority(splitExp);
            if (operationIndex != -1)
            {             
                splitExp[operationIndex] = value;
                splitExp.RemoveAt(operationIndex + 1);
                splitExp.RemoveAt(operationIndex - 1);

                return parser.Join(splitExp);
            }

            return expression;
        }
    }
}
