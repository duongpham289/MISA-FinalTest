<template>
  <div class="modal-container" v-if="showForm">
    <div class="modal-custom">
      <div class="modal-header items-center">
        <div class="modal-header__title">Thông tin nhân viên</div>
        <div class="modal-checkbox">
          <div class="modal-checkbox__content">
            <input class="checkbox" type="checkbox" />
            <span class="checkmark">
              <div class="mi mi-16 mi-checkbox-active"></div
            ></span>
          </div>
          <div class="modal-checkbox__title">Là khách hàng</div>
        </div>
        <div class="modal-checkbox items-center">
          <div class="modal-checkbox__content">
            <input class="checkbox" type="checkbox" />
            <span class="checkmark">
              <div class="mi mi-16 mi-checkbox-active"></div
            ></span>
          </div>
          <div class="modal-checkbox__title">Là nhà cung cấp</div>
          <div class="modal__button-close" @click="closeForm">
            <div class="mi mi-24 mi-close"></div>
          </div>
        </div>
      </div>
      <div class="modal-content">
        <div class="content-body">
          <div class="d-flex p-b--18">
            <div class="w-50 p-r--26">
              <div class="d-flex row-input">
                <div class="w-40 p-r--6">
                  <base-input
                    ref="EmployeeCode"
                    label="Mã"
                    id="EmployeeCode"
                    placeholder="NV8888..."
                    :value="employee.EmployeeCode"
                    required
                  >
                  </base-input>
                </div>
                <div class="w-60">
                  <base-input
                    ref="FullName"
                    label="Tên"
                    id="FullName"
                    placeholder="Nguyễn Văn A..."
                    :value="employee.FullName"
                    required
                  />
                </div>
              </div>
              <div class="row-input">
                <div class="w-100 p-b--4">
                  <combo-box
                    label="Đơn vị"
                    :required="true"
                    :data="departmentCbb"
                    :value="employee.DepartmentId"
                    id="DepartmentId"
                  >
                    <template #combo-box-options="{ options }">
                      <combo-box-option
                        v-for="item in options"
                        :key="item.DepartmentId"
                        :dataSrc="item"
                        :checked="item.DepartmentId === department"
                        @select-item="selectItem"
                      ></combo-box-option>
                    </template>
                  </combo-box>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    label="Chức danh"
                    id="PositionName"
                    placeholder=""
                    :value="employee.PositionName"
                  />
                </div>
              </div>
            </div>
            <div class="w-50">
              <div class="d-flex row-input">
                <div class="w-40 p-r--6">
                  <base-input id="DateOfBirth" :hasLabel="true" label="Ngày sinh"></base-input>
                </div>
                <div class="w-60">
                  <div class="flex flex-column">
                    <div class="label p-l--10">Giới tính</div>
                    <div class="p-l--10 p-t--5 p-b--6">
                      <label
                        class="container-radio"
                        for="0"
                        @click="onChangeInput({ value: 0, id: 'Gender' })"
                      >
                        <input label="0" type="radio" value="0" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="employee.Gender === 0"
                          ></span>
                        </span>
                        <span class="label-radio">Nữ</span>
                      </label>
                      <label
                        class="container-radio"
                        for="1"
                        @click="onChangeInput({ value: 1, id: 'Gender' })"
                      >
                        <input type="radio" label="1" :value="1" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="employee.Gender === 1"
                          ></span>
                        </span>
                        <span class="label-radio">Nam</span>
                      </label>
                      <label
                        class="container-radio"
                        for="2"
                        @click="onChangeInput({ value: 2, id: 'Gender' })"
                      >
                        <input type="radio" label="2" :value="2" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="employee.Gender === 2"
                          ></span>
                        </span>
                        <span class="label-radio">Khác</span>
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="d-flex row-input">
                <div class="w-60 p-r--6">
                  <base-input
                    label="Số CMTND"
                    id="IdentityNumber"
                    type="number"
                    placeholder="0123456789..."
                    :value="employee.IdentityNumber"
                  />
                </div>
                <div class="w-40">
                  <base-input id="IdentityDate" :hasLabel="true" label="Ngày cấp"></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    label="Nơi cấp"
                    id="IdentityPlace"
                    type="text"
                    placeholder="Hà Nội..."
                    :value="employee.IdentityPlace"
                  />
                </div>
              </div>
            </div>
          </div>
          <div>
            <div class="row-input">
              <div class="w-100">
                <base-input
                  label="Địa chỉ"
                  id="Address"
                  type="text"
                  placeholder="Hà Nội..."
                  :value="employee.Address"
                />
              </div>
            </div>

            <div class="d-flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  label="ĐT di động"
                  id="MobilePhoneNumber"
                  placeholder="0123456789..."
                  :value="employee.MobilePhoneNumber"
                />
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  label="ĐT cố định"
                  id="TelephoneNumber"
                  placeholder="0123456789..."
                  :value="employee.TelephoneNumber"
                />
              </div>
              <div class="w-25">
                <base-input
                  label="Email"
                  id="Email"
                  type="email"
                  placeholder="example@gmail.com..."
                  :value="employee.Email"
                />
              </div>
            </div>

            <div class="d-flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  label="Tài khoản ngân hàng"
                  id="BankAccount"
                  type="number"
                  placeholder="0123456789..."
                  :value="employee.BankAccount"
                />
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  label="Tên ngân hàng"
                  id="BankName"
                  type="text"
                  placeholder="ACB..."
                  :value="employee.BankName"
                />
              </div>
              <div class="w-25">
                <base-input
                  label="Chi nhánh"
                  id="BankBranch"
                  type="text"
                  placeholder="Cầu Giấy..."
                  :value="employee.BankBranch"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EmployeeModel from "@/models/EmployeeModel.js";
