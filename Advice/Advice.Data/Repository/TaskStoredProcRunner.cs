using Advice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;

namespace Advice.Data.Repository
{
    public class TaskStoredProcRunner : ITaskStoredProcRunner
    {
        public IEnumerable<GetTasksByTeamIds_Type> ExecuteQuery(ObjectContext context, String storedProcName, long [] teamIds)
        {
            if(null == context)
            {
                throw new ArgumentNullException("context must not be null");
            }

            if(null == storedProcName)
            {
                throw new ArgumentNullException("storedProcName must not be null");
            }

            if("" == storedProcName)
            {
                throw new ArgumentException("storedProcName must not be empty");
            }

            if(null == teamIds)
            {
                return new List<GetTasksByTeamIds_Type>();
            }

            DataTable teamIdsTable = GenerateTeamIdsTable(teamIds);

            object[] teamIds_parameters = new object[] { GenerateStoredProcParameter(teamIdsTable) };

            return context.ExecuteStoreQuery<GetTasksByTeamIds_Type>("exec " + storedProcName + " @p0", teamIds_parameters);
        }

        private static DataTable GenerateTeamIdsTable(long[] teamIds)
        {
            DataTable teamIdsTable = new DataTable();

            teamIdsTable.Columns.Add("teamId");

            foreach (long teamId in teamIds)
            {
                teamIdsTable.Rows.Add(teamId);
            }

            return teamIdsTable;
        }

        private static SqlParameter GenerateStoredProcParameter(DataTable teamIdsTable)
        {
            SqlParameter storedProcParameter = new SqlParameter();
            storedProcParameter.ParameterName = "p0";
            storedProcParameter.Value = teamIdsTable;
            storedProcParameter.TypeName = "[dbo].[tmp_teamids]";
            storedProcParameter.SqlDbType = SqlDbType.Structured;

            return storedProcParameter;
        }
    }
}
