<template>
  <div class="input">
    <label v-if="label" class="modal__text text-semibold">
      {{ label }}
      <span v-if="required" class="required">
        <span class="required-input">*</span>
      </span>
    </label>
    <div class="field-input">
      <b-input-group v-if="type === 'date'">
        <b-form-input
          v-model="dateInput"
          type="date"
          ref="InputDate"
          @input="onChangeInput"
        ></b-form-input>
        <b-input-group-append>
          <b-form-datepicker
            v-model="dateInput"
            button-only
            right
            calendar-width="370px"
            label-current-month="Tháng hiện tại"
            label-next-month="Tháng sau"
            label-next-year="Năm sau"
            label-prev-month="Tháng trước"
            label-prev-year="Năm trước"
            selected-variant
            :start-weekday="1"
            @input="onChangeInput"
            weekday-header-format="narrow"
            :max="new Date()"
            :hide-header="true"
            locale="vi"
            label-help
          ></b-form-datepicker>
        </b-input-group-append>
      </b-input-group>
      <input
        v-else
        ref="AutoFocus"
        class="input"
        :type="type"
        :title="title"
        :placeholder="placeholder"
        :maxlength="maxLength"
        :value="value"
        :class="!isValidated ? 'invalid' : ''"
        @input="onChangeInput"
      />
    </div>
  </div>
</template>

<script>
import Resources from "@/js/resources/resources";

export default {
  name: "base-input",

  // #region props
  props: {
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
      isValidated: true,
      title: "",
      dateInput: null,
    };
  },

  watch: {
    value: function () {
      if (this.type == "date") {
        this.dateInput = this.value;
      }
    },
  },

  methods: {
    /**
     * Focus vào ô input
     * CreatedBy: PHDUONG(01/09/2021)
     */
    autoFocus() {
      this.$nextTick(() => {
        this.$refs.AutoFocus.focus();
      });
    },

    /**
     * Xử lý thay đổi dữ liệu khi nhập
     * CreatedBy: PHDUONG(30/08/2021)
     */
    onChangeInput(event) {
      if (this.type == "date") {
        if (new Date(this.dateInput) > new Date()) {
          this.dateInput = this.$format.formatDate(new Date(), true);
        }
        this.$emit("handleInput", { id: this.id, value: this.dateInput });
      } else {
        let tmp = event.target.value;
        this.validateInput(tmp);
        this.$emit("handleInput", { id: this.id, value: tmp });
      }
    },

    /**
     * Xử lý validate dữ liệu
     * CreatedBy: PHDUONG(01/09/2021)
     */
    validateInput(tmp = null) {
      let value = tmp;

      if (value === null) value = this.value;

      if (this.required) {
        if (!value || value == "") {
          this.isValidated = false;
          this.title = Resources.ErrorMessage[this.id];
        } else {
          this.isValidated = true;
          this.title = "";
        }
      }

      return this.isValidated;
    },
  },
};
</script>