using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;

namespace MISA.Test.Core.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        #region DECLARE

        IProjectRepository _projectRepository;
        #endregion

        #region Constructor
        public ProjectService(IProjectRepository projectRepository, IBaseRepository<Project> baseRepository) : base(baseRepository)
        {
            _projectRepository = projectRepository;
        }
        #endregion
    }
}
