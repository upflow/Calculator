using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Domain
{
    public class Parser : IParser
    {
        private readonly IMathSymbols operations;
        private const char MINUS = '-';

        public Parser(IMathSymbols operations)
        {
            this.operations = operations;
        }

        /// <summary>
        /// Удаляет все пробелы в строке.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string DeleteWhiteSpaces(string expression)
        {
            return expression.Replace(" ", "");
        }

        /// <summary>
        /// Разбивает строку на массив символов и чисел.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<string> Split(string expression)
        {
            expression = DeleteWhiteSpaces(expression);
            const string SEPARATOR = " ";

            var operationSymbolList = operations.GetOperationSymbolList();

            List<char> symbolsList = new List<char>();
            symbolsList.Add(Brackets.OpenBracket);
            symbolsList.Add(Brackets.CloseBracket);
            symbolsList.AddRange(operationSymbolList);

            char[] symbols = symbolsList.ToArray();

            int index = expression[0] == MINUS ? 1 : 0;

            while (index != -1)
            {
                index = expression.IndexOfAny(symbols, index);
                if (index != -1)
                {
                    string symbol = expression.ElementAt(index).ToString();
                    expression = expression.Remove(index, 1);
                    expression = expression.Insert(index, SEPARATOR + symbol + SEPARATOR);
                    index += 2;
                }
            }
            // Удаляем пробелы в начале и конце строки.
            expression = expression.Trim();
            // Если 2 специальных символа стоят подряд, то между ними появится двойной пробел. Заменяем на один.
            expression = expression.Replace(SEPARATOR + SEPARATOR, SEPARATOR);
            // Если после скобки стоит отрицательное число, то убрать пробел между минусом и числом.
            expression = expression.Replace(
                Brackets.OpenBracket + SEPARATOR + MINUS + SEPARATOR,//"( - "
                Brackets.OpenBracket + SEPARATOR + MINUS); // "( -"           

            List<string> result = expression.Split(' ').ToList();

            // Удаляем скобки у отрицательных чисел
            for (int i = 1; i < result.Count - 1; i++)
            {
                if (result[i][0] == MINUS &&
                    result[i - 1] == Brackets.OpenBracket.ToString() &&
                    result[i + 1] == Brackets.CloseBracket.ToString())
                {
                    result.RemoveAt(i + 1); // )
                    result.RemoveAt(i - 1); // (                
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Является ли переданная строка мат. символом имеющимся в списке операций.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsOperaionSymbol(string symbol)
        {
            if (symbol.Length > 1) return false;
            return operations.GetOperationSymbolList().ToArray().Contains(symbol[0]);
        }

        /// <summary>
        /// Соединяет массив символов и чисел в строку. 
        /// Отрицательные числа, если необходимо, ставит в скобки.
        /// </summary>
        /// <param name="dividedExp"></param>
        /// <returns></returns>
        public string Join(IEnumerable<string> dividedExp)
        {
            List<string> expList = dividedExp.ToList();

            for (int i = 1; i < expList.Count; i++)
            {
                if (expList[i][0] == MINUS && IsOperaionSymbol(expList[i - 1]))
                {
                    expList.Insert(i + 1, Brackets.CloseBracket.ToString());
                    expList.Insert(i, Brackets.OpenBracket.ToString());
                }
            }

            string result = string.Join("", expList);
            return result;
        }

        /// <summary>
        /// Преобразует простое выражение в структуру.
        /// </summary>
        /// <param name="simpleExp"></param>
        /// <returns></returns>
        public OperationInfo ParseSimpleExp(string simpleExp)
        {
            string[] splitExp = Split(simpleExp).ToArray();
            if (splitExp.Length != 3)
                throw new FormatException("Не верный формат выражения");

            return new OperationInfo()
            {
                OperandLeft = double.Parse(splitExp[0]),
                OperationSymbol = splitExp[1][0],
                OperandRight = double.Parse(splitExp[2])
            };
        }
    }
}
