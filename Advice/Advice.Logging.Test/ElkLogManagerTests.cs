using System;
using System.Reflection;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using NUnit.Framework;

namespace Advice.Logging.Test
{
    [TestFixture]
    public class ElkLogManagerTests
    {
        [Test]
        public void Given_When_Index_Type__Message_User_Are_Defined_Then_Log_Is_Stored_In_Elastic_Search()
        {
            IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            Log.Info(new InfoLogRequest() { IndexType = ElkIndexTypeProvider.EventsTasklog, Message = "again" });

        }

        [Test]
        public void Given_When_Message_And_User_Are_Defined_Then_Log_Is_Stored_In_Elastic_Search_In_Default_Index()
        {
            IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            Log.Info(new InfoLogRequest() { Message = "hazel", User = "Grove" });
     
        }

        [Test]
        public void Given_When_Index_Type_User_Are_Defined_Then_Log_Is_Stored_In_Elastic_Search()
        {
            IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            Log.Info(new InfoLogRequest() { IndexType = ElkIndexTypeProvider.ErrorsError, Message = "hello", User = "world" });
          
        }

        [Test]
        public void Given_When_Exception_is_thrown_it_is_logged_In_Elastic_Search()
        {
            IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            try
            {
                //InfoLogRequest info = null;
                //string i = info.IndexType.Type;
                int[] a = new[] { 1, 3 };
                int i = a[3];
            }
            catch (Exception e)
            {
                var exceptionLogRequest = new ExceptionLogRequest()
                {
                    Exception = e
                };
                Log.LogException(exceptionLogRequest); 
            }
           

        }
    }
}
