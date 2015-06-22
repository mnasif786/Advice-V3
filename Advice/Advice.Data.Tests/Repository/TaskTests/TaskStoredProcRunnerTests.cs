using Advice.Data.Repository;
using Advice.Domain;
using Advice.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Data.Tests.Repository.TaskTests
{
    [TestFixture]
    public class TaskStoredProcRunnerTests
    {
        [SetUp]
        public void TestFixtureSetup()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaskStoredProcRunner_Throws_if_context_is_null()
        {
            TaskStoredProcRunner runner = new TaskStoredProcRunner();

            runner.ExecuteQuery(null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaskStoredProcRunner_Throws_if_procedure_name_is_null()
        {
            TaskStoredProcRunner runner = new TaskStoredProcRunner();

            AdviceEntities testEntities = new AdviceEntities();
            ObjectContext testContext = ((IObjectContextAdapter) testEntities).ObjectContext;

            String nullStoredProcName = null;

            runner.ExecuteQuery(testContext, nullStoredProcName, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TaskStoredProcRunner_Throws_if_procedure_name_is_empty()
        {
            TaskStoredProcRunner runner = new TaskStoredProcRunner();

            AdviceEntities testEntities = new AdviceEntities();
            ObjectContext testContext = ((IObjectContextAdapter) testEntities).ObjectContext;

            String emptyStoredProcName = "";
            runner.ExecuteQuery(testContext, emptyStoredProcName, null);
        }

        [Test]
        public void TaskStoredProcRunner_returns_empty_list_if_teamIds_is_null()
        {
            TaskStoredProcRunner runner = new TaskStoredProcRunner();

            AdviceEntities testEntities = new AdviceEntities();
            ObjectContext testContext = ((IObjectContextAdapter)testEntities).ObjectContext;

            String storedProcName = "Test Stored Procedure Name";
            long[] nullTeamId = null;
            IEnumerable<GetTasksByTeamIds_Type> queryResult = runner.ExecuteQuery(testContext, storedProcName, nullTeamId);

            Assert.AreEqual(0, queryResult.Count());
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void TaskStoredProcRunner_throws_exception_for_unknown_procedure()
        {
            TaskStoredProcRunner runner = new TaskStoredProcRunner();

            AdviceEntities testEntities = new AdviceEntities();
            ObjectContext testContext = ((IObjectContextAdapter)testEntities).ObjectContext;

            String storedProcName = "Invalid Stored Procedure Name";
            long[] teamIds = new long[] { 0 };
            IEnumerable<GetTasksByTeamIds_Type> queryResult = runner.ExecuteQuery(testContext, storedProcName, teamIds);

            Assert.AreEqual(0, queryResult.Count());
        }
    }
}
