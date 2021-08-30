<template>
    <div class="content__header">
        <div class="header__title">Nhân viên</div>
        <div class="button__container">
          <div class="button--primary button__icon" id="button__add-employee" @click="openForm">
            <!-- <div class="icon-default -add"></div> -->
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
         * NVTOAN 13/06/2021
         */
        openForm() {
            this.$emit('openForm', null);
        },
    },
}
</script>