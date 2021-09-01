<template>
  <div class="content">
    <!-- Phần Header của Content -->

    <ContentHeader @openForm="openForm" />

    <!-- Phần bảng dữ liệu chính của Content -->
    <div class="content-table__container">
      <ContentToolBar
        @btnReloadOnClick="btnReloadOnClick"
        @deleteSelectedRows="deleteSelectedRows"
      />
      <div class="table-view">
        <base-table
          :columns="columns"
          :data="employeesData"
          @dbClickRow="openForm"
          @checkBoxOnClick="checkBoxOnClick"
          @deleteSelectedRows="deleteSelectedRows"
        />
      </div>
      <div class="content-footer items-center">
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
      @setPopup="setPopup"
      @btnReloadOnClick="btnReloadOnClick"
    />

    <base-popup :info="popupInfo" @close="closePopup"></base-popup>
  </div>
</template>

<script>
import Modal from "../employees/Modal.vue";
import EmployeeAPI from "@/api/components/EmployeeAPI.js";
import DepartmentAPI from "@/api/components/DepartmentAPI.js";
import ErrorMessage from "@/js/resources/ErrorMsg";

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
      this.search.employeeFilter,
      this.search.departmentId
    );
    this.getDropdownData();
  },
  data() {
    return {
      pageIndex: 1,
      pageSize: 20,
      totalRecord: 0,

      isShowToast: false,
      toastMessageState: 0,
      errorMsg: "",
      toastList: [],

      departmentCbb: [],

      search: {
        departmentId: "",
        positionId: "",
        employeeFilter: "",
      },

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
        this.search.employeeFilter
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
          vm.employeesData = res.data.data;
          vm.pageIndex = pageIndex;

          if (res.data) {
            vm.totalRecord = res.data.totalRecord;
          } else {
            vm.totalRecord = 0;
          }

          this.loading = false;
          // if (res.data.data) {
          //   this.setToast("success", "Tải dữ liệu thành công");
          //   // vm.responseHandler(2, "");
          // } else {
          //   this.setToast("info", "Không có dữ liệu");
          //   // vm.responseHandler(6, "");
          // }
        })
        .catch(() => {
          // var message = vm.responseHandler(err);
          // this.setToast("fail", message);
        });
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
        .then(() => {
          // if (res.status != 204) {
          //   this.toastList.push({
          //     type: Resource.ToastType.Success,
          //     message: Resource.ToastMessage.DeleteSuccess,
          //   });
          this.employeesToDelete = [];
          this.btnReloadOnClick();
          // }
        })
        .catch(() => {
          // this.toastList.push({
          //   type: Resource.ToastType.ERROR,
          //   message: Resource.ToastMessage.ServerError,
          // });
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
        .then(() => {
          this.employeesToDelete = [];
          this.btnReloadOnClick();
        })
        .catch(() => {});
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
        this.search.employeeFilter
      );
    },
    value: function () {
      this.pageSize = this.value.substring(0, 2);

      this.getEmployeePagingData(1, this.pageSize, this.search.employeeFilter);
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