<template>
  <div class="content">
    <!-- Phần Header của Content chi tiết:
                        - Header Content
                        - Nút thêm nhân viên
                -->

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
        />
      </div>
      <div class="content-footer items-center">
        <div class="record-display">Tổng số: 46 bản ghi</div>
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
    <Modal ref="Modal" :departmentCbb="departmentCbb" />
  </div>
</template>

<script>
import Modal from "../employees/Modal.vue";
import EmployeesAPI from "@/api/components/EmployeesAPI.js";
import DepartmentAPI from "@/api/components/DepartmentAPI.js";

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
      pageSize: 10,
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

      value: "10 nhân viên/trang",
      options: [
        "10 nhân viên/trang",
        "20 nhân viên/trang",
        "30 nhân viên/trang",
      ],

      isHiddenDialogDetail: true,
      modeFormDetail: 0,

      isHiddenPopupMessage: true,
      loading: true,
    };
  },
  methods: {
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
     * Author: PHDUONG(30/07/2021)
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
     * Author: PHDUONG(08/08/2021)
     */
    getEmployeePagingData(pageIndex, pageSize, employeeFilter) {
      var vm = this;
      vm.loading = true;

      EmployeesAPI.paging(pageIndex, pageSize, employeeFilter)
        .then((res) => {
          vm.employeesData = res.data.data;
          vm.pageIndex = pageIndex;

          if (res.data) {
            vm.totalRecord = res.data.totalRecord;
            // vm.totalPage = Math.ceil(vm.totalRecord / vm.pageSize);
            // vm.totalPage = 2;
          } else {
            vm.totalRecord = 0;
            vm.totalPage = 1;
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
     * Author: PHDUONG(29/07/2021)
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
    deleteSelectedRows() {
      console.log(this.employeesToDelete);
    },
    /**
     * Hàm mở form thêm sửa
     * NVTOAN 14/06/2021
     */
    openForm(employee) {
      if (employee) {
        this.$refs.Modal.openForm(employee.EmployeeId);
      } else {
        this.$refs.Modal.openForm("");
      }
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
      this.pageSize = this.value.substring(0,2);

      this.getEmployeePagingData(
        1,
        this.pageSize,
        this.search.employeeFilter
      );
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