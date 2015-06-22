using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class CorporatePriority
    {
        public static CorporatePriority Create(string can, string contractDetail, DateTime? contractEndDate, decimal? contractValue,long userId, CreatedUser createdUser)
        {
            return new CorporatePriority
            {
                Can = can,
                ContractDetail = contractDetail,
                ContractEndDate = contractEndDate,
                ContractValue = contractValue,
                CreatedBy = createdUser.Name,
                CreatedDate = createdUser.CreatedDate,
                UserId = userId
            };
        }

        public void MarkAsDelete(ModifiedUser modifiedUser, DeletedUser deletedUser)
        {
            Deleted = true;
            DeletedBy = deletedUser.Name;
            DeletedDate = deletedUser.DeletedDate;
            LastModifiedBy = modifiedUser.Name;
            LastModifiedDate = modifiedUser.LastModifiedDate;
        }

        public void Edit(decimal? contractValue, string contractDetail, DateTime? contractEndDate, long userId ,ModifiedUser modifiedUser)
        {
            ContractValue = contractValue;
            ContractDetail = contractDetail;
            ContractEndDate = contractEndDate;
            UserId = userId;
            LastModifiedBy = modifiedUser.Name;
            LastModifiedDate = modifiedUser.LastModifiedDate;
        }
    }
}
