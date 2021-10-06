using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;

namespace MISA.Test.Core.Services
{
    public class TaskService : BaseService<Task>, ITaskService
    {
        #region DECLARE

        ITaskRepository _taskRepository;
        #endregion

        #region Constructor
        public TaskService(ITaskRepository taskRepository, IBaseRepository<Task> baseRepository) : base(baseRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion
    }
}
