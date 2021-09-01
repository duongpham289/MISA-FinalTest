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
          <div class="modal__button-close" @click="preCloseForm">
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
                    @handleInput="onChangeInput"
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
                    @input="onChangeInput"
                    :title="isInvalid ? 'Đơn vị không được để trống' : ''"
                    :class="isInvalid ? 'invalid' : ''"
                    :options="departmentList"
                    :searchable="true"
                    :close-on-select="true"
                    :show-labels="false"
                    :allowEmpty="false"
                    :multiple="false"
                    :custom-label="displayName"
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
                        <input label="0" type="radio" :value="0" />
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
                        for="0"
                        @click="onChangeInput({ value: 2, id: 'Gender' })"
                      >
                        <input label="0" type="radio" :value="2" />
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
import EmployeeAPI from "@/api/components/EmployeeAPI.js";
import ErrorMessage from "@/js/resources/ErrorMsg";
import ToastMessage from "@/js/resources/ToastMsg";

export default {
  components: {},
  props: {
    departmentCbb: {
      type: Array,
      required: true,
    },
    mode: {
      type: Number,
      required: true,
    },
    toastList: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      showForm: false,
      employee: EmployeeModel.initData(),
      originData: {},
      employeeId: null,
      empCode: null,
      empCodeWhenUpdate: null,
      formType: null, // 0: thêm, 1 : sửa, 2 : nhân bản
      isFormChanged: false,
      departmentList: this.departmentCbb,
      departmentItem: [],
      departmentTitleInvalid: "",
      isInvalid: false,
      hasError: false,
      isValidated: true,
    };
  },
  methods: {
    /**
     * Hàm mở form
     * PHDUONG 30/08/2021
     */
    openForm(employeeId, mode) {
      var vm = this;
      //Gán lại giá trị của employee
      vm.employee = EmployeeModel.initData();
      vm.departmentItem = [];
      vm.isInvalid = false;
      // vm.hasError= false;
      // vm.isFormChanged= false;

      vm.isValidated = true;
      vm.showForm = true;

      //Nếu là form sửa
      if (employeeId.length > 0) {
        EmployeeAPI.getById(employeeId)
          .then((res) => {
            vm.employee = res.data;
            vm.empCode = vm.employee.EmployeeCode;
            vm.empCodeWhenAdd = vm.employee.EmployeeCode;

            vm.employee.DateOfBirth = vm.$format.formatDate(
              res.data.DateOfBirth,
              true
            );

            vm.employee.IdentityDate = vm.$format.formatDate(
              res.data.IdentityDate,
              true
            );

            vm.$refs.EmployeeCode.autoFocus();
          })
          .catch((err) => {
            this.$emit("errorHandler", err);
          });
      }
      if (mode != 1) {
        EmployeeAPI.getNewCode()
          .then((response) => {
            vm.employee.EmployeeCode = response.data;
            vm.empCode = vm.employee.EmployeeCode;
            vm.$refs.EmployeeCode.autoFocus();
          })
          .catch((err) => {
            this.$emit("errorHandler", err);
          });
      }
    },

    saveData() {
      let vm = this;

      vm.validateForm();

      if (vm.isValidated) {
        vm.hasError = false;
        if (vm.mode != 1) {
          EmployeeAPI.create(vm.employee)
            .then(() => {
              vm.toastList.push({
                type: ToastMessage.Type.Success,
                message: ToastMessage.Message.AddSuccess,
              });
              setTimeout(function () {
                vm.reloadTable();
              }, 1000);
            })
            .catch((err) => {
              this.$emit("errorHandler", err);
            });
        } else if (vm.isFormChanged) {
          console.log(vm.employee);

          EmployeeAPI.update(vm.employee.EmployeeId, vm.employee)
            .then(() => {
              vm.toastList.push({
                type: ToastMessage.Type.Success,
                message: ToastMessage.Message.UpdateSuccess,
              });
              setTimeout(function () {
                vm.reloadTable();
              }, 1000);
            })
            .catch((err) => {
              this.$emit("errorHandler", err);
            });
        } else {
          let msg = ErrorMessage["FormNotChanged"];
          this.$emit(
            "setPopup",
            msg,
            "mi-warning",
            null,
            null,
            null,
            "Đóng",
            null,
            null
          );
        }
      } else {
        EmployeeAPI.getAllCode()
          .then((res) => {
            var listCode = res.data;
            listCode.splice(this.empCodeWhenAdd, 1);
            console.log(listCode.includes(vm.employee.EmployeeCode));
            if (listCode.includes(vm.employee.EmployeeCode)) {
              vm.hasError = true;
              vm.isValidated = false;
              console.log(vm.isValidated);
              let msg = ErrorMessage["EmployeeCodeDuplicate"];
              vm.$emit(
                "setPopup",
                msg,
                "mi-warning",
                null,
                null,
                null,
                "Đóng",
                null,
                null
              );
            }
          })
          .catch((err) => {
            this.$emit("errorHandler", err);
          });
      }
    },

    /**
     * Hàm thêm và thoát form
     * NVTOAN 08/07/2021
     */
    saveAndOut() {
      this.saveData();

      if (this.isValidated && this.isFormChanged && !this.hasError) {
        this.closeForm();
      }
    },
    /**
     * Hàm cất và thêm dữ liệu
     * NVTOAN 08/07/2021
     */

    async saveAndAdd() {
      var vm = this;
      await vm.saveData(); //nếu có lỗi thì sao

      //Nếu thêm thành công
      vm.employee = EmployeeModel.initData();

      //Lấy lại mã nhân viên

      let res = await EmployeeAPI.getNewCode();
      vm.employee.EmployeeCode = res.data;
    },

    /**
     * Reload lại bảng
     * Autthor: PHDUONG(2/8/2021)
     */
    reloadTable() {
      this.$emit("btnReloadOnClick");
    },

    /**
     * Hiển thị tên đơn vị khi chọn
     */
    displayName({ DepartmentName }) {
      return `${DepartmentName}`;
    },

    /**
     * Lưu dữ liệu được nhập vào employee
     * CreatedBy: PHDUONG(31/08/2021)
     */
    onChangeInput({ id, value }) {
      this.isFormChanged = true;
      if (
        this.departmentItem &&
        this.employee.DepartmentId != this.departmentItem.DepartmentId
      ) {
        this.employee.DepartmentId = this.departmentItem.DepartmentId;
      }
      this.employee[id] = value;
    },

    /**
     * Hàm đóng form
     * NVTOAN 07/06/2021
     */
    closeForm() {
      Object.keys(this.$refs).forEach(
        (el) => (this.$refs[el].isValidated = true)
      );
      // this.$emit("mode");
      this.isFormChanged = false;
      this.showForm = false;
    },

    /**
     * validate form
     * CreatedBy: NHHoang (28/08/2021)
     */
    validateForm() {
      let vm = this;

      vm.isValidated = true;
      Object.keys(vm.$refs).forEach((el) => {
        vm.$refs[el].validateInput();

        if (!vm.$refs[el].isValidated) {
          if (vm.isValidated === true) {
            let msg = ErrorMessage[vm.$refs[el].id];

            this.$emit(
              "setPopup",
              msg,
              "mi-error",
              null,
              null,
              null,
              "Đóng",
              null,
              null
            );
          }
          vm.isValidated = vm.$refs[el].isValidated;
        }
      });
      if (!vm.departmentItem) {
        vm.isInvalid = true;
        vm.isValidated = false;
        let msg = ErrorMessage.DepartmentId;
        vm.$emit(
          "setPopup",
          msg,
          "mi-error",
          null,
          null,
          null,
          "Đóng",
          null,
          null
        );
      }

      if (this.empCode != vm.employee.EmployeeCode) {
        var isDuplicated = false;
        this.empCode = vm.employee.EmployeeCode;

        if (this.mode != 1) {
          EmployeeAPI.checkDuplicate(vm.employee.EmployeeCode)
            .then((res) => {
              isDuplicated = res.data;
              if (isDuplicated) {
                vm.hasError = true;
                vm.isValidated = false;
                let msg = ErrorMessage["EmployeeCodeDuplicate"];
                vm.$emit(
                  "setPopup",
                  msg,
                  "mi-warning",
                  null,
                  null,
                  null,
                  "Đóng",
                  null,
                  null
                );
              }
            })
            .catch((err) => {
              this.$emit("errorHandler", err);
            });
        }
        if (this.mode == 1) {
          vm.isValidated = false;
        }
      }
    },

    /**
     * Xử lý xem data có thay đổi không và hỏi người dùng muốn lưu data ko? = popup
     * CreatedBy: NHHoang (29/08/2021)
     */
    preCloseForm() {
      if (this.isValidated && this.isFormChanged) {
        let msg = "Dữ liệu đã bị thay đổi. Bạn có muốn cất không";

        this.$emit(
          "setPopup",
          msg,
          "mi-ques",
          "Hủy",
          "Không",
          "Có",
          null,
          this.saveAndOut,
          this.closeForm
        );
      } else {
        this.closeForm();
      }
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
        if (this.departmentItem) {
          this.isInvalid = false;
        }
      },
    },
  },
};
</script>