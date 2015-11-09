using System;
using Calculator_BLL;
using Calculator_BLL.Abstract;

namespace Calculator_UI
{
    class Program
    {
        static void Main(string[] args)
        {                      
            ICalculator calc = Injector.GetCalculator();

            while (true)
            {
                Console.WriteLine("Введите выражение:");
                string expression = Console.ReadLine();
                if (expression == "q") break;

                try
                {
                    string result = calc.Calculate(expression);
                    Console.WriteLine("Результат: {0}", result);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Произошла ошибка: Деление на ноль!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Произошла ошибка: Неверный формат выражения!");
                }
            }
        }
    }
}
