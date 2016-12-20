using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EOS
{
    class LogMgt
    {
        private const string LOG_TYPE_DBUG = "D";

        public LogMgt()
        {
 
        }

        public static void Debug(string strTag, string strContent)
        {
            string strLog = String.Format("[%s][%s][%s]", LOG_TYPE_DBUG, strTag, strContent);

            Console.WriteLine(strLog);
        }
    }
}
