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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách nhân viên phân trang từ DataBase
        /// </summary>
        /// <returns>Danh sách nhân viên và dữ liệu phân trang</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public dynamic GetPaging(int pageIndex, int pageSize, string employeeFilter, bool check)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeFilter", employeeFilter ?? String.Empty);
                parameters.Add("@PageIndex", pageIndex);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var data = _dbConnection.Query<User>($"Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                var totalRecord = parameters.Get<Int32>("@TotalRecord");


                var pagingData = new
                {
                    totalRecord,
                    data
                };

                if (check)
                {
                    return pagingData;
                }
                else
                {
                    return data.AsList();
                }
            }
        }


        /// <summary>
        /// Lấy dữ liệu Mã entity
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<string> GetAllEmployeeCode()
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var empCodes = _dbConnection.Query<string>($"Proc_GetAllEmployeeCode", commandType: CommandType.StoredProcedure);

                return empCodes.AsList();
            }
        }

        /// <summary>
        /// Lấy dữ liệu Mã entity
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public string GetNewEmployeeCode()
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                var empCode = _dbConnection.QueryFirstOrDefault($"Proc_GetAllEmployeeCode", commandType: CommandType.StoredProcedure);


                var empNewCode = empCode.EmployeeCode.Substring(0, 3) + (Int32.Parse(empCode.EmployeeCode.Substring(3)) + 1).ToString();

                return empNewCode;
            }
        }
        #endregion

    }
}
