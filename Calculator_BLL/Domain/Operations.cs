using System;
using System.Collections.Generic;
using System.Linq;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Domain
{
    public class Operations : IOperations, IPriorityOperations
    {
        private readonly IEnumerable<IMathOperation> operationsList;

        public Operations(IEnumerable<IMathOperation> operationsList)
        {
            this.operationsList = operationsList;
        }

        /// <summary>
        /// Получает список приоритетов мат. операций (Указывается в перечислении PriorityEnum)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<byte> GetPriorityCollections()
        {
            var priorityCollections =
                Enum.GetNames(typeof(PriorityEnum))
                    .Select(p => (byte)Enum.Parse(typeof(PriorityEnum), p))
                    .Distinct()
                    .OrderBy(p => p);

            return priorityCollections;
        }

        /// <summary>
        /// Возвращает коллекцию всех мат. символов которые присутствуют в списке операций
        /// </summary>
        /// <returns></returns>
        public IEnumerable<char> GetOperationSymbolList()
        {
            return operationsList.Select(op => op.OperationSymbol).ToArray();
        }

        /// <summary>
        /// Возвращает коллекцию мат. символов которые имеют указанный приоритет
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public IEnumerable<char> GetOperationSymbolListByPriority(byte priority)
        {
            var operationSymbolCollection =
                operationsList.Where(op => (byte)op.OperationPriority == priority)
                    .Select(s => s.OperationSymbol);

            return operationSymbolCollection;
        }

        /// <summary>
        /// Считает простое выражение
        /// </summary>
        /// <param name="firstOperand"></param>
        /// <param name="secondOperand"></param>
        /// <param name="operationSymbol"></param>
        /// <returns></returns>
        public double Calculate(double firstOperand, double secondOperand, char operationSymbol)
        {
            IMathOperation mathOperation = operationsList.FirstOrDefault(op => op.OperationSymbol == operationSymbol);
            if (mathOperation == null)
                throw new ArgumentException("Математическая операция не найдена");

            return mathOperation.Calculate(firstOperand, secondOperand);
        }
    }
}
