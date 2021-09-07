import BaseAPIConfig from "./baseAPIConfig.js";

export default class BaseAPI {
  constructor() {
    this.controller = null;
  }
  /**
   * Phương thức lấy tất cả dữ liệu
   */
  getAll() {
    return BaseAPIConfig.get(`${this.controller}`);
  }
  /**
   * Phương thức lấy dữ liệu theo Id
   */
  getById(id) {
    return BaseAPIConfig.get(`${this.controller}/${id}`);
  }
  /**
   * Phương thức lấy code mới
   * @param {*} propName 
   * @returns 
   */
  getNewCode(){
    return BaseAPIConfig.get(`${this.controller}/getNewCode`);
  }
  /**
   * Phương thức lấy tất code
   * @returns 
   */
  getAllCode(){
    return BaseAPIConfig.get(`${this.controller}/getAllCode`);
  }
  /**
   * Phương thức thêm mới dữ liệu
   */
  create(body) {
    return BaseAPIConfig.post(`${this.controller}`, body);
  }
  /**
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   */
  update(id, body) {
    return BaseAPIConfig.put(`${this.controller}/${id}`, body);
  }
  /**
   * Hàm xóa 1 bản ghi
   * @param {*} id
   */
  delete(id) {
    return BaseAPIConfig.delete(`${this.controller}/${id}`);
  }
  /**
   * Hàm xóa nhiều bản ghi
   * @param {*} listId
   */
  deleteList(listId) {
    return BaseAPIConfig.post(`${this.controller}/delete`,listId);
  }
  /**
   * Check trùng
   * @param {*} entityCode
   */
  checkDuplicate(entityCode) {
    return BaseAPIConfig.get(`${this.controller}/checkDuplicate/${entityCode}`);
  }
}
