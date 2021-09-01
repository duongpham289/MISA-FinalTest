import BaseAPI from "@/api/base/BaseAPI.js";
import BaseAPIConfig from "../base/BaseAPIConfig.js";

class EmployeeAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "v1/Employees";
  }

  /**
   * Hàm lấy dữ liệu phân trang
   * @param {*} payload
   */
   paging(pageIndex, pageSize, entityFilter) {
    return BaseAPIConfig.get(`${this.controller}/paging?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${entityFilter}`);
  }
}

export default new EmployeeAPI();
