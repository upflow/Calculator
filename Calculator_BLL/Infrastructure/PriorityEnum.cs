using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_BLL.Infrastructure
{
    /// <summary>
    /// Приоритеты операций (сначала будут выполняться операции с большим приоритетом)
    /// </summary>
    public enum PriorityEnum : byte
    {
        Sum = 10,
        Dif = 10,
        Mult = 20,
        Div = 20
    }
}
