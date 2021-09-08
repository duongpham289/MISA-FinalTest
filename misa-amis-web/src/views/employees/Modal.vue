<template>
  <div class="modal-container" v-if="showForm">
    <div class="modal-custom">
      <div class="ms-modal-header items-center">
        <div class="ms-modal-header__title">Thông tin nhân viên</div>
        <div class="modal-checkbox items-center">
          <div
            class="modal-checkbox__content"
            :class="isCustomer ? 'border-active rotate-90' : ''"
          >
            <input class="checkbox" type="checkbox" />
            <span class="checkmark" @click="isCustomer = !isCustomer">
              <div
                class="mi mi-16 mi-checkbox-active rotate-270"
                v-if="isCustomer"
              ></div
            ></span>
          </div>
          <div class="modal-checkbox__title">Là khách hàng</div>
        </div>
        <div class="modal-checkbox items-center">
          <div
            class="modal-checkbox__content"
            :class="isSupplier ? 'border-active rotate-90' : ''"
          >
            <input class="checkbox" type="checkbox" />
            <span class="checkmark" @click="isSupplier = !isSupplier">
              <div
                class="mi mi-16 mi-checkbox-active rotate-270"
                v-if="isSupplier"
              ></div
            ></span>
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
                    :title="
                      departmentInvalid ? 'Đơn vị không được để trống' : ''
                    "
                    :class="departmentInvalid ? 'invalid' : ''"
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
                      <div class="de-code w-20 d-flex flex-center m-l--5">
                        Mã đơn vị
                      </div>
                      <div class="de-name w-40 d-flex pl-20">
                        Tên đơn vị
                      </div>
                    </span>
                    <span slot="noResult">Không tìm thấy đơn vị</span>
                    <template slot="singleLabel" slot-scope="props">
                      <span>{{ props.option.DepartmentName }}</span>
                    </template>
                    <template slot="option" slot-scope="props">
                      <span class="w-20 d-flex flex-center m-l--5">{{
                        props.option.DepartmentCode
                      }}</span>
                      <span class="w-40 d-flex pl-20">{{
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
                        v-for="item in genderList"
                        :key="item.value"
                        class="container-radio"
                        :for="item.value"
                        @click="
                          onChangeInput({ value: item.value, id: 'Gender' })
                        "
                      >
                        <input
                          type="radio"
                          :label="item.value"
                          :value="item.value"
                        />
                        <span class="radio">
                          <span
                            class="radio-border"
                            :class="{ checked: employee.Gender === item.value }"
                          ></span>
                          <span
                            class="radio-circle"
                            v-show="employee.Gender === item.value"
                          ></span>
                        </span>
                        <span class="label-radio">{{ item.name }}</span>
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
                  :maxLength="50"
                  :value="employee.MobilePhoneNumber"
                  @handleInput="onChangeInput"
                />
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  label="ĐT cố định"
                  id="TelephoneNumber"
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
              @click="saveData(0)"
              @keyup.enter="saveData(0)"
            >
              Cất
            </div>
            <div
              class="misa-button misa-button-primary text-semibold"
              tabindex="0"
              @click="saveData(1)"
              @keyup.enter="saveData(1)"
            >
              Cất và thêm
            </div>
          </div>
        </div>
      </div>
    </div>
    <base-spinner :isLoading="isLoading" />
  </div>
</template>

<script>
import EmployeeModel from "@/models/EmployeeModel.js";
import EmployeeAPI from "@/api/components/employeeAPI.js";

import Resources from "@/js/resources/resources";

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
      isLoading: false,

      employee: EmployeeModel.initData(),
      empCode: null,
      empCodeWhenUpdate: null,

      departmentList: this.departmentCbb,
      departmentItem: [],
      departmentTitleInvalid: "",

      isCustomer: false,
      isSupplier: false,
      isFormChanged: false,

      hasError: false,
      departmentInvalid: false,

      genderList: Resources.GenderList,
    };
  },
  methods: {
    /**
     * Hàm mở form
     * CreatedBy: PHDUONG (30/08/2021)
     */
    openForm(employeeId, mode) {
      var vm = this;

      //Gán lại giá trị của employee
      vm.employee = EmployeeModel.initData();
      vm.departmentItem = [];

      vm.departmentInvalid = false;
      vm.showForm = true;

      //Nếu là form sửa
      if (employeeId.length > 0) {
        vm.isLoading = true;
        EmployeeAPI.getById(employeeId)
          .then((res) => {
            vm.isLoading = false;
            vm.employee = res.data;

            vm.empCode = vm.employee.EmployeeCode;
            vm.empCodeWhenUpdate = vm.employee.EmployeeCode;

            vm.employee.DateOfBirth = vm.$format.formatDate(
              vm.employee.DateOfBirth,
              this.$enum.OnModal
            );

            vm.employee.IdentityDate = vm.$format.formatDate(
              vm.employee.IdentityDate,
              this.$enum.OnModal
            );

            vm.$refs.EmployeeCode.autoFocus();
          })
          .catch((err) => {
            vm.$emit("errorHandler", err);
          });
      }
      if (mode != this.$enum.Update) {
        vm.isLoading = true;
        EmployeeAPI.getNewCode()
          .then((response) => {
            vm.isLoading = false;
            vm.employee.EmployeeCode = response.data;
            vm.empCode = vm.employee.EmployeeCode;
            vm.$refs.EmployeeCode.autoFocus();
          })
          .catch((err) => {
            vm.$emit("errorHandler", err);
          });
      }
    },

    /**
     * Hàm lưu dữ liệu
     * CreatedBy: PHDUONG (30/08/2021)
     */
    async saveData(saveMode) {
      let vm = this;

      if (!saveMode) {
        saveMode = vm.$enum.Add;
      }

      var isCodeValid = await vm.checkDuplicate();
      var isInputValid = vm.validateForm();

      if (isCodeValid && isInputValid && !vm.departmentInvalid) {
        if (vm.mode != this.$enum.Update) {
          vm.isLoading = true;

          EmployeeAPI.create(vm.employee)
            .then(() => {
              vm.isLoading = false;

              vm.toastList.push({
                type: Resources.ToastType["Success"],
                message: Resources.ToastMessage["AddSuccess"],
              });

              if (saveMode == this.$enum.Add) {
                vm.closeForm();
              } else vm.openForm("", this.$enum.Add);

              setTimeout(function () {
                vm.reloadTable();
              }, 1000);
            })
            .catch((err) => {
              vm.$emit("errorHandler", err);
            });
        } else if (vm.isFormChanged) {
          vm.isLoading = true;
          EmployeeAPI.update(vm.employee.EmployeeId, vm.employee)
            .then(() => {
              vm.isLoading = false;

              if (saveMode == this.$enum.Add) {
                vm.closeForm();
              } else vm.openForm("", this.$enum.Add);

              vm.toastList.push({
                type: Resources.ToastType["Success"],
                message: Resources.ToastMessage["UpdateSuccess"],
              });

              setTimeout(function () {
                vm.reloadTable();
              }, 1000);
            })
            .catch((err) => {
              vm.$emit("errorHandler", err);
            });
        } else {
          let msg = Resources.PopupMessage["FormNotChanged"];
          vm.$emit(
            "setPopup",
            msg,
            "mi-warning",
            null,
            null,
            null,
            Resources.PoupButton.ButtonClose,
            null,
            null
          );
        }
      }
    },

    /**
     * Reload lại bảng
     * CreatedBy: PHDUONG (01/09/2021)
     */
    reloadTable() {
      this.$emit(
        "btnReloadOnClick",
        this.$enum.EmptyFilter,
        this.$enum.NotShowToast
      );
    },

    /**
     * Hiển thị tên đơn vị khi chọn
     * CreatedBy: PHDUONG (01/09/2021)
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
     * CreatedBy: PHDUONG (01/09/2021)
     */
    closeForm() {
      try {
        Object.keys(this.$refs).forEach(
          (el) => (this.$refs[el].isValidated = true)
        );
        this.isFormChanged = false;
        this.showForm = false;
      } catch (error) {
        this.$emit("errorHandler", error);
      }
    },

    /**
     * Check DuplicateCode
     * CreatedBy: PHDUONG(06/09/2021)
     */
    async checkDuplicate() {
      try {
        let vm = this;

        var isCodeValid = true;
        var res = null;
        var listCode = [];

        if (vm.mode != this.$enum.Update) {
          res = await EmployeeAPI.checkDuplicate(vm.employee.EmployeeCode);
        } else {
          res = await EmployeeAPI.getAllCode();
          listCode = res.data;
          listCode.splice(listCode.indexOf(vm.empCodeWhenUpdate), 1);
        }

        if (res.data == true || listCode.includes(vm.employee.EmployeeCode)) {
          isCodeValid = false;
          let msg = Resources.PopupMessage.EmployeeCodeDuplicate.replace(
            "EmployeeCode",
            `${vm.employee.EmployeeCode}`
          );
          vm.$emit(
            "setPopup",
            msg,
            "mi-warning",
            null,
            null,
            null,
            Resources.PoupButton.ButtonClose,
            null,
            null
          );
        }
        return isCodeValid;
      } catch (error) {
        this.$emit("errorHandler", error);
      }
    },

    /**
     * Validate form
     * CreatedBy: PHDUONG (01/09/2021)
     */
    validateForm() {
      try {
        let vm = this;

        var isInputValid = true;

        Object.keys(vm.$refs).forEach((el) => {
          vm.$refs[el].validateInput();

          if (!vm.$refs[el].isValidated) {
            if (isInputValid === true) {
              let msg = Resources.PopupMessage[vm.$refs[el].id];

              this.$emit(
                "setPopup",
                msg,
                "mi-error",
                null,
                null,
                null,
                Resources.PoupButton.ButtonClose,
                null,
                null
              );
            }
            isInputValid = vm.$refs[el].isValidated;
          }
        });

        if (!vm.departmentItem) {
          vm.departmentInvalid = true;
          let msg = Resources.PopupMessage.DepartmentId;
          vm.$emit(
            "setPopup",
            msg,
            "mi-error",
            null,
            null,
            null,
            Resources.PoupButton.ButtonClose,
            null,
            null
          );
        }

        return isInputValid;
      } catch (error) {
        this.$emit("errorHandler", error);
      }
    },

    /**
     * Xử lý xem data có thay đổi không và hỏi người dùng muốn lưu data ko? = popup
     * CreatedBy: PHDUONG (01/09/2021)
     */
    preCloseForm() {
      if (this.isFormChanged) {
        let msg = Resources.PopupMessage.CloseOnFormChanged;

        this.$emit(
          "setPopup",
          msg,
          "mi-ques",
          Resources.PoupButton.ButtonCancel,
          Resources.PoupButton.ButtonDecline,
          Resources.PoupButton.ButtonYes,
          null,
          this.saveData,
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
          this.departmentInvalid = false;
        }
      },
    },
  },
};
</script>