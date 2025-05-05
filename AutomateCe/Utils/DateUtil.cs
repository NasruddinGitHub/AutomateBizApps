using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Utils
{
    public static class DateUtil
    {

        public static string GetTimeStamp(string format)
        {
            return DateTime.Now.ToString(format);
        }
    }
}
