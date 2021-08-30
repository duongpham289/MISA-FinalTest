using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web07.FinalTest.MF960.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntityController<Department>
    {
        #region Constructor
        public DepartmentsController(IDepartmentService departmentService, IDepartmentRepository departmentRepository) : base(departmentService, departmentRepository)
        {
            
        }
        #endregion
    }
}
