using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator_BLL.Infrastructure;

namespace Calculator_BLL.Abstract
{
    public interface IExpression
    {
        string GetFirstSubExpInBrackets(string expression);
        string ReplaceFirstExpInBrackets(string expression, string value);
        OperationInfo FindFirstOperationByPriority(string expression);
        string ReplaceFirstOperationByPriority(string expression, string value);
    }
}
