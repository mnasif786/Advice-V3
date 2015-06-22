using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Data.Helpers
{
    public class DatabaseFunctions
    {
        [DbFunction("Edm", "TruncateTime")]
        public static DateTime? TruncateTime(DateTime? date)
        {
            return date.HasValue ? date.Value.Date : (DateTime?)null;
        }

        [DbFunction("Edm", "AddDays")]
        public static DateTime? AddDays(DateTime? date, int noOfDaysToAdd)
        {
            return date.HasValue ? date.Value.AddDays(noOfDaysToAdd) : (DateTime?)null;
        }

        //[DbFunction("Edm", "CreateDateTime")]
        //public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
        //{
        //    return new DateTime(year,month,day, hour, minute, second); //DbFunctions.CreateDateTime(year, month, day, hour, minute, second);
        //}

        [DbFunction("Edm", "CreateDateTime")]
        public static DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second); //DbFunctions.CreateDateTime(year, month, day, hour, minute, second);
        }
       
    }
}
