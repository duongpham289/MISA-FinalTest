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
        public ServiceResult GetDepartmentsWithProjects(Guid userId)
        {

            var departments = _departmentRepository.GetByUserId(userId);
            var allProjects = _projectRepository.GetAll();
            var projectsAssigned = _projectRepository.GetByUserId(userId);


            var temp = new List<Department>();

            foreach (var department in departments)
            {
                foreach (var project in allProjects)
                {
                    if (project.DepartmentId == department.DepartmentId)
                    {
                        department.IsBelongToCurrentUser = true;
                        department.ListProjects.Add(project);
                    }

                }
                foreach (var project in projectsAssigned)
                {
                    if (project.DepartmentId != department.DepartmentId)
                    {
                        var additionDepartment = new Department
                        {
                            DepartmentId = project.DepartmentId,
                            DepartmentName = project.DepartmentName,
                            IsBelongToCurrentUser = false
                        };

                        additionDepartment.ListProjects.Add(project);

                        temp.Add(additionDepartment);
                    }


                }
            }

            departments.AddRange(temp);

            _serviceResult.Data = departments;
            return _serviceResult;

        }
        #endregion
    }
}
