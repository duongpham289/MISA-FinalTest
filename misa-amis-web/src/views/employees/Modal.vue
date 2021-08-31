<template>
  <div class="modal-container" v-if="showForm">
    <div class="modal-custom">
      <div class="ms-modal-header items-center">
        <div class="ms-modal-header__title">Thông tin nhân viên</div>
        <div class="modal-checkbox">
          <div class="modal-checkbox__content">
            <input class="checkbox" type="checkbox" />
            <span class="checkmark"> </span>
          </div>
          <div class="modal-checkbox__title">Là khách hàng</div>
        </div>
        <div class="modal-checkbox items-center">
          <div class="modal-checkbox__content">
            <input class="checkbox" type="checkbox" />
            <span class="checkmark"> </span>
          </div>
          <div class="modal-checkbox__title">Là nhà cung cấp</div>
          <div class="modal__button-close" @click="closeForm">
            <div class="mi mi-24 mi-close"></div>
          </div>
        </div>
      </div>
      <div class="ms-modal-content">
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
                    :maxLength="20"
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
                    :maxLength="100"
                    placeholder="Nguyễn Văn A..."
                    :value="employee.FullName"
                    @handleInput="onChangeInput"
                    required
                  />
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <label class="modal__text text-semibold p-b--4">
                    Đơn vị
                    <span class="required">
                      <span class="required-input">*</span>
                    </span>
                  </label>
                  <multiselect
                    class="custom-select-form"
                    v-model="departmentItem"
                    @input="getDepartmentId"
                    :options="departmentList"
                    :searchable="true"
                    :close-on-select="true"
                    :show-labels="false"
                    :allowEmpty="false"
                    :multiple="false"
                    :custom-label="nameWithLang"
                    placeholder
                  >
                    <span
                      slot="beforeList"
                      class="custom-seclect-header d-flex items-center"
                    >
                      <div class="de-code w-20 d-flex flex-center">
                        Mã đơn vị
                      </div>
                      <div class="de-name w-40 d-flex flex-center">
                        Tên đơn vị
                      </div>
                    </span>
                    <span slot="noResult">Không tìm thấy đơn vị</span>
                    <template slot="singleLabel" slot-scope="props">
                      <span>{{ props.option.DepartmentName }}</span>
                    </template>
                    <template slot="option" slot-scope="props">
                      <span class="w-20 d-flex flex-center">{{
                        props.option.DepartmentCode
                      }}</span>
                      <span class="w-40 d-flex flex-center">{{
                        props.option.DepartmentName
                      }}</span>
                    </template>
                  </multiselect>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    label="Chức danh"
                    id="PositionName"
                    placeholder=""
                    :maxLength="100"
                    :value="employee.PositionName"
                    @handleInput="onChangeInput"
                  />
                </div>
              </div>
            </div>
            <div class="w-50">
              <div class="d-flex row-input">
                <div class="w-40 p-r--6">
                  <base-input
                    id="DateOfBirth"
                    type="date"
                    @handleInput="onChangeInput"
                    :value="employee.DateOfBirth"
                    label="Ngày sinh"
                  ></base-input>
                </div>
                <div class="w-60">
                  <div class="flex flex-column">
                    <div class="label p-l--10">Giới tính</div>
                    <div class="p-l--10 p-t--5 p-b--6">
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
                    </div>
                  </div>
                </div>
              </div>
              <div class="d-flex row-input">
                <div class="w-60 p-r--6">
                  <base-input
                    label="Số CMND"
                    id="IdentityNumber"
                    type="text"
                    placeholder="0123456789..."
                    :maxLength="25"
                    :value="employee.IdentityNumber"
                    @handleInput="onChangeInput"
                  />
                </div>
                <div class="w-40">
                  <base-input
                    label="Ngày cấp"
                    id="IdentityDate"
                    type="date"
                    @handleInput="onChangeInput"
                    :value="employee.IdentityDate"
                  ></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    label="Nơi cấp"
                    id="IdentityPlace"
                    type="text"
                    :maxLength="255"
                    placeholder="Hà Nội..."
                    :value="employee.IdentityPlace"
                    @handleInput="onChangeInput"
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
                  :maxLength="255"
                  placeholder="Hà Nội..."
                  :value="employee.Address"
                  @handleInput="onChangeInput"
                />
              </div>
            </div>

            <div class="d-flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  label="ĐT di động"
                  id="MobilePhoneNumber"
                  placeholder="0123456789..."
                  :maxLength="50"
                  :value="employee.MobilePhoneNumber"
                  @handleInput="onChangeInput"
                />
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  label="ĐT cố định"
                  id="TelephoneNumber"
                  placeholder="0123456789..."
                  :maxLength="50"
                  :value="employee.TelephoneNumber"
                  @handleInput="onChangeInput"
                />
              </div>
              <div class="w-25">
                <base-input
                  label="Email"
                  id="Email"
                  type="email"
                  :maxLength="50"
                  placeholder="example@gmail.com..."
                  :value="employee.Email"
                  @handleInput="onChangeInput"
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
                  :maxLength="25"
                  :value="employee.BankAccount"
                  @handleInput="onChangeInput"
                />
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  label="Tên ngân hàng"
                  id="BankName"
                  type="text"
                  :maxLength="100"
                  placeholder="ACB..."
                  :value="employee.BankName"
                  @handleInput="onChangeInput"
                />
              </div>
              <div class="w-25">
                <base-input
                  label="Chi nhánh"
                  id="BankBranch"
                  type="text"
                  :maxLength="100"
                  placeholder="Cầu Giấy..."
                  :value="employee.BankBranch"
                  @handleInput="onChangeInput"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="ms-modal-footer">
          <div
            class="misa-button misa-button-default text-semibold"
            tabindex="0"
            @click="showForm = false"
            @keyup.enter="showForm = false"
          >
            Hủy
          </div>
          <div class="misa-button-group d-flex">
            <div
              class="misa-button misa-button-default text-semibold"
              tabindex="0"
              @click="saveAndOut"
              @keyup.enter="saveAndOut"
            >
              Cất
            </div>
            <div
              class="misa-button misa-button-primary text-semibold"
              tabindex="0"
              @click="saveAndAdd"
              @keyup.enter="saveAndAdd"
            >
              Cất và thêm
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
  components: {},
  props: {
    departmentCbb: {
      type: Array,
      required: true,
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
      departmentList: this.departmentCbb,
      departmentItem: [],
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
     * Hàm thêm và thoát form
     * NVTOAN 08/07/2021
     */
    saveAndOut() {
      console.log(this.employee);
      let vm = this;
      if (vm.mode == 0) {
        EmployeesAPI.create(vm.employee)
          .then(() => {
            vm.btnCancelOnClick();
            setTimeout(function () {
              vm.reloadTable();
            }, 3000);

            // vm.$emit("responseHandler", 3, res);
          })
          .catch((err) => {
            console.log(err);
            // vm.$emit("responseHandler", 1, err);
          });
      } else {
        if (this.checkUpdateData) {
          EmployeesAPI.update(vm.employee.EmployeeId, vm.employee)
            .then(() => {
              vm.btnCancelOnClick();
              setTimeout(function () {
                vm.reloadTable();
              }, 3000);

              // vm.$emit("responseHandler", 4, res);
            })
            .catch(() => {
              // vm.$emit("responseHandler", 1, err);
            });
        } else {
          // vm.$emit("responseHandler", 1, "");
          console.log(1);
        }
      }
      // this.saveData();

      //Nếu thêm thành công
      // if (this.allInputValid) {
      //   this.showForm = false;
      // }
    },

    /**
     * Reload lại bảng
     * Autthor: PHDUONG(2/8/2021)
     */
    reloadTable() {
      this.$emit("btnReloadOnClick");
    },

    nameWithLang({ DepartmentCode, DepartmentName }) {
      return `${DepartmentCode} — [${DepartmentName}]`;
    },
    /**
     * Hàm cất và thêm dữ liệu
     * NVTOAN 08/07/2021
     */

    saveAndAdd() {
      this.saveValidate = true;

      this.saveData();

      //Nếu thêm thành công
      if (this.allInputValid) {
        this.saveValidate = false;
        this.employee = {};

        //Lấy lại mã nhan viên

        EmployeesAPI.getNewCode()
          .then((response) => {
            this.employee.EmployeeCode = response.data;

            //Lấy dữ liệu gốc để đối chiếu xem người dùng đã thay đổi dữ liệu chưa
            this.originData = JSON.parse(JSON.stringify(this.employee));
          })
          .catch(() => {
            //Nếu không lưu được thì thông báo lỗi
            this.allInputValid = false;
            // this.errorMessage = Resource.Message.ServerError;
            this.showErrorPopup = true;
          });
      }
    },
    /**
     * Hàm mở form
     * NVTOAN 06/07/2021
     */
    openForm(employeeId, employee) {
      var vm = this;
      //Gán lại giá trị của form
      vm.employee = EmployeeModel.initData();
      vm.departmentItem = [];
      vm.showForm = true;

      //Nếu là form sửa
      if (employeeId.length > 0) {
        //Xác định formType
        vm.employeeId = employeeId;
        vm.formType = 1;

        EmployeesAPI.getById(employeeId)
          .then((res) => {
            vm.employee = res.data;
            vm.employee.DateOfBirth = vm.$format.formatDate(
              res.data.DateOfBirth,
              true
            );

            vm.employee.IdentityDate = vm.$format.formatDate(
              res.data.IdentityDate,
              true
            );
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        //Xác định formType
        vm.formType = 0;

        //Nếu có employee thì là nhân bản
        if (employee) {
          //Xác định formType
          vm.formType = 2;

          vm.employee = JSON.parse(JSON.stringify(employee));
        }

        EmployeesAPI.getNewCode()
          .then((response) => {
            vm.employee.EmployeeCode = response.data;
          })
          .catch((err) => {
            console.log(err);
          });
      }

      //Lấy dữ liệu gốc để đối chiếu xem người dùng đã thay đổi dữ liệu chưa
      // this.originData = JSON.parse(JSON.stringify(this.employee));
    },

    /**
     * xử lý onchangeinput
     */
    onChangeInput({ value }) {
      console.log(value);
    },
    /**
     * xử lý onchangeinput
     */
    getDepartmentId() {
      this.employee.DepartmentId = this.departmentItem.DepartmentId;
      // this.employee = value;
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
  watch: {
    employee: {
      immediate: true,
      deep: true,
      handler() {
        let index = this.departmentList.find(
          (item) => item.DepartmentId == this.employee.DepartmentId
        );
        this.departmentItem = index ? index : null;
      },
    },
  },
};
</script>