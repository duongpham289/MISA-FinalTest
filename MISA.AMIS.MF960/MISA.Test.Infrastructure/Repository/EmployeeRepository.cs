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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách nhân viên phân trang từ DataBase
        /// </summary>
        /// <returns>Danh sách nhân viên và dữ liệu phân trang</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public object GetPaging(int pageIndex, int pageSize, string employeeFilter, Guid? departmentId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeFilter", employeeFilter != null ? employeeFilter : String.Empty);
                parameters.Add("@DepartmentId", departmentId);
                parameters.Add("@PageIndex", pageIndex);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var data = _dbConnection.Query<Employee>($"Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                var totalRecord = parameters.Get<Int32>("@TotalRecord");


                var pagingData = new
                {
                    totalRecord,
                    data
                };

                return pagingData;
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
