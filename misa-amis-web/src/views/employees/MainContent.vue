<template>
  <div class="content">
    <ContentHeader @openForm="openForm" />

    <div class="content-table__container">
      <ContentToolBar
        @btnReloadOnClick="btnReloadOnClick"
        @deleteSelectedRows="deleteSelectedRows"
        @filterDataTable="filterDataTable"
        @exportData="exportData"
      />

      <div
        class="table-view"
        :style="employeesData.length <= 0 ? 'overflow:hidden' : ''"
      >
        <base-table
          :columns="columns"
          :data="employeesData"
          @dbClickRow="openForm"
          @checkBoxOnClick="checkBoxOnClick"
          @deleteSelectedRows="deleteSelectedRows"
        />
      </div>

      <div
        v-if="employeesData.length <= 0"
        class="no-content d-block flex-center w-100"
      >
        <div class="no-content-img">
          <img src="../../assets/img/no_conent.svg" />
        </div>

        <label>Không có dữ liệu</label>
      </div>

      <base-paginate
        :employeesData="employeesData"
        :pageIndex="pageIndex"
        :totalRecord="totalRecord"
        :pageSize="pageSize"
        :employeeFilter="employeeFilter"
        @changePageIndex="getEmployeePagingData"
        @changePageSize="getEmployeePagingData"
      />
    </div>

    <base-spinner :isLoading="isLoading" />

    <Modal
      ref="Modal"
      :departmentCbb="departmentCbb"
      :mode="mode"
      :toastList="toastList"
      @setPopup="setPopup"
      @btnReloadOnClick="btnReloadOnClick"
      @errorHandler="errorHandler"
    />

    <base-toast-message
      v-for="(item, index) in toastList"
      :toast="item"
      :key="index"
      :index="index"
    ></base-toast-message>

    <base-popup :info="popupInfo" @close="closePopup"></base-popup>
  </div>
</template>

<script>
import Modal from "../employees/Modal.vue";
import EmployeeAPI from "@/api/components/EmployeeAPI.js";
import DepartmentAPI from "@/api/components/DepartmentAPI.js";
import ErrorMessage from "@/js/resources/ErrorMsg";
import ToastMessage from "@/js/resources/ToastMsg";

import ContentHeader from "./ContentHeader.vue";
import ContentToolBar from "./ContentToolBar.vue";

import { columns } from "./EmployeeTableCols.js";
import BasePaginate from "../../components/table/BasePaginate.vue";

export default {
  components: {
    Modal,
    ContentHeader,
    ContentToolBar,
    BasePaginate,
  },

  created() {
    this.getEmployeePagingData(
      this.pageIndex,
      this.pageSize,
      this.employeeFilter
    );
    this.getDropdownData();
  },

  data() {
    return {
      pageIndex: 1,
      pageSize: 20,
      totalRecord: 0,

      toastList: [],
      timeoutRemoveToastList: null,

      departmentCbb: [],

      employeeFilter: "",

      employeesData: [],
      employeesToDelete: [],
      columns: columns,

      mode: 0,
      isLoading: false,

      popupInfo: {
        btnLeft: null,
        btnRightFirst: null,
        btnRightSec: null,
        btnCenter: null,
        isShowed: false,
        icon: null,
        message: "",
        action: null,
        cancel: null,
      },
    };
  },
  methods: {
    /**
     * Lấy dữ liệu Dropdown
     * CreatedBy: PHDUONG(30/07/2021)
     */
    getDropdownData() {
      var vm = this;

      DepartmentAPI.getAll()
        .then((res) => {
          res.data.forEach((item) => {
            vm.departmentCbb.push({
              DepartmentId: item.DepartmentId,
              DepartmentCode: item.DepartmentCode,
              DepartmentName: item.DepartmentName,
            });
          });
        })
        .catch((err) => {
          vm.errorHandler(err);
        });
    },

    /**
     * Reload bảng dữ liệu sau khi xóa
     * CreatedBy: PHDUONG(30/07/2021)
     */
    btnReloadOnClick() {
      this.employeesData = [];
      this.getEmployeePagingData(
        this.pageIndex,
        this.pageSize,
        this.employeeFilter
      );
    },

    /**
     * Lấy dữ liệu nhân viên từ Api
     * CreatedBy: PHDUONG(29/08/2021)
     */
    getEmployeePagingData(pageIndex, pageSize, employeeFilter) {
      var vm = this;

      vm.isLoading = true;

      EmployeeAPI.paging(pageIndex, pageSize, employeeFilter)
        .then((res) => {
          vm.employeesData = res.data.data ? res.data.data : [];

          vm.isLoading = false;
          vm.pageIndex = pageIndex;

          if (res.data) {
            vm.totalRecord = res.data.totalRecord;
          } else {
            vm.totalRecord = 0;
          }
          if (res.status != 204) {
            this.toastList.push({
              type: ToastMessage.Type.Success,
              message: ToastMessage.Message.LoadSuccess,
            });
          }
        })
        .catch((err) => {
          vm.errorHandler(err);
        });
    },

    /**
     * Lấy dữ liệu employee qua filter
     * CreatedBy: PHDUONG(01/09/2021)
     */
    filterDataTable(value) {
      clearTimeout(this.timeoutItem);
      this.timeoutItem = setTimeout(() => {
        this.employeeFilter = value;
        this.getEmployeePagingData(
          this.pageIndex,
          this.pageSize,
          this.employeeFilter
        );
      }, 500);
    },

    exportData() {
      EmployeeAPI.export(this.pageIndex, this.pageSize, this.employeeFilter)
        .then((res) => {
          if (res) {
            const blob = new Blob([res.data], {
              type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            });

            const downloadLink = document.createElement("a");
            downloadLink.href = URL.createObjectURL(blob);
            downloadLink.download = "Danh sách nhân viên";
            downloadLink.click();
            URL.revokeObjectURL(downloadLink.href);
          }
          this.toastList.push({
            type: ToastMessage.Type.Success,
            message: ToastMessage.Message.ExportSuccess,
          });
        })
        .catch((err) => {
          this.errorHandler(err);
        });
    },
    /**
     * Xử lý hiển thị lỗi khi gọi api
     * CreatedBy: PHDUONG(01/09/2021)
     */
    errorHandler(err) {
      if (err.response && err.response.status < 500 && err.response.status >= 400) {
        let msg = err.response.data.userMsg;
        this.toastList.push({
          type: ToastMessage.Type.Error,
          message: msg,
        });
      }

      if (err.response && err.response.status >= 500) {
        this.toastList.push({
          type: ToastMessage.Type.Error,
          message: ToastMessage.Message.ServerError,
        });
      }

      if (err) {
        this.toastList.push({
          type: ToastMessage.Type.Error,
          message: err,
        });
      }
    },

    /**
     * Hiển thị button delete và checkbox
     * CreatedBy: PHDUONG(31/08/2021)
     */
    checkBoxOnClick(checkedId) {
      if (checkedId.length > 0) {
        // debugger;
        this.employeesToDelete = [];

        for (let index = 0; index <= checkedId.length - 1; index++) {
          this.employeesToDelete.push({
            id: checkedId[index].id,
            code: checkedId[index].code,
          });
        }
      } else {
        this.employeesToDelete = [];
      }
    },

    /**
     * Mở popup khi chọn xóa nhân viên
     * CreatedBy: PHDUONG(31/08/2021)
     */
    deleteSelectedRows(employeeId, employeeCode) {
      if (employeeId || this.employeesToDelete.length == 1) {
        if (employeeId) {
          this.employeesToDelete.push({
            id: employeeId,
            code: employeeCode,
          });
        }
        let message = `Bạn có chắc chắn muốn xóa Nhân viên < ${
          employeeCode ? employeeCode : this.employeesToDelete[0].code
        } > không?`;

        this.setPopup(
          message,
          "mi-warning",
          "Không",
          null,
          "Có",
          null,
          this.deleteById,
          null
        );
      } else if (this.employeesToDelete.length > 1) {
        let message = `Bạn có chắc chắn muốn xóa các nhân viên này không?`;

        this.setPopup(
          message,
          "mi-warning",
          "Không",
          null,
          "Có",
          null,
          this.deleteByListId,
          null
        );
      } else {
        this.employeesToDelete = [];
        let msg = ErrorMessage["DeleteListEmpty"];
        this.setPopup(msg, "mi-warning", null, null, null, "Đóng", null, null);
      }
    },

    /**
     * Xóa theo id
     * CreatedBy: PHDUONG(01/09/2021)
     */
    deleteById() {
      EmployeeAPI.getById(this.employeesToDelete[0].id)
        .then((res) => {
          if (res.status != 204) {
            EmployeeAPI.delete(this.employeesToDelete[0].id)
              .then((res) => {
                if (res.status != 204) {
                  this.toastList.push({
                    type: ToastMessage.Type.Success,
                    message: ToastMessage.Message.DeleteSuccess,
                  });
                }
                this.employeesToDelete = [];
                this.btnReloadOnClick();
              })
              .catch((err) => {
                this.errorHandler(err);
              });
          } else if(res.status == 204) {
            let msg =
              "Dữ liệu này không còn tồn tại trên hệ thống, vui lòng load lại trang !";
            this.setPopup(
              msg,
              "mi-warning",
              null,
              null,
              null,
              "Đóng",
              null,
              null
            );
          }
        })
        .catch((err) => {
          this.errorHandler(err);
        });
    },

    /**
     * Xóa theo list id
     * CreatedBy: PHDUONG(01/09/2021)
     */
    deleteByListId() {
      var listId = [];
      this.employeesToDelete.forEach((data) => {
        listId.push(data.id);
      });

      EmployeeAPI.deleteList(listId)
        .then((res) => {
          if (res.status != 204) {
            this.toastList.push({
              type: ToastMessage.Type.Success,
              message: ToastMessage.Message.DeleteSuccess,
            });
          }
          this.employeesToDelete = [];
          this.btnReloadOnClick();
        })
        .catch((err) => {
          this.errorHandler(err);
        });
    },

    /**
     * Hàm mở form thêm sửa
     * CreatedBy: PHDUONG(30/09/2021)
     */
    openForm(employeeId, mode) {
      if (employeeId) {
        this.mode = mode;
        this.$refs.Modal.openForm(employeeId, mode);
      } else {
        this.mode = mode;
        this.$refs.Modal.openForm("", mode);
      }
    },
    /**
     * Thiết lập popup
     * @param msg: tin nhắn
     * @param icon: tên icon
     * @param btnLeft: tên của nút bấm bên trái -> Đóng form, với giá trị null : ko có.
     * @param btnRightFrist: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành động cancel, với giá trị  null : ko có
     * @param btnRightSec: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành đọng action, với giá trị  null : ko có
     * @param center: tên của nút bấm ở giữa  -> Đóng form ~ thường là message cảnh báo, với giá trị null : ko có
     * @param action: action sẽ thực hiện nếu bấm nút btnRightSec, với giá trị null : ko có
     * @param cancel: thực hiện hành động nếu bấm nút btnRightFirst, với giá trị null : ko có
     * CreatedBy: PHDUONG(02/09/2021)
     */
    setPopup(
      message,
      icon,
      btnLeft = null,
      btnRightFirst = null,
      btnRightSec = null,
      btnCenter = null,
      action = null,
      cancel = null
    ) {
      this.popupInfo = {
        btnLeft: btnLeft,
        btnRightFirst,
        btnRightSec,
        btnCenter,
        isShowed: true,
        icon: icon,
        message,
        action,
        cancel,
      };
    },

    /**
     * Đóng popup
     * CreatedBy: PHDUONG(02/09/2021)
     */
    closePopup() {
      this.popupInfo = {
        btnLeft: null,
        btnRightFirst: null,
        btnRightSec: null,
        btnCenter: null,
        isShowed: false,
        icon: null,
        message: "",
        action: null,
        cancel: null,
      };
    },
  },
  watch: {
    /**
     * Xóa danh sách toastList sau 3 đối với phần tử cuối cùng
     * CreatedBy: PHDUONG(02/09/2021)
     */
    toastList: {
      deep: true,
      immediate: true,
      handler: function () {
        clearTimeout(this.timeoutRemoveToastList);

        this.timeoutRemoveToastList = setTimeout(() => {
          if (this.toastList.length > 0) this.toastList = [];
        }, 2000);
      },
    },
  },
};
</script>

<style scoped>
.content {
  padding: 0 20px;
  background-color: #f4f5f8;
  height: calc(100vh - var(--header-height));
}

.content-main {
  background-color: #fff;
  max-height: calc(100vh - var(--header-height) - 86px);
  padding: 0 16px;
}
</style>