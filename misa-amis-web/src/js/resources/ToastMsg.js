var Toast = Toast || {};

// Thông báo
Toast.Message = {
  LoadSuccess: "Tải dữ liệu thành công",
  AddSuccess: "Thêm dữ liệu thành công",
  UpdateSuccess: "Cập nhật dữ liệu thành công",
  DeleteSuccess: "Xoá dữ liệu thành công",
  ServerError: "Có lỗi xảy ra, vui lòng liên hệ MISA",
};

//Các chế độ toast message
Toast.Type = {
  Success: "success",
  Warning: "warning",
  Error: "error",
};

export default Toast;
