using System;

namespace Advice.Data.CustomExceptions
{
    public class TeamNameNotFoundException : ApplicationException
    {
        public TeamNameNotFoundException(string teamName, string exceptionMessage) : base(string.Format("Team name {0} does not exist.  Error is: {1}", teamName, exceptionMessage))
        {
        }
    }

    public class MultipleTaskFoundException : ApplicationException
    {
        public MultipleTaskFoundException(long taskId, string exceptionMessage)
            : base(string.Format("TaskId {0} has multiple entries in the database.  ErrorMessage: {1}", taskId, exceptionMessage))
        {
        }
    }

    public class MultipleDepartmentFoundException : ApplicationException
    {
        public MultipleDepartmentFoundException(long departmentId, string exceptionMessage)
            : base(string.Format("DepartmentId {0} has multiple entries in the database.  ErrorMessage: {1}", departmentId, exceptionMessage))
        {
        }
    }

   
}
