using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Common.Logic
{
    public interface ILogger
    {
        TimeSpan ExecutionTime { get; set; }
        int counter { get; set; }
        
        void Debug(string message);
        void Error(string message);
        void Exception(Exception exception, string message);
        void Exception(Exception exception);
    }
}