import EmployeesAPI from "@/api/components/EmployeesAPI.js";

export default {
  components: {
  },
  props: {
    departmentCbb: {
      type: Array,
      required: true
    },
  },
  data() {
    return {
      showForm: false,
      employee: EmployeeModel.initData(),
      originData: {},
      department: "1",
      employeeId: null,
      formType: null, // 0: thêm, 1 : sửa, 2 : nhân bản
    };
  },
  methods: {
    /**
     * Xử lý chọn option trong cac combobox
     * CreateBy: NHHoang(28/08/2021)
     */
    selectItem({ id, value }) {
      this.employee[id] = value;
    },
    /**
     * Hàm mở form
     * NVTOAN 06/07/2021
     */
    openForm(employeeId,employee) {
      //Gán lại giá trị của form
      // await Object.assign(this.$data, initState(false));
      this.showForm = true;

      //Nếu là form sửa
      if (employeeId.length > 0) {
        //Xác định formType
        this.employeeId = employeeId;
        this.formType = 1;

         EmployeesAPI.getById(employeeId)
          .then((response) => {
            this.employee = response.data;
          })
          .catch(() => {
            //Nếu không lấy được dữ liệu từ db
            this.allInputValid = false;
            // this.errorMessage = Resource.Message.ServerError;
            this.showErrorPopup = true;
          });
      } else {
        //Xác định formType
        this.formType = 0;

        //Nếu có employee thì là nhân bản
        if (employee) {
          //Xác định formType
          this.formType = 2;

          this.employee = JSON.parse(JSON.stringify(employee));
          this.employee.employeeName = "";
        }

         EmployeesAPI.getNewCode()
          .then((response) => {
            this.employee.EmployeeCode = response.data;
          })
          .catch(() => {
            //Nếu không lấy được mã nhân viên mới
            this.allInputValid = false;
            // this.errorMessage = Resource.Message.GetNewEmployeeCodeError;
            this.showErrorPopup = true;
          });
      }

      //Lấy dữ liệu gốc để đối chiếu xem người dùng đã thay đổi dữ liệu chưa
      // this.originData = JSON.parse(JSON.stringify(this.employee));
    },

    /**
     * xử lý onchangeinput
     */
    onChangeInput({ value, id }) {
      this.employee[id] = value;
    },

    /**
     * Hàm đóng form
     * NVTOAN 07/06/2021
     */
    closeForm() {
      // if (this.dataChanged()) {
      //   this.showConfirmPopup = true;
      // } else {
      this.showForm = false;
      // }
    },
  },
};
</script>