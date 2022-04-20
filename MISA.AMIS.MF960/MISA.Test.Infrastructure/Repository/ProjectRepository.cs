using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Infrastructure.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Constructor
        public ProjectRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách Thực thể từ DataBase
        /// </summary>
        /// <returns>List Thực thể</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<Project> GetByUserId(Guid userId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@UserId", userId);
                var projects = _dbConnection.Query<Project>($"Proc_GetProjectByUserId", param: parameters, commandType: CommandType.StoredProcedure);

                return projects.AsList();
            }
        }

        /// <summary>
        /// Thêm mới Dự án
        /// </summary>
        /// <param name="project">Dự án</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        public Guid AddProject(Project project)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var dynamicParam = new DynamicParameters();

                //Đọc từng prop của obj:
                var properties = project.GetType().GetProperties();

                //Duyệt từng prop:
                foreach (var prop in properties)
                {
                    var propName = prop.Name;

                    var propValue = prop.GetValue(project);

                    dynamicParam.Add($"@{propName}", propValue);
                }


                dynamicParam.Add("@ProjectId", dbType: DbType.Guid, direction: ParameterDirection.Output);

                _dbConnection.Execute($"Proc_InsertProject", param: dynamicParam, commandType: CommandType.StoredProcedure);

                return dynamicParam.Get<Guid>("@ProjectId");
            }
        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department">Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        public int AddProjectUser(ProjectUser projectUser)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                _dbConnection.Open();
                var transaction = _dbConnection.BeginTransaction();

                var rowsEffect = 0;
                foreach (var id in projectUser.ListUserId)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add($"@ProjectId", projectUser.ProjectId.ToString());
                    parameters.Add($"@UserId", id.ToString());

                    rowsEffect += _dbConnection.Execute($"Proc_InsertProjectUser", param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();

                if (rowsEffect == projectUser.ListUserId.Count)
                    return rowsEffect;
                else
                    return 0;

            }
        }
        #endregion
    }
}
