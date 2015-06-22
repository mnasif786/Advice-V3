using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Application.Implementations
{
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository _divisionRepository;

        public DivisionService(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public IEnumerable<DivisionModel> GetAllDivisions()
        {
            return _divisionRepository.GetAll().Select(d => new DivisionModel() { DivisionId = d.DivisionId, Description = d.Description });
        }

        public void UpdateDivision(DivisionModel divisionModel)
        {
            var division = _divisionRepository.GetById(divisionModel.DivisionId);
            division.Description = divisionModel.Description;
            _divisionRepository.Update(division);
            _divisionRepository.SaveChanges();
        }

        public void AddDivision(DivisionModel divisionModel, string username)
        {
            Division division = new Division()
            {
                Description = divisionModel.Description,
            };

            _divisionRepository.Insert(division);
            _divisionRepository.SaveChanges();
        }
    }
}
