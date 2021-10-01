using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web07.FinalTest.MF960.Controllers;

namespace MISA.AMIS.MF960.Controllers
{
   [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectsController : BaseEntityController<Project>
    {
        #region Constructor
        public ProjectsController(IProjectService departmentService, IProjectRepository departmentRepository) : base(departmentService, departmentRepository)
        {
            
        }
        #endregion
    }
}
