using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_BLL.Abstract
{
    public interface IPriorityOperations : IMathSymbols
    {
        IEnumerable<byte> GetPriorityCollections();
        IEnumerable<char> GetOperationSymbolListByPriority(byte priority);
    }
}
