using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Advice.Data.Tests.TestHelpers
{
    // TODO: this is going to be useful in other test projects too. Might be worth moving somewhere common?
    public static class DbSetInitialisedMockFactory<TEntity>
        where TEntity : class
    {
        public static Mock<DbSet<TEntity>> CreateDbSetInitalisedMock(List<TEntity> dataList)
        {
            var data = dataList.AsQueryable();
            var dbSetMock = new Mock<DbSet<TEntity>>();

            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            return dbSetMock;
        }

    }
}
