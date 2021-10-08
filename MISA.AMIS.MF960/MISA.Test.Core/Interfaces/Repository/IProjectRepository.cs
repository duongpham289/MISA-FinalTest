using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Repository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        /// <summary>
        /// Lấy thông tin entity qua Id
        /// </summary>
        /// <param name="userId">Mã định danh user</param>
        /// <returns>Thông tin entity</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        List<Project> GetByUserId(Guid userId);

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="project">Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        Guid AddProject(Project project);

        /// <summary>
        /// Thêm mới người tham dự phòng ban
        /// </summary>
        /// <param name="projectUser"></param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        int AddProjectUser(ProjectUser projectUser);
    }
}
