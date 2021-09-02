<template>
  <div class="content">
    <!-- Phần Header của Content -->

    <ContentHeader @openForm="openForm" />

    <!-- Phần bảng dữ liệu chính của Content -->
    <div class="content-table__container">
      <ContentToolBar
        @btnReloadOnClick="btnReloadOnClick"
        @deleteSelectedRows="deleteSelectedRows"
        @filterDataTable="filterDataTable"
      />
      <div class="table-view" :style="employeesData.length <= 0 ? 'overflow:hidden':''">
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
          <img
            src="https://actappg1.misacdn.net/img/bg_report_nodata.76e50bd8.svg"
          />
        </div>

        <label>Không có dữ liệu</label>
      </div>
      <div class="content-footer items-center" v-if="employeesData.length > 0">
        <div class="record-display">
          Tổng số: &nbsp;<span class="text-semibold"> {{ totalRecord }}</span
          >&nbsp; bản ghi
        </div>
        <div class="paginate items-center">
          <multiselect
            class="custom-select-paging w-200"
            @keyup.native="onKeyup($event, value)"
            v-model="value"
            :options="options"
            :searchable="false"
            :close-on-select="false"
            :show-labels="false"
            :allowEmpty="false"
          ></multiselect>
          <b-pagination
            class="paginate-custom"
            v-model="pageIndex"
            :total-rows="totalRecord"
            :per-page="pageSize"
            prev-text="Trước"
            next-text="Sau"
            limit="4"
            first-number
            last-number
          ></b-pagination>
        </div>
      </div>
    </div>
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

export default {
  components: {
    Modal,
    ContentHeader,
    ContentToolBar,
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

      value: "20 nhân viên/trang",
      options: [
        "10 nhân viên/trang",
        "20 nhân viên/trang",
        "30 nhân viên/trang",
      ],

      isHiddenDialogDetail: true,

      department: "1",
      mode: 0,

      isHiddenPopupMessage: true,
      loading: true,

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
          console.log(err);
          // vm.responseHandler(1, err);
        });
    },

    /**
     * Reload bảng dữ liệu sau khi xóa
     * CreatedBy: PHDUONG(30/07/2021)
     */
    btnReloadOnClick() {
      // this.employeesToDelete = [];
      this.employeesData = [];
      this.getEmployeePagingData(
        this.pageIndex,
        this.pageSize,
        this.employeeFilter
      );
    },

    /**
     * Lấy dữ liệu nhân viên từ Api
     * CreatedBy: PHDUONG(08/08/2021)
     */
    getEmployeePagingData(pageIndex, pageSize, employeeFilter) {
      var vm = this;
      vm.loading = true;

      EmployeeAPI.paging(pageIndex, pageSize, employeeFilter)
        .then((res) => {
          vm.employeesData = res.data.data ? res.data.data : [];
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

          vm.loading = false;
        })
        .catch((err) => {
          vm.errorHandler(err);
        });
    },

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

    errorHandler(err) {
      if (err.response.status < 500 && err.response.status >= 400) {
        let msg = err.response.data.userMsg;
        this.toastList.push({
          type: ToastMessage.Type.Error,
          message: msg,
        });
      }

      if (err.response.status >= 500) {
        this.toastList.push({
          type: ToastMessage.Type.Error,
          message: ToastMessage.Message.ServerError,
        });
      }
    },

    /**
     * Bắt sự kiện sử dụng phím mũi tên cho dropdown
     * CreatedBy: duylv 29/08/2021
     */
    onKeyup(event, value) {
      let index = this.options.indexOf(value);
      console.log(index);
      if (event.key == "ArrowUp" && index != -1 && index > 0) {
        index--;
      } else if (
        event.key == "ArrowDown" &&
        index != -1 &&
        index < this.options.length - 1
      ) {
        index++;
      }
      this.value = this.options[index];
    },

    /**
     * Hiển thị button delete và checkbox
     * CreatedBy: PHDUONG(29/07/2021)
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

    deleteSelectedRows(employeeId, employeeCode) {
      if (employeeId) {
        this.employeesToDelete.push({
          id: employeeId,
          code: employeeCode,
        });
        let message = `Bạn có chắc chắn muốn xóa Nhân viên < ${employeeCode} > không?`;

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
      } else if (this.employeesToDelete.length > 0) {
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
     * CreatedBy: NHHoang (29/08/2021)
     */
    deleteById() {
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
    },
    /**
     * Xóa theo id
     * CreatedBy: NHHoang (29/08/2021)
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
     * NVTOAN 14/06/2021
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
     * thiết lập popup
     * @param msg: tin nhắn
     * @param icon: tên icon
     * @param btnLeft: tên của nút bấm bên trái -> Đóng form, với giá trị null : ko có.
     * @param btnRightFrist: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành động cancel, với giá trị  null : ko có
     * @param btnRightSec: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành đọng action, với giá trị  null : ko có
     * @param center: tên của nút bấm ở giữa  -> Đóng form ~ thường là message cảnh báo, với giá trị null : ko có
     * @param action: action sẽ thực hiện nếu bấm nút btnRightSec, với giá trị null : ko có
     * @param cancel: thực hiện hành động nếu bấm nút btnRightFirst, với giá trị null : ko có
     * CreatedBy: NHHoang (29/08/2021)
     */
    setPopup(
      message,
      icon,
      btnLef = null,
      btnRightFirst = null,
      btnRightSec = null,
      btnCenter = null,
      action = null,
      cancel = null
    ) {
      this.popupInfo = {
        btnLeft: btnLef,
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
     * đóng popup
     * CreatedBy: NHHoang (29/08/2021)
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
    pageIndex: function () {
      this.getEmployeePagingData(
        this.pageIndex,
        this.pageSize,
        this.employeeFilter
      );
    },
    value: function () {
      this.pageSize = this.value.substring(0, 2);

      this.getEmployeePagingData(1, this.pageSize, this.employeeFilter);
    },
    /**
     * Xóa danh sách toastList sau 3 đối với phần tử cuối cùng.
     * CreatedBy: NHHoang(01/09/2021)
     */

    toastList: {
      deep: true,
      immediate: true,
      handler: function () {
        clearTimeout(this.timeoutRemoveToastList);

        this.timeoutRemoveToastList = setTimeout(() => {
          if (this.toastList.length > 0) this.toastList = [];
        }, 3000);
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