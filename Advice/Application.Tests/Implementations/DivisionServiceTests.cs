using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Implementations;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations
{
    [TestFixture]
    public class DivisionServiceTests
    {
        private Mock<IDivisionRepository> _divisionRepository;

        [SetUp]
        public void TestFixtureSetup()
        {
            _divisionRepository = new Mock<IDivisionRepository>();
        }


        [Test]
        public void Given_Division_exists_When_UpdateDivision_Is_called_Then_Division_Details_are_updated()
        {
            var division = new Division()
            {
                DivisionId = 1,
                Description = "Division 1"
            };

            _divisionRepository.Setup(x => x.GetById(1)).Returns(division);


            _divisionRepository
                .Setup( x => x.Update( It.IsAny<Division>()) )
                .Callback<Division>( (d) => division = d );
            
            var divisionService = GetTarget();


            const int divisionId = 1;
            const string divisionName = "JoyDivision";            
            var updatedModel = new DivisionModel(){DivisionId = divisionId,  Description = divisionName};
            divisionService.UpdateDivision( updatedModel);

            Assert.That(division, Is.Not.Null);

            Assert.AreEqual( divisionId, division.DivisionId );
            Assert.AreEqual( divisionName, division.Description );
        }

        [Test]
        public void Given_Division_does_not_exist_When_AddDivision_Is_called_Then_Division_Is_Added()
        {
            Division division = null;
            _divisionRepository
                .Setup(x => x.Insert(It.IsAny<Division>()))
                .Callback<Division>((d) => division = d);

            var divisionService = GetTarget();

            DivisionModel model = new DivisionModel() { Description = "JoyDivision" };

            string username = "Barney Rubble";
            divisionService.AddDivision( model, username);

            Assert.That(division, Is.Not.Null);

            Assert.AreEqual(model.Description, division.Description);
        }


        private DivisionService GetTarget()
        {
            return new DivisionService(_divisionRepository.Object);
        }
    }
}
