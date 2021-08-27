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
          <div class="input-search flex">
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
      </div>
    <div class="paginate">
        <div class="record-display">Tổng số: 46 bản ghi</div>
    </div>
    </div>
  </div>
</template>