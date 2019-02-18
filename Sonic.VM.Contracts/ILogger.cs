using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonic.VM.Contracts
{
    public interface ILogger
    {
        void LogInfoMessage(string format, string message);
        void LogErrorMessage(string format, string message);
        void LogError(string message, Exception ex);
    }
}
