<template>
  <div class="table-tool items-center">
    <div class="input-search">
      <input
        autofocus
        type="text"
        class="table-tool__search"
        placeholder="Tìm theo mã, tên nhân viên"
        @input="filterDataTable"
      />
      <div class="search-icon">
        <div class="mi mi-24 mi-search"></div>
      </div>
    </div>

    <div class="table-tool__icon" @click="refreshData">
      <div class="mi mi-24 mi-refresh"></div>
    </div>
    <div class="table-tool__icon" @click="exportData">
      <div class="mi mi-24 mi-excel__nav"></div>
    </div>
    <div class="table-tool__icon" @click="deleteSelectedRows">
      <div class="mi mi-14 mi-trashbin"></div>
    </div>
  </div>
</template>


<script>
export default {
  methods: {
    /**
     * Hàm refresh dữ liệu
     * CreatedBy:PHDUONG(31/08/2021)
     */
    refreshData() {
      this.$emit("btnReloadOnClick");
    },

    /**
     * Hàm filter dữ liệu trên bảng
     * CreatedBy:PHDUONG(02/09/2021)
     */
    filterDataTable(event) {
      this.$emit("filterDataTable", event.target.value);
    },

    /**
     * Hàm export dữ liệu sang excel
     * CreatedBy:PHDUONG(02/09/2021)
     */
    exportData() {
      this.$emit("exportData");
    },

    /**
     * Hàm xóa bản ghi đã chọn
     * CreatedBy:PHDUONG(01/09/2021)
     */
    deleteSelectedRows() {
      this.$emit("deleteSelectedRows");
    },
  },
};
</script>