<template>
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
      </tr>
    </thead>
    <tbody class="table-body">
      <tr
        v-for="item in data"
        :key="item[columns[0].id]"
        @dblclick="dbClickRow(item)"
      >
        <td>
          <div class="delete-box">
            <input
              class="checkbox"
              type="checkbox"
              v-model="checkedId"
              :value="{ id: item[columns[0].id], code: item[columns[0].name] }"
            />
            <span class="checkmark"
              ><div class="mi mi-16 mi-checkbox-active"></div
            ></span>
          </div>
        </td>
        <td v-for="col in columns" :key="col.name" :class="col.className">
          {{ formatText(col, item[col.name]) }}
        </td>
      </tr>
    </tbody>
  </table>
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
      checked: 0,
      // isCheckAll: false
    };
  },
  methods: {
    /**
     * Mở modal khi double click
     * CreatedBy: PHDUONG(30/08/2021)
     */
    dbClickRow(item) {
      this.$emit("dbClickRow", item);
    },

    /**
     * Thay đổi màu của row khi click vào checkbox
     * CreatedBy: PHDUONG(30/08/2021)
     */
    changeColorOnClick(id) {
      var isTrue = false;
      this.checkedId.find((item) => {
        if (item.id === id) {
          isTrue = true;
        }
      });
      return isTrue || this.isCheckAll;
    },

    // /**
    //  * Chọn tất cả bản ghi
    //  * CreatedBy: PHDUONG(19/08/2021)
    //  */
    // checkAll() {
    //   var vm = this;
    //   vm.checked++;
    //   this.isCheckAll = !this.isCheckAll;
    //   if (vm.checked == 1) {
    //     $(".checkbox").prop("checked", true);
    //     for (let index = 0; index <= vm.data.length - 1; index++) {
    //       vm.checkedId.push({
    //         id: vm.data[index].id,
    //         code: vm.data[index].name,
    //       });
    //     }
    //     vm.$emit("checkBoxOnClick", vm.checkedId);
    //   } else {
    //     $(".checkbox").prop("checked", false);
    //     vm.checkedId = [];
    //     vm.$emit("checkBoxOnClick", vm.checkedId);
    //     vm.checked = 0;
    //   }
    // },

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