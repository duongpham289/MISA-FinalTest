<template>
  <div class="table-tool items-center">
    <div class="input-search">
      <input
        type="text"
        class="table-tool__search"
        placeholder="Tìm theo mã, tên nhân viên"
      />
      <div class="search-icon">
        <div class="mi mi-24 mi-search"></div>
      </div>
    </div>

    <div class="table-tool__icon"  @click="refreshData">
      <div class="mi mi-24 mi-refresh"></div>
    </div>
    <div class="table-tool__icon" @click="exportData">
      <div class="mi mi-24 mi-excel__nav"></div>
    </div>
    <div class="table-tool__icon"  @click="deleteSelectedRows">
      <div class="mi mi-14 mi-trashbin"></div>
    </div>
  </div>
</template>


<script>
// import Tooltip from "../../components/Tooltip.vue";
export default {
  components: {
    // Tooltip,
  },
  data() {
    return {
      searchFocused: false,
      refreshFocused: false,
      exportFocused: false,
    };
  },
  methods: {
    /**
     * Hàm refresh dữ liệu
     * NVTOAN 05/07/2021
     */
    refreshData() {
      this.$emit("btnReloadOnClick");
    },

    /**
     * Hàm filter dữ liệu trên bảng
     * NVTOAN 09/07/2021
     */
    filterDataTable(val) {
      this.$emit("filterDataTable", val);
    },

    /**
     * Hàm export dữ liệu sang excel
     * NVTOAN 12/07/2021
     */
    exportData() {
      this.$emit("exportData");
    },

    /**
     * Hàm xóa bản ghi đã chọn
     * NVTOAN 15/07/2021
     */
    deleteSelectedRows() {
      this.$emit('deleteSelectedRows');
    }
  },
};
</script>