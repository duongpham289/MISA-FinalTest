<template>
  <div>
    <table class="table-content">
      <thead class="table-header">
        <tr>
          <th>
            <div class="delete-box">
              <input class="checkbox" type="checkbox" />
              <span class="checkmark"></span>
            </div>
          </th>
          <th v-for="col in columns" :key="col.name" :class="col.className">
            {{ col.label }}
          </th>
          <th>Chức năng</th>
        </tr>
      </thead>
      <tbody class="table-body">
        <tr
          ref="TableRow"
          v-for="item in data"
          :key="item[columns[0].id]"
          @dblclick="dbClickRow(item[columns[0].id])"
        >
          <td>
            <div
              class="delete-box"
              :class="
                checkboxOnclick(item[columns[0].id]) ? 'border-active' : ''
              "
            >
              <input
                ref="TableCheckbox"
                class="checkbox"
                type="checkbox"
                v-model="checkedId"
                :value="{
                  id: item[columns[0].id],
                  code: item[columns[0].name],
                }"
              />
              <span class="checkmark"
                ><div
                  class="mi mi-16 mi-checkbox-active"
                  v-if="checkboxOnclick(item[columns[0].id])"
                ></div
              ></span>
            </div>
          </td>
          <td v-for="col in columns" :key="col.name" :class="col.className">
            {{ formatText(col, item[col.name]) }}
          </td>
          <td :ref="item[columns[0].id]">
            <div class="table-function d-flex flex-center">
              <div
                class="table-function-text p-r--10"
                @click="dbClickRow(item[columns[0].id])"
              >
                Sửa
              </div>
              <div
                class="table-function-dropdown"
                :class="{ active: idDropdown == item[columns[0].id] }"
                @click="
                  dropdownFunction(item[columns[0].id], item[columns[0].name])
                "
              >
                <div class="mi mi-16 mi-arrow-up--blue"></div>
              </div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="table-funcs" ref="TableFunction">
      <div class="clone-btn" @click="cloneEmployee(idDropdown)">Nhân bản</div>
      <div class="delete-btn" @click="deleteRow(idDropdown, codeDropdown)">
        Xóa
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-table",
  props: {
    columns: {
      type: Array,
      required: true,
    },
    data: {
      type: Array,
      required: true,
      default: function () {
        return [];
      },
    },
  },
  data() {
    return {
      checkedId: [],
      idDropdown: null,
      codeDropdown: null,
    };
  },
  methods: {

    /**
     * Xử lý hiển thị icon khi click checkbox
     * CreatedBy: PHDUONG(30/08/2021)
     */
    checkboxOnclick(value) {
      let index = this.checkedId.find((item) => item.id == value);
      return index ? true : false;
    },

    /**
     * Mở modal khi double click
     * CreatedBy: PHDUONG(30/08/2021)
     */
    dbClickRow(employeeId) {
      this.$emit("dbClickRow", employeeId, 1);
      this.$refs["TableFunction"].style.display = "none";
      this.idDropdown = null;
      this.codeDropdown = null;
    },

    /**
     * Mở modal khi clone
     * CreatedBy: PHDUONG(31/08/2021)
     */
    cloneEmployee(employeeId) {
      this.$emit("dbClickRow", employeeId, 2);
      this.$refs["TableFunction"].style.display = "none";
      this.idDropdown = null;
      this.codeDropdown = null;
    },

    /**
     * Mở popup khi chọn xóa
     * CreatedBy: PHDUONG(01/09/2021)
     */
    deleteRow(employeeId, employeeCode) {
      this.$refs["TableFunction"].style.display = "none";
      this.$emit("deleteSelectedRows", employeeId, employeeCode);
    },

    /**
     * Hiển thị các function khi nhấn dropdown trên table
     * CreatedBy: PHDUONG(31/08/2021)
     */
    dropdownFunction(id, code) {
      if (this.idDropdown == id) {
        this.$refs["TableFunction"].style.display = "none";
        this.idDropdown = null;
        this.codeDropdown = null;
      } else {
        this.$refs["TableFunction"].style.display = "block";
        this.idDropdown = id;
        this.codeDropdown = code;
        this.$refs["TableFunction"].style.right =
          this.$refs[id][0].getBoundingClientRect().right - 1750 + "px";
        if (this.$refs[id][0].getBoundingClientRect().top == 893) {
          this.$refs["TableFunction"].style.top =
            this.$refs[id][0].getBoundingClientRect().top - 60 + "px";
        } else {
          this.$refs["TableFunction"].style.top =
            this.$refs[id][0].getBoundingClientRect().top + 40 + "px";
        }
      }
    },

    /**
     * Format dữ liệu trước khi gắn dữ liệu vào bảng
     * CreatedBy: PHDUONG(30/08/2021)
     */
    formatText(col, item) {
      let tmp = item;

      if (col.format === this.$enum.DATE) {
        tmp = this.$format.formatDate(tmp, false);
      }
      if (col.format === this.$enum.GENDER) {
        tmp = this.$format.formatGender(tmp);
      }

      return tmp;
    },
  },

  watch: {
    checkedId: function () {
      this.$emit("checkBoxOnClick", this.checkedId);
    },
  },
};
</script>