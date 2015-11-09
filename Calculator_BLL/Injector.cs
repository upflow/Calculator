using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Abstract;
using Calculator_BLL.Domain;
using Calculator_BLL.MathOperations;
using Ninject;

namespace Calculator_BLL
{
    public static class Injector
    {
        private static IKernel ninject;

        private static void Injection()
        {
            ninject = new StandardKernel();

            ninject.Bind<Calculator>().ToSelf();

            ninject.Bind<IMathOperation>().To<SumOperation>();
            ninject.Bind<IMathOperation>().To<DifferentOperation>();
            ninject.Bind<IMathOperation>().To<MultiplicationOperation>();
            ninject.Bind<IMathOperation>().To<DivisionOperation>();

            ninject.Bind<IOperations>().To<Operations>();
            ninject.Bind<IMathSymbols>().To<Operations>();
            ninject.Bind<IPriorityOperations>().To<Operations>();
            ninject.Bind<IParser>().To<Parser>();

            ninject.Bind<IExpression>().To<Expression>();
        }

        static Injector()
        {
            Injection();
        }

        public static ICalculator GetCalculator()
        {
            return ninject.Get<Calculator>();
        }
    }
}
