<template>
  <div class="employee-list">
    <div class="content">
      <!-- Phần Header của Content chi tiết:
                        - Header Content
                        - Nút thêm nhân viên
                -->

      <div class="content__header">
        <div class="content__header header__title">Nhân viên</div>
        <div class="button__container">
          <div class="button--primary" id="button__delete-employee">
            <div class="button__text">Xóa nhân viên</div>
          </div>
          <div class="button--primary button__icon" id="button__add-employee">
            <!-- <div class="icon-default -add"></div> -->
            <div class="button__text">Thêm mới nhân viên</div>
          </div>
        </div>
      </div>

      <!-- Phần bảng dữ liệu chính của Content -->
      <div class="content__table-container table__employee">
        <div class="table-tool flex items-center">
          <div class="input-search">
            <input
              class="table-tool__search"
              placeholder="Tìm theo mã, tên nhân viên"
              type="text"
            />
            <div class="search-icon">
              <div class="mi mi-24 mi-search"></div>
            </div>
          </div>

          <div class="table-tool__icon">
            <div class="mi mi-24 mi-refresh"></div>
          </div>
          <div class="table-tool__icon">
            <div class="mi mi-24 mi-excel__nav"></div>
          </div>
        </div>
        <!-- <base-table
          :columns="columns"
          :data="employeesData"
          @rowOnDblClick="rowOnDblClick"
          @checkBoxOnClick="checkBoxOnClick"
        /> -->
        <table class="content__table">
          <thead class="table__header">
            <tr>
              <th>
              </th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
              <th>Mã nhân viên</th>
            </tr>
          </thead>
          <tbody class="table__body">
            <tr>
              <td>
                <div class="delete-box">
                  <input class="checkbox" type="checkbox" />
                  <span class="checkmark"><div class="mi mi-16 mi-checkbox-active"></div></span>
                </div>
              </td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
            </tr>
            <tr>
              <td>
                <div class="delete-box">
                  <input class="checkbox" type="checkbox" />
                  <span class="checkmark"><div class="mi mi-16 mi-checkbox-active"></div></span>
                </div>
              </td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
              <td>Mã nhân viên</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="content-footer flex items-center">
        <div class="record-display flex">Tổng số: 46 bản ghi</div>
        <div class="paginate items-center">
          <b-dropdown
            class="dropdown-custom items-center"
            split
            split-variant="outline-primary"
            :text="'20 bản ghi trên 1 trang'"
          >
            <b-dropdown-item href="#">10 bản ghi trên 1 trang</b-dropdown-item>
            <b-dropdown-item href="#">20 bản ghi trên 1 trang</b-dropdown-item>
            <b-dropdown-item href="#">30 bản ghi trên 1 trang</b-dropdown-item>
          </b-dropdown>
          <b-pagination
            v-model="currentPage"
            :total-rows="rows"
            :per-page="perPage"
            class="paginate-custom"
            prev-text="Trước"
            next-text="Sau"
            limit="4"
            first-number
            last-number
          ></b-pagination>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      rows: 100,
      perPage: 1,
      currentPage: 1,
    };
  },
};
</script>

