<template>
  <transition name="slide">
    <div
      class="toast-custom"
      :class="{
        'toast-success': toast.type == Resources.ToastType.Success,
        'toast-warning': toast.type == Resources.ToastType.Warning,
        'toast-error': toast.type == Resources.ToastType.Error,
      }"
      :style="{ top: `${10 + ((index * 48 + 5 * index) % 540)}px` }"
      v-show="isShowed"
    >
      <div class="toast__icon">
        <div
          class="mi mi-24"
          :class="{
            'mi-toast-success': toast.type == Resources.ToastType.Success,
            'mi-toast-warning': toast.type == Resources.ToastType.Warning,
            'mi-toast-error': toast.type == Resources.ToastType.Error,
          }"
        ></div>
      </div>
      <div class="toast__message">{{ toast.message }}</div>
    </div>
  </transition>
</template>

<script>
import Resources from "@/js/resources/resources";
export default {
  name: "base-toast-message",

  props: {
    toast: {
      type: Object,
      required: true,
    },
    index: {
      type: Number,
      required: true,
    },
  },

  data() {
    return {
      isShowed: false,
      Resources: Resources,
    };
  },

  created() {
    this.showToast();
  },

  methods: {

    /**
     * Hiện ToastMessage
     * CreatedBy: PHDUONG (01/09/2021)
     */
    showToast() {
      this.isShowed = true;
      setTimeout(() => {
        this.isShowed = false;
      }, 2000);
    },
  },
};
</script>

<style lang="css">

.slide-leave-active,
.slide-enter-active {
  transition: all 0.3s;
}

.slide-enter,
.slide-leave-to {
  transform: translateX(100%);
  opacity: 0;
}
</style>
