using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Application.Utils
{
    public class Util
    {
        public static DateTime HrBrasilia(DateTime prmDate)
        {
            TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(prmDate, hrBrasilia);
        }
    }
}
