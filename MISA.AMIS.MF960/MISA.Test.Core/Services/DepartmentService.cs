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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region DECLARE

        IDepartmentRepository _departmentRepository;
        IProjectRepository _projectRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IProjectRepository projectRepository, IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu truyền vào</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(16/08/2021)
        public ServiceResult GetDepartmentsWithProjects()
        {

            var departments = _departmentRepository.GetAll();
            var projects = _projectRepository.GetAll();

            foreach (var department in departments)
            {
                foreach (var project in projects)
                {
                    if (project.DepartmentId == department.DepartmentId)
                    {
                        department.ListProjects.Add(project);
                    }
                }
            }

            _serviceResult.Data = departments;
            return _serviceResult;

        }
        #endregion
    }
}
