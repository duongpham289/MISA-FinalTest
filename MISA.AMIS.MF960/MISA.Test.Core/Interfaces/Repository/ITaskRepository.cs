using MISA.Test.Core.Entities;
using System.Collections.Generic;

namespace MISA.Test.Core.Interfaces.Repository
{
    public interface ITaskRepository : IBaseRepository<Task>
    {
        /// <summary>
        /// Lấy tất cả Công việc theo ProjectId
        /// </summary>
        /// <param name="ProjectId">Biến ProjectId</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        List<Task> GetTasksByProjectId(int ProjectId);
    }
}
