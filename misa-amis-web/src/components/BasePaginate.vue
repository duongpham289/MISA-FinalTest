<template>
  <div class="content-footer items-center" v-if="employeesData.length > 0">
    <div class="record-display">
      Tổng số: &nbsp;<span class="text-semibold"> {{ totalRecord }}</span
      >&nbsp; bản ghi
    </div>

    <div class="paginate items-center">
      <multiselect
        class="custom-select-paging w-200"
        @keyup.native="onKeyup($event, value)"
        v-model="pagingValue"
        :options="pagingOptions"
        :searchable="false"
        :close-on-select="false"
        :show-labels="false"
        :allowEmpty="false"
      ></multiselect>
      <b-pagination
        class="paginate-custom"
        v-model="pageIndex"
        :total-rows="totalRecord"
        :per-page="pageSize"
        prev-text="Trước"
        next-text="Sau"
        limit="4"
        first-number
        last-number
      ></b-pagination>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-paginate",

  props: {
    employeesData: {
      type: Object,
      required: true,
    },
    pagingValue: {
      type: String,
      required: true,
    },
    pagingOptions: {
      type: Object,
      required: true,
    },
    pageIndex: {
      type: String,
      required: true,
    },
    totalRecord: {
      type: String,
      required: true,
    },
    pageSize: {
      type: String,
      required: true,
    },
  },
};
</script>