<template>
  <div class="input">
    <label v-if="label" class="modal__text text-semibold">
      {{ label }}
      <span v-if="required" class="required">
        <span class="required-input">*</span>
      </span>
    </label>
    <div class="field-input">
      <input
        ref="AutoFocus"
        class="input"
        :type="type"
        :max="maxDate"
        :title="title"
        :placeholder="placeholder"
        :maxlength="maxLength"
        :value="attachValue"
        :class="!isValidated ? 'invalid' : ''"
        @input="onChangeInput"
      />
    </div>
  </div>
</template>

<script>
import ErrorMessage from "@/js/resources/ErrorMsg";

export default {
  name: "base-input",

  // #region props
  props: {
    errorMsg: {
      type: Boolean,
      default: false,
    },
    readonly: {
      type: Number,
      default: 0,
    },
    id: {
      type: String,
      required: true,
    },
    value: {
      type: [String, Number],
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    format: {
      type: String,
      required: false,
    },
    label: {
      type: String,
    },
    placeholder: {
      type: String,
    },
    required: {
      type: Boolean,
    },
    maxLength: {
      type: Number,
    },
  },
  //#endregion

  emits: ["handle-input"],

  data() {
    return {
      maxDate:
        this.type == "date" ? new Date().toISOString().split("T")[0] : null,
      isValidated: true,
      title: "",
    };
  },

  computed: {
    attachValue: function () {
      return this.value;
    },
  },

  methods: {
    autoFocus() {
      this.$nextTick(() => {
        this.$refs.AutoFocus.focus()
      });
    },
    /**
     * Xử lý thay đổi dữ liệu
     * CreatedBy: PHDUONG(30/08/2021)
     */
    onChangeInput(event) {
      let tmp = event.target.value;
      this.validateInput(tmp);
      this.$emit("handleInput", { id: this.id, value: tmp });
    },
    validateInput(tmp = null) {
      let value = tmp;
      if (value === null) value = this.value;

      if (this.required) {
        if (!value || value == "") {
          this.isValidated = false;
          this.title = ErrorMessage[this.id];
        } else {
          this.isValidated = true;
          this.title = "";
        }
      }
      // else {
      //   if (value === null || value?.trim() === "") {
      //     this.isValidated = true;
      //     this.title = "";
      //   } else if ((value !== null) | (value?.trim() !== "")) {
      //     if (this.format === "email") {
      //       if (!Validation.validateEmail(value)) {
      //         this.isValidated = false;
      //         this.title = ErrorMessage[this.id];
      //       } else {
      //         this.isValidated = true;
      //         this.title = "";
      //       }
      //     }

      return this.isValidated;
    },
  },
};
</script>