using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Abstract
{
    public interface IParser
    {
        IEnumerable<string> Split(string expression);
        string Join(IEnumerable<string> dividedExp);
        OperationInfo ParseSimpleExp(string simpleExp);
    }
}
