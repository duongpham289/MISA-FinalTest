<template>
  <div v-show="info.isShowed">
    <div class="modal-container">
      <div class="pop-up">
        <div class="pop-up__content">
          <div class="icon__msg">
            <div class="mi mi-48" :class="info.icon"></div>
          </div>
          <div class="content__msg">
            {{ info.message }}
          </div>
        </div>
        <div class="pop-up__line"></div>
        <div class="pop-up__footer">
          <div class="left" v-show="info.btnLeft !== null">
            <div
              class="misa-button misa-button-default text-semibold"
              @click="handleCancel"
            >
              {{ info.btnLeft }}
            </div>
          </div>
          <div class="right-sec" v-show="info.btnRightSec !== null">
            <div
              class="misa-button misa-button-primary text-semibold"
              @click="handleAction"
            >
              {{ info.btnRightSec }}
            </div>
          </div>
          <div class="right-first" v-show="info.btnRightFirst !== null">
            <div
              class="misa-button misa-button-default text-semibold"
              @click="$emit('close')"
            >
              {{ info.btnRightFirst }}
            </div>
          </div>
          <div class="center" v-show="info.btnCenter">
            <div
              class="misa-button misa-button-default text-semibold"
              @click="$emit('close')"
            >
              {{ info.btnCenter }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-popup",

  props: {
    info: {
      type: Object,
      required: true,
    },
  },

  emits: ["close"],

  watch: {},

  data() {
    return {
      className: "pop-up pop-up--hidden",
    };
  },

  methods: {
    /**
     * Khi ấn nút done dẽ thực hiện hành động đã được truyền vào
     * CreatedBy: NHHoang (29/08/2021)
     */
    handleAction() {
      if (this.info.action !== null) this.info.saveAndOut();

      this.$emit("close");
    },

    /*
      Khi ấn nút cancel sẽ không thực hành động đẫ được truyền vào
    */
    handleCancel() {
      if (this.info.cancel !== null) this.info.cancel();
      this.$emit("close");
    },
  },
};
</script>
