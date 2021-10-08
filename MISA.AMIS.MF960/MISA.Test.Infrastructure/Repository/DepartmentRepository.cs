using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Test.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department">Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        public Guid AddDepartment(Department department)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var dynamicParam = new DynamicParameters();

                //Đọc từng prop của obj:
                var properties = department.GetType().GetProperties();

                //Duyệt từng prop:
                foreach (var prop in properties)
                {
                    var propName = prop.Name;

                    var propValue = prop.GetValue(department);

                    dynamicParam.Add($"@{propName}", propValue);
                }


                dynamicParam.Add("@DepartmentId", dbType: DbType.Guid, direction: ParameterDirection.Output);

                _dbConnection.Execute($"Proc_InsertDepartment", param: dynamicParam, commandType: CommandType.StoredProcedure);

                return dynamicParam.Get<Guid>("@DepartmentId");
            }
        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="departmentUser">Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/10/2021)
        public int AddDepartmentUser(DepartmentUser departmentUser)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                _dbConnection.Open();
                var transaction = _dbConnection.BeginTransaction();

                var rowsEffect = 0;
                foreach (var id in departmentUser.ListUserId)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add($"@DepartmentId", departmentUser.DepartmentId.ToString());
                    parameters.Add($"@UserId", id.ToString());

                    rowsEffect += _dbConnection.Execute($"Proc_InsertDepartmentUser", param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();

                if (rowsEffect == departmentUser.ListUserId.Count)
                    return rowsEffect;
                else
                    return 0;

            }
        }

        /// <summary>
        /// Lấy danh sách Thực thể từ DataBase
        /// </summary>
        /// <returns>List Thực thể</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<Department> GetByUserId(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@UserId", entityId);
                var departments = _dbConnection.Query<Department>($"Proc_GetDepartmentByUserId", param: parameters, commandType: CommandType.StoredProcedure);

                return departments.AsList();
            }
        }
        #endregion
    }
}
