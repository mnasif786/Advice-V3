using System.Linq;
using Advice.Data.Contracts;
using Advice.Data.CustomExceptions;
using Advice.Data.Tests.Common;
using Advice.Domain;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.TeamTests
{
    [TestFixture]
    public class TeamRepositoryTests : BaseTeamRepositoryTests
    {
        [Test]
        public void Give_valid_departement_id_When_Get_by_Department_Id_is_called_Then_all_relevant_Teams_returned()
        {
            var teams = TeamRepository.GetTeamsByDepartmentId(1);
            Assert.That(teams.Count(), Is.EqualTo(2));

            teams = TeamRepository.GetTeamsByDepartmentId(2);
            Assert.That(teams.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GivenWhen_get_by_teamName_then_return_Id_of_team()
        {
            var teamId = TeamRepository.GetTeamIdByTeamName(Teams[0].Description);
            Assert.That(teamId, Is.EqualTo(Teams[0].TeamID));

            teamId = TeamRepository.GetTeamIdByTeamName(Teams[1].Description);
            Assert.That(teamId, Is.EqualTo(Teams[1].TeamID));
        }

        [Test]
        public void Given_teamName_does_not_exist_then_return_null()
        {
            Assert.Throws<TeamNameNotFoundException>(() => TeamRepository.GetTeamIdByTeamName("TheTeamThatDoesNotExist"));
        }
    }
}
