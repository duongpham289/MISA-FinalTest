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
import EmployeeAPI from "@/api/components/employeeAPI.js";
import DepartmentAPI from "@/api/components/departmentAPI.js";

import ContentHeader from "./ContentHeader.vue";
import ContentToolBar from "./ContentToolBar.vue";
import Modal from "../employees/Modal.vue";
import BasePaginate from "../../components/table/BasePaginate.vue";

import Resources from "@/js/resources/resources";
import { columns } from "./employeeTableCols.js";

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
    btnReloadOnClick(empFilter, notShowToast) {
      this.employeesData = [];
      this.getEmployeePagingData(
        this.pageIndex,
        this.pageSize,
        empFilter === "" ? this.employeeFilter = empFilter : this.employeeFilter,
        notShowToast
      );
    },

    /**
     * Lấy dữ liệu nhân viên từ Api
     * CreatedBy: PHDUONG(29/08/2021)
     */
    getEmployeePagingData(pageIndex, pageSize, employeeFilter, notShowToast) {
      var vm = this;

      vm.isLoading = true;

      EmployeeAPI.paging(pageIndex, pageSize, employeeFilter)
        .then((res) => {
          vm.employeesData = res.data.data ? res.data.data : [];

          vm.isLoading = false;
          vm.pageIndex = pageIndex;

          if (res.data) vm.totalRecord = res.data.totalRecord;
          else vm.totalRecord = 0;

          if (!notShowToast && res.status === Resources.StatusCode["OK"]) {
            this.toastList.push({
              type: Resources.ToastType["Success"],
              message: Resources.ToastMessage["LoadSuccess"],
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

    /**
     * Xuất dữ liệu ra file excel
     * CreatedBy: PHDUONG(03/09/2021)
     */
    exportData() {
      this.isLoading = true;
      EmployeeAPI.export(this.pageIndex, this.pageSize, this.employeeFilter)
        .then((res) => {
          this.isLoading = false;
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
            type: Resources.ToastType["Success"],
            message: Resources.ToastMessage["ExportSuccess"],
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
      this.isLoading = false;
      try {
        if (err.response) {
          switch (err.response.status) {
            case Resources.StatusCode["BadRequest"]:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: err.response.data.userMsg
                  ? err.response.data.userMsg
                  : Resources.ToastMessage["BadRequest"],
              });
              break;
            case Resources.StatusCode.Unauthorized:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: Resources.ToastMessage["Unauthorized"],
              });
              break;
            case Resources.StatusCode.NotFound:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: Resources.ToastMessage["NotFound"],
              });
              break;
            case Resources.StatusCode.MethodNotAllowed:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: Resources.ToastMessage["MethodNotAllowed"],
              });
              break;
            case Resources.StatusCode.ServerError:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: Resources.ToastMessage["ServerError"],
              });
              break;

            default:
              this.toastList.push({
                type: Resources.ToastType["Error"],
                message: err.response.data,
              });
              break;
          }
        }
      } catch (error) {
        this.toastList.push({
          type: Resources.ToastType["Error"],
          message: error,
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

        let message = Resources.PopupMessage["DeleteOne"].replace(
          "EmployeeCode",
          `${employeeCode ? employeeCode : this.employeesToDelete[0].code}`
        );

        this.setPopup(
          message,
          "mi-warning",
          Resources.PoupButton.ButtonDecline,
          null,
          Resources.PoupButton.ButtonYes,
          null,
          this.deleteById,
          null
        );
      } else if (this.employeesToDelete.length > 1) {
        let message = Resources.PopupMessage["DeleteMany"];

        this.setPopup(
          message,
          "mi-warning",
          Resources.PoupButton.ButtonDecline,
          null,
          Resources.PoupButton.ButtonYes,
          null,
          this.deleteByListId,
          null
        );
      } else {
        this.employeesToDelete = [];
        let msg = Resources.PopupMessage["DeleteListEmpty"];
        this.setPopup(
          msg,
          "mi-warning",
          null,
          null,
          null,
          Resources.PoupButton.ButtonClose,
          null,
          null
        );
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
            this.isLoading = true;
            EmployeeAPI.delete(this.employeesToDelete[0].id)
              .then((res) => {
                this.isLoading = false;
                if (res.status != 204) {
                  this.toastList.push({
                    type: Resources.ToastType["Success"],
                    message: Resources.ToastMessage["DeleteSuccess"],
                  });
                }
                this.employeesToDelete = [];
                this.btnReloadOnClick(
                  this.$enum.EmptyFilter,
                  this.$enum.NotShowToast
                );
              })
              .catch((err) => {
                this.errorHandler(err);
              });
          } else if (res.status == 204) {
            let msg = Resources.PopupMessage["UnIdentifyData"];
            this.setPopup(
              msg,
              "mi-warning",
              null,
              null,
              null,
              Resources.PoupButton.ButtonClose,
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

      this.isLoading = true;
      EmployeeAPI.deleteList(listId)
        .then((res) => {
          this.isLoading = false;
          if (res.status != 204) {
            this.toastList.push({
              type: Resources.ToastType["Success"],
              message: Resources.ToastMessage["DeleteSuccess"],
            });
          }
          this.employeesToDelete = [];
          this.btnReloadOnClick(
            this.$enum.EmptyFilter,
            this.$enum.NotShowToast
          );
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