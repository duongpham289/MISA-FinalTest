<template>
  <div class="content__header">
    <div class="header__title">Nhân viên</div>
    <div class="button__container">
      <div
        class="misa-button misa-button-primary text-semibold"
        id="button__add-employee"
        @click="openForm"
      >
        <div class="button__text">Thêm mới nhân viên</div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  methods: {
    /**
     * Hàm gọi cha mở form thêm sửa
     * CreatedBy:PHDUONG(31/08/2021)
     */
    openForm() {
      this.$emit("openForm", null, this.$enum.Add);
    },
  },
};
</script>