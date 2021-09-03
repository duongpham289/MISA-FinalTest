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
              @click="$emit('close')"
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
              @click="handleCancel"
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

  methods: {
    /**
     * Thực hiện hành động khi ấn done
     * CreatedBy: PHDUONG (01/09/2021)
     */
    handleAction() {
      if (this.info.action !== null) this.info.action();
      this.$emit("close");
    },

    /**
     * Thực hiện hành động khi ấn cancel
     * CreatedBy: PHDUONG (01/09/2021)
     */
    handleCancel() {
      if (this.info.cancel !== null) this.info.cancel();
      this.$emit("close");
    },
  },
};
</script>
