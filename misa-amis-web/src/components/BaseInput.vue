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
        class="input"
        :type="type"
        :max="maxDate"
        :placeholder="placeholder"
        :maxlength="maxLength"
        :value="attachValue"
        @input="onChangeInput"
      />
    </div>
  </div>
</template>

<script>
// import FormatData from "@/utils/format/FormatData.js";

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
    };
  },

  computed: {
    attachValue: function () {
      return this.value;
    },
  },

  methods: {
    /**
     * Xử lý thay đổi dữ liệu
     * CreatedBy: PHDUONG(30/08/2021)
     */
    onChangeInput(event) {
      let tmp = event.target.value;
      this.$emit("handleInput", { id: this.id, value: tmp });
    },
  },
};
</script>