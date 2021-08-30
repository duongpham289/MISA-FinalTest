using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;

namespace MISA.Test.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
    }
}
