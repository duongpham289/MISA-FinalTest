var Resource = Resource || {};

// Thông báo của Popup
Resource.PopupMessage = {
  DeleteMany: `Bạn có thực sự muốn xóa các nhân viên này không?`,
  DeleteOne: `Bạn có thực sự muốn xóa Nhân viên <EmployeeCode> không?`,
  EmployeeCode: "Mã không được để trống.",
  EmployeeCodeDuplicate: "Mã nhân viên <EmployeeCode> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
  DepartmentId: "Đơn vị không được để trống.",
  FullName: "Tên không được để trống.",
  FormNotChanged: "Không thể cất vì chưa thực hiện thay đổi.",
  DeleteListEmpty: "Không có dữ liệu để xóa.",
  UnIdentifyData: "Dữ liệu này không còn tồn tại trên hệ thống, vui lòng load lại trang.",
  CloseOnFormChanged: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
};

//Các Button Popup
Resource.PoupButton = {
  ButtonCancel: "Hủy",
  ButtonCecline: "Không",
  ButtonYes: "Có",
  ButtonAgree: "Đồng ý",
  ButtonClose: "Đóng",
};

// Mã trả về
Resource.StatusCode = {
  OK: 200,
  Created: 201,
  NoContent: 204,
  BadRequest: 400,
  Unauthorized: 401,
  NotFound: 404,
  MethodNotAllowed: 405,
  ServerError: 500,
};

// Lỗi
Resource.ErrorMessage = {
  BadRequest: "Có lỗi cú pháp trong yêu cầu và yêu cầu bị từ chối",
  Unauthorized: "Không có quyền truy cập",
  NotFound: "Không tìm thấy đường dẫn",
  MethodNotAllowed: "Phương pháp truy cập file không được cho phép"
};

// Thông báo
Resource.ToastMessage = {
  LoadSuccess: "Tải dữ liệu thành công",
  AddSuccess: "Thêm dữ liệu thành công",
  UpdateSuccess: "Cập nhật dữ liệu thành công",
  DeleteSuccess: "Xoá dữ liệu thành công",
  ExportSuccess: "Xuất dữ liệu thành công",
  ServerError: "Có lỗi xảy ra, vui lòng liên hệ MISA",
};

// Các chế độ toast message
Resource.ToastType = {
  Success: "success",
  Warning: "warning",
  Error: "error",
};

// Dữ liệu giới tính 
Resource.GenderList = [
  { value: 1, name: "Nam" },
  { value: 0, name: "Nữ" },
  { value: 2, name: "Khác" },
];

export default Resource;