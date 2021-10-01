using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        #region DECLARE

        IProjectRepository _departmentRepository;
        #endregion

        #region Constructor
        public ProjectService(IProjectRepository departmentRepository, IBaseRepository<Project> baseRepository) : base(baseRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
    }
}
