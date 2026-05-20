using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public static class Logger
    {
        public static ILog Log
        {
            get { return LogManager.GetLogger(typeof(Logger)); }
        }
    }
}
