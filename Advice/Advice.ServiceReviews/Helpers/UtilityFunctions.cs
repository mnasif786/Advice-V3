using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;

namespace Advice.ServiceReviews.Helpers
{
    public static class UtilityFunctions
    {
       public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteToLog(string message)
        {
            Log.Info(new InfoLogRequest
            {
              IndexType   = ElkIndexTypeProvider.ServiceReviewService,
              Message = message
            });
        }

        //write out to file
       //public static void WriteDebug(string stringToWrite)
       // {
       //     var fs = new FileStream(Settings.Default.DebugOutputFilePath,
       //     FileMode.OpenOrCreate, FileAccess.Write);
       //     var mStreamWriter = new StreamWriter(fs);
       //     mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
       //     mStreamWriter.WriteLine("Date[" + DateTime.Now + "]:-   " + stringToWrite);
       //     mStreamWriter.Flush();
       //     mStreamWriter.Close();
       // }
    }
}
