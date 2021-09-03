<template>
  <div class="content-footer items-center" v-if="employeesData.length > 0">
    <div class="record-display">
      Tổng số: &nbsp;<span class="text-semibold"> {{ totalRecord }}</span
      >&nbsp; bản ghi
    </div>

    <div class="paginate items-center">
      <multiselect
        class="custom-select-paging w-200"
        :value="pagingValue"
        @select="changePageSize"
        :options="pagingOptions"
        :searchable="false"
        :close-on-select="false"
        :show-labels="false"
      ></multiselect>
      <b-pagination
        class="paginate-custom"
        :value="pageIndex"
        @change="changePageIndex"
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
</template>

<script>
export default {
  name: "base-paginate",
  data() {
    return {
      pagingValue: "20 nhân viên/trang",
      pagingOptions: [
        "10 nhân viên/trang",
        "20 nhân viên/trang",
        "30 nhân viên/trang",
      ],
    };
  },
  props: {
    employeesData: {
      type: Array,
      required: true,
    },
    pageIndex: {
      type: Number,
      required: true,
    },
    totalRecord: {
      type: Number,
      required: true,
    },
    pageSize: {
      type: Number,
      required: true,
    },
    employeeFilter: {
      type: String,
      required: true,
    },
  },
  methods: {
    /**
     * Bắt sự kiện thay đổi pageIndex
     * CreatedBy: PHDUONG(31/08/2021)
     */
    changePageIndex(value) {
      this.$emit("changePageIndex", value, this.pageSize, this.employeeFilter);
    },

    /**
     * Bắt sự kiện thay đổi pageSize
     * CreatedBy: PHDUONG(01/09/2021)
     */
    changePageSize(value) {
      this.pagingValue = value;

      this.$emit(
        "changePageSize",
        this.pageIndex,
        value.substring(0, 2),
        this.employeeFilter
      );
    },
  },
};
</script>