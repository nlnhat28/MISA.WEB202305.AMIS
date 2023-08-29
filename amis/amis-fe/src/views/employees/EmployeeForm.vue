<template>
  <m-popup
    :title="title"
    @emitClose="onClickCloseForm()"
    @keydown="handleShortKey"
    tabindex="0"
  >
    <template #mask>
      <m-loading v-if="isLoading"></m-loading>
    </template>
    <template #headLeft>
      <div class="checkbox-group">
        <m-checkbox-item
          v-model:checked="employee.IsCustomer"
          name="Là Khách hàng"
        ></m-checkbox-item>
        <m-checkbox-item
          v-model:checked="employee.IsProvider"
          name="Là Nhà cung cấp"
        ></m-checkbox-item>
      </div>
    </template>
    <template #body>
      <m-form-body>
        <!-- form__row--1 -->
        <m-form-row>
          <!-- form__column--1.1 -->
          <m-form-column>
            <m-form-group>
              <!-- Mã -->
              <m-form-item
                :label="fields.employeeCode.label"
                :title="fields.employeeCode.title"
                :isRequired="true"
                style="width: 60%"
              >
                <m-textfield
                  v-model="employee.EmployeeCode"
                  :validate="validateEmployeeCode"
                  :format="formatCode"
                  :isFocused="true"
                  :label="fields.employeeCode.label"
                  :maxLength="fields.employeeCode.maxLength"
                  :action="actionEmployeeCode"
                  ref="EmployeeCode"
                >
                </m-textfield>
              </m-form-item>
              <!-- Tên -->
              <m-form-item
                :label="fields.fullName.label"
                :isRequired="true"
              >
                <m-textfield
                  v-model="employee.FullName"
                  :validate="validateFullName"
                  :label="fields.fullName.label"
                  :maxLength="fields.fullName.maxLength"
                  ref="FullName"
                >
                </m-textfield>
              </m-form-item>
            </m-form-group>
            <!-- Đơn vị -->
            <m-form-item
              :label="fields.department.label"
              :isRequired="true"
            >
              <m-combobox
                :theSelects="departmentsSelects"
                v-model:id="employee.DepartmentId"
                v-model:name="employee.DepartmentName"
                :validate="validateDepartment"
                :label="fields.department.label"
                :maxLength="fields.department.maxLength"
                @emitSelected="this.$refs['Position'].focus()"
                ref="Department"
              >
              </m-combobox>
            </m-form-item>
            <!-- Chức danh -->
            <m-form-item :label="fields.position.label">
              <m-textfield
                v-model="employee.Position"
                :label="fields.position.label"
                :maxLength="fields.position.maxLength"
                ref="Position"
              ></m-textfield>
            </m-form-item>
          </m-form-column>
          <!-- form__column--1.2 -->
          <m-form-column>
            <m-form-group>
              <!-- Ngày sinh -->
              <m-form-item
                :label="fields.dateOfBirth.label"
                style="width: 60%"
              >
                <m-date-picker
                  v-model:dateModel="employee.DateOfBirth"
                  :validate="validateDateOverToday"
                  :label="fields.dateOfBirth.label"
                  :dateFormat="this.dateFormat"
                  ref="DateOfBirth"
                  @emitSelected="this.$refs['Gender'].focus()"
                >
                </m-date-picker>
              </m-form-item>
              <!-- Giới tính -->
              <m-form-item :label="fields.gender.label">
                <m-radio-group
                  v-model:id="employee.Gender"
                  v-model:name="employee.GenderName"
                  :radios="genderSelects"
                  groupName="gender"
                  ref="Gender"
                >
                </m-radio-group>
              </m-form-item>
            </m-form-group>
            <m-form-group>
              <!-- Số CMND -->
              <m-form-item
                :label="fields.identityNumber.label"
                :title="fields.identityNumber.title"
              >
                <m-textfield
                  v-model="employee.IdentityNumber"
                  :format="formatStringBySpace"
                  :label="fields.identityNumber.label"
                  :maxLength="fields.identityNumber.maxLength"
                  ref="IdentityNumber"
                >
                </m-textfield>
              </m-form-item>
              <!-- Ngày cấp CCCD -->
              <m-form-item
                :label="fields.identityDate.label"
                style="width: 60%"
              >
                <m-date-picker
                  v-model:dateModel="employee.IdentityDate"
                  :validate="validateDateOverToday"
                  :label="fields.identityDate.label"
                  ref="IdentityDate"
                  @emitSelected="this.$refs['IdentityPlace'].focus()"
                >
                </m-date-picker>
              </m-form-item>
            </m-form-group>
            <!-- Nơi cấp CCCD -->
            <m-form-item :label="fields.identityPlace.label">
              <m-textfield
                v-model="employee.IdentityPlace"
                :label="fields.identityPlace.label"
                :maxLength="fields.identityPlace.maxLength"
                ref="IdentityPlace"
              >
              </m-textfield>
            </m-form-item>
          </m-form-column>
        </m-form-row>
        <!-- form__row--2 -->
        <m-form-row>
          <!-- form__column--2.1 -->
          <m-form-column>
            <!-- Địa chỉ -->
            <m-form-item :label="fields.address.label">
              <m-textfield
                v-model="employee.Address"
                :label="fields.address.label"
                :maxLength="fields.address.maxLength"
                ref="Address"
              >
              </m-textfield>
            </m-form-item>
            <m-form-group>
              <!-- ĐT đi động -->
              <m-form-item
                :label="fields.phoneNumber.label"
                :title="fields.phoneNumber.title"
              >
                <m-textfield
                  v-model="employee.PhoneNumber"
                  :format="formatPhoneNumber"
                  :label="fields.phoneNumber.label"
                  :maxLength="fields.phoneNumber.maxLength"
                  ref="PhoneNumber"
                >
                </m-textfield>
              </m-form-item>
              <!-- ĐT cố định -->
              <m-form-item
                :label="fields.landlineNumber.label"
                :title="fields.landlineNumber.title"
              >
                <m-textfield
                  v-model="employee.LandlineNumber"
                  :format="formatPhoneNumber"
                  :label="fields.landlineNumber.label"
                  :maxLength="fields.landlineNumber.maxLength"
                  ref="LandlineNumber"
                >
                </m-textfield>
              </m-form-item>
              <!-- Email -->
              <m-form-item :label="fields.email.label">
                <m-textfield
                  v-model="employee.Email"
                  :validate="validateEmail"
                  :label="fields.email.label"
                  :maxLength="fields.email.maxLength"
                  ref="Email"
                >
                </m-textfield>
              </m-form-item>
            </m-form-group>
            <m-form-group>
              <!-- Số tài khoản ngân hàng -->
              <m-form-item :label="fields.bankAccount.label">
                <m-textfield
                  v-model="employee.BankAccount"
                  :label="fields.bankAccount.label"
                  :maxLength="fields.bankAccount.maxLength"
                  :format="formatStringBySpace"
                  ref="BankAccount"
                >
                </m-textfield>
              </m-form-item>
              <!-- Tên ngân hàng -->
              <m-form-item :label="fields.bankName.label">
                <m-textfield
                  v-model="employee.BankName"
                  :label="fields.bankName.label"
                  :maxLength="fields.bankName.maxLength"
                  ref="BankName"
                >
                </m-textfield>
              </m-form-item>
              <!-- Chi nhánh ngân hàng -->
              <m-form-item :label="fields.bankBranch.label">
                <m-textfield
                  v-model="employee.BankBranch"
                  :label="fields.bankBranch.label"
                  :maxLength="fields.bankBranch.maxLength"
                  ref="BankBranch"
                >
                </m-textfield>
              </m-form-item>
            </m-form-group>
          </m-form-column>
        </m-form-row>
      </m-form-body>
    </template>
    <!-- footer's form -->
    <template #footer>
      <div class="button-container">
        <m-button
          :type="this.$enums.buttonType.primary"
          :text="this.$resources['vn'].saveAdd"
          tooltipContent="Ctrl + Shift + S"
          :click="onClickSaveAdd"
        ></m-button>
        <m-button
          :type="this.$enums.buttonType.secondary"
          :text="this.$resources['vn'].save"
          tooltipContent="Ctrl + S"
          :click="onClickSave"
        ></m-button>
      </div>
      <div class="button-container">
        <m-button
          :type="this.$enums.buttonType.secondary"
          :text="this.$resources['vn'].cancel"
          tooltipContent="Esc"
          :click="onClickCloseForm"
          @keydown="handleShortKeyLastButton"
        ></m-button>
      </div>
    </template>
  </m-popup>
  <!-- dialogs -->
  <m-dialog
    :type="this.noticeDialog.type"
    :title="this.noticeDialog.title"
    :content="this.noticeDialog.content"
    @emitCloseDialog="closeNoticeDialog()"
    v-if="this.noticeDialog.isShowed"
  >
    <template #footer>
      <m-button
        :type="this.$enums.buttonType.primary"
        :text="this.$resources['vn'].gotIt"
        :click="closeNoticeDialog"
        ref="buttonFocus"
      ></m-button>
    </template>
  </m-dialog>
  <m-dialog
    :type="this.confirmDialog.type"
    :title="this.confirmDialog.title"
    :content="this.confirmDialog.content"
    @emitCloseDialog="closeConfirmDialog()"
    v-if="this.confirmDialog.isShowed"
  >
    <template #footer>
      <div class="button-container">
        <m-button
          :type="this.$enums.buttonType.primary"
          :text="this.$resources['vn'].save"
          ref="buttonFocus"
          :click="onClickSaveConfirm"
        ></m-button>
        <m-button
          :type="this.$enums.buttonType.secondary"
          :text="this.$resources['vn'].no"
          :click="hideForm"
        ></m-button>
      </div>
      <m-button
        :type="this.$enums.buttonType.secondary"
        :text="this.$resources['vn'].cancel"
        :click="closeConfirmDialog"
      ></m-button>
    </template>
  </m-dialog>
</template>

<script>
import { fields } from "@/js/form/form.js";
import enums from "@/constants/enums.js";
const formMode = enums.formMode;
import {
  validateEmployeeCode,
  validateFullName,
  validateIdentityNumber,
  validateDateOverToday,
  validatePhoneNumber,
  validateDepartment,
  validatePosition,
  validateEmail,
  validateBankAccount,
} from "@/js/form/validate.js";
import {
  formatDate,
  formatPhoneNumber,
  formatStringBySpace,
  formatCode,
} from "@/js/utils/format.js";
import { sortArrayByName } from "@/js/utils/sort.js";
import { removeSpace, reformatDate } from "@/js/utils/clean-format";
import employeeService from "@/services/employee.js";
import departmentService from "@/services/department.js";

export default {
  name: "EmployeeForm",
  props: {
    /**
     * (Props) Employee for update
     */
    theEmployee: {
      type: Object,
      default: {},
    },
    /**
     * Function to hide this form
     */
    hideForm: {
      type: Function,
    },
    /**
     * (Props) Form mode {create | update}
     */
    formMode: {
      type: Number,
      default: formMode.create,
      validator: (value) => {
        return [formMode.create, formMode.update, formMode.duplicate].includes(
          value
        );
      },
    },
  },
  data() {
    return {
      /**
       * Mode of form
       */
      mode: this.formMode,
      /**
       * Fields info
       */
      title: this.formTitle,
      /**
       * Employee show on form
       */
      employee: {},
      /**
       * Id of employee show on form
       */
      employeeId: null,
      /**
       * Fields info
       */
      fields: fields,
      /**
       * Departments list from API
       */
      departments: [],
      /**
       * Departments list show on combobox
       */
      departmentsSelects: [],
      /**
       * Gender list show on combobox
       */
      genderSelects: this.$common.selects.genders,
      /**
       * Dialog notice info
       */
      noticeDialog: {
        type: this.$enums.dialogType.error,
        title: this.$resources["vn"].error,
        content: "",
        isShowed: false,
      },
      /**
       * Dialog confirm info
       */
      confirmDialog: {
        type: this.$enums.dialogType.question,
        title: this.$resources["vn"].saveChange,
        content: "",
        isShowed: false,
      },
      /**
       * Flag check success response
       */
      isSuccessResponseFlag: true,
      /**
       * Message to show on dialog if invalid form
       */
      messageValidate: "",
      /**
       * Focused input
       */
      refFocus: null,
      /**
       * Flag loading
       */
      isLoading: false,
      /**
       * Hành động lấy code mới
       */
      actionEmployeeCode: {
        icon: "refresh--sm",
        title: this.$resources['vn'].getNewCode,
        method: this.getNewEmployeeCode,
      },
      /**
       * Date format
       */
      dateFormat: "dd/MM/yyyy",
    };
  },
  async created() {
    this.title = this.formTitle;
    this.employee = { ...this.theEmployee };
    this.employeeId = this.employee?.EmployeeId;
    this.$emitter.on("focusFormItem", (ref) => {
      this.focusErrorItem(ref);
    });
    this.$emitter.on("setMessageFormItem", (ref, errorMessage) => {
      this.setErrorMessage(ref, errorMessage);
    });
    await this.makeLoadingEffect(async () => {
      await Promise.all([
        this.getDepartments(),
        this.getNewEmployeeCodeOnCreated(),
      ]);
    });
  },
  mounted() {
    this.refs = [
      this.$refs.BankName,
      this.$refs.BankBranch,
      this.$refs.BankAccount,
      this.$refs.Email,
      this.$refs.LandlineNumber,
      this.$refs.PhoneNumber,
      this.$refs.Address,
      this.$refs.IdentityPlace,
      this.$refs.IdentityDate,
      this.$refs.IdentityNumber,
      this.$refs.DateOfBirth,
      this.$refs.Position,
      this.$refs.Department,
      this.$refs.FullName,
      this.$refs.EmployeeCode,
    ];
  },
  emits: [
    "emitReloadData",
    "emitReRenderForm",
    "emitUpdateFocusedId",
  ],
  watch: {
    mode() {
      this.title = this.formTitle;
    },
  },
  computed: {
    /**
     * Check form is valid or invalid
     *
     * Author: nlnhat (02/07/2023)
     * @return {*} True if valid, false if invalid
     */
    isValidForm() {
      try {
        let isValid = true;
        this.refs.forEach((ref) => {
          if (ref.checkValidate() != null) {
            isValid = false;
            ref.focus();
            this.refFocus = ref;
            this.messageValidate = ref.errorMessage;
          }
        });
        return isValid;
      } catch (error) {
        console.error(error);
        return false;
      }
    },
    /**
     * Reformat date before call api (remove format)
     *
     * Author: nlnhat (02/07/2023)
     * @return {*} Employee with reformatted data to save
     */
    reformatEmployee() {
      let employee = { ...this.employee };
      employee.IdentityNumber = removeSpace(employee.IdentityNumber);
      employee.PhoneNumber = removeSpace(employee.PhoneNumber);
      employee.LandlineNumber = removeSpace(employee.LandlineNumber);
      employee.BankAccount = removeSpace(employee.BankAccount);
      employee.DateOfBirth = reformatDate(employee.DateOfBirth);
      employee.IdentityDate = reformatDate(employee.IdentityDate);
      employee.Gender = employee.Gender ?? this.$enums.gender.other.id;
      return employee;
    },
    /**
     * Change title when change mode
     *
     * Author: nlnhat (02/07/2023)
     * @return {*} New title update or create
     */
    formTitle() {
      switch (this.mode) {
        case this.$enums.formMode.create:
          return this.$resources["vn"].createEmployee;
        case this.$enums.formMode.update:
          return this.$resources["vn"].updateEmployee;
        case this.$enums.formMode.duplicate:
          return this.$resources["vn"].duplicateEmployee;
        default:
          return this.$resources["vn"].createEmployee;
      }
    },
  },
  methods: {
    /**
     * Make loading effect
     *
     * Author: nlnhat (05/07/2023)
     * @param {function} func Function executes in loading process
     */
    async makeLoadingEffect(func) {
      try {
        this.isLoading = true;
        await func();
      } catch (error) {
        console.error(error);
      } finally {
        this.isLoading = false;
      }
    },
    /**
     * Get departments
     *
     * Author: nlnhat (02/07/2023)
     */
    async getDepartments() {
      try {
        let selects = [];
        const response = await departmentService.get();
        this.departments = response.data;
        this.departments.forEach((department) => {
          selects.push({
            id: department.DepartmentId,
            name: department.DepartmentName,
          });
        });
        this.departmentsSelects = this.sortArrayByName(selects);
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Get new employee code on created
     *
     * Author: nlnhat (02/07/2023)
     */
    getNewEmployeeCodeOnCreated() {
      try {
        if (
          this.mode == this.$enums.formMode.create ||
          this.mode == this.$enums.formMode.duplicate
        ) {
          this.getNewEmployeeCode();
        }
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * Get new employee code
     *
     * Author: nlnhat (02/07/2023)
     */
    async getNewEmployeeCode() {
      try {
        const response = await employeeService.getNewCode();
        if (response?.status == this.$enums.status.ok) {
          this.employee.EmployeeCode = response.data;
          return true;
        }
      } catch (error) {
        console.error(error);
        return false;
      }
    },
    /**
     * Show notice dialog
     *
     * Author: nlnhat (04/07/2023)
     * @param {string} message Content to show on dialog
     */
    showNoticeDialog(message) {
      this.noticeDialog.content = message;
      this.noticeDialog.isShowed = true;
      this.focusOnButton();
    },
    /**
     * Close notice dialog
     *
     * Author: nlnhat (10/07/2023)
     */
    closeNoticeDialog() {
      try {
        this.noticeDialog.isShowed = false;
        if (this.refFocus) this.refFocus.focus();
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Close notice dialog
     *
     * Author: nlnhat (10/07/2023)
     */
    closeConfirmDialog() {
      this.confirmDialog.isShowed = false;
      this.focusFirstInput();
    },
    /**
     * Focus on button
     *
     * Author: nlnhat (25/07/2023)
     */
    focusOnButton() {
      this.$nextTick(() => {
        this.$refs.buttonFocus.focus();
      });
    },
    /**
     * Create new employee
     *
     * Author: nlnhat (02/07/2023)
     */
    async createEmployee() {
      try {
        const response = await employeeService.post(this.reformatEmployee);
        if (response?.status == this.$enums.status.created) {
          this.employeeId = response.data;
          if (this.mode == this.$enums.formMode.create)
            this.showToastCreateSuccess();
          else if (this.mode == this.$enums.formMode.duplicate)
            this.showToastDuplicateSuccess();
          this.isSuccessResponseFlag = true;
        } else {
          this.isSuccessResponseFlag = false;
        }
      } catch (error) {
        console.error(error);
        this.isSuccessResponseFlag = false;
      }
    },
    /**
     * Update employee
     *
     * Author: nlnhat (02/07/2023)
     */
    async updateEmployee() {
      try {
        const response = await employeeService.put(
          this.employee.EmployeeId,
          this.reformatEmployee
        );
        if (response?.status == this.$enums.status.ok) {
          this.showToastUpdateSuccess();
          this.isSuccessResponseFlag = true;
        } else {
          this.isSuccessResponseFlag = false;
        }
      } catch (error) {
        console.error(error);
        this.isSuccessResponseFlag = false;
      }
    },
    /**
     * Save when create or update
     *
     * Author: nlnhat (02/07/2023)
     */
    async onSave() {
      await this.makeLoadingEffect(async () => {
        switch (this.mode) {
          case this.$enums.formMode.create:
            await this.createEmployee();
            break;
          case this.$enums.formMode.update:
            await this.updateEmployee();
            break;
          case this.$enums.formMode.duplicate:
            await this.createEmployee();
            break;
          default:
            break;
        }
      });
    },
    /**
     * On click button save (Cất)
     *
     * Author: nlnhat (02/07/2023)
     */
    async onClickSave() {
      try {
        if (this.isValidForm) {
          await this.onSave();
          if (this.isSuccessResponseFlag == true) {
            this.$emit("emitUpdateFocusedId", this.employeeId);
            this.$emit("emitReloadData"); 
            this.hideForm();
          }
        } else {
          this.showNoticeDialog(this.messageValidate);
        }
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * On click button save and add (Cất và thêm)
     *
     * Author: nlnhat (02/07/2023)
     */
    async onClickSaveAdd() {
      try {
        if (this.isValidForm) {
          await this.onSave();
          if (this.isSuccessResponseFlag == true) {
            this.$emit("emitReloadData");
            this.resetForm();
          }
        } else {
          this.showNoticeDialog(this.messageValidate);
        }
      } catch (error) {
        console.error(error);
      }
    },
    resetForm() {
      this.$emit("emitReRenderForm");
      this.getNewEmployeeCode();
    },
    /**
     * Show toast message after create success
     *
     * Author: nlnhat (02/07/2023)
     */
    showToastCreateSuccess() {
      const content = `${this.$resources["vn"].created} ${this.$resources["vn"].employee} <${this.employee.EmployeeCode}>`;
      this.$emitter.emit("showToastSuccess", content);
    },
    /**
     * Show toast message after update success
     *
     * Author: nlnhat (02/07/2023)
     */
    showToastUpdateSuccess() {
      const content = `${this.$resources["vn"].updated} ${this.$resources["vn"].employee} <${this.employee.EmployeeCode}>`;
      this.$emitter.emit("showToastSuccess", content);
    },
    /**
     * Show toast message after duplicate success
     *
     * Author: nlnhat (02/07/2023)
     */
    showToastDuplicateSuccess() {
      const content = `${this.$resources["vn"].duplicated} ${this.$resources["vn"].employee} <${this.employee.EmployeeCode}>`;
      this.$emitter.emit("showToastSuccess", content);
    },
    /**
     * Show confirm dialog
     *
     * Author: nlnhat (06/07/2023)
     * @param {string} message Content to show on dialog
     */
    showSaveConfirmDialog(message) {
      this.confirmDialog.content = message;
      this.confirmDialog.isShowed = true;
      this.focusOnButton();
    },
    /**
     * Handle when click save on confirm dialog
     *
     * Author: nlnhat (06/07/2023)
     */
    async onClickSaveConfirm() {
      this.confirmDialog.isShowed = false;
      await this.onClickSave();
    },
    /**
     * Handle click close this form
     *
     * Author: nlnhat (26/06/2023)
     */
    onClickCloseForm() {
      const employeeBefore = JSON.stringify(this.theEmployee);
      const employeeAfter = JSON.stringify(this.reformatEmployee);

      if (employeeBefore != employeeAfter)
        this.showSaveConfirmDialog(this.$resources["vn"].saveChangeConfirm);
      else this.hideForm();
    },
    /**
     * Handle shortcut keys
     *
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event
     */
    handleShortKey(event) {
      const code = this.$enums.keyCode;

      // Handle shortcut keys on confirm dialog
      if (this.confirmDialog.isShowed) {
        this.handleShortKeyConfirmDialog(event);
      }
      // Handle shortcut keys on notice dialog
      else if (this.noticeDialog.isShowed) {
        this.handleShortKeyNoticeDialog(event);
      }

      // Ctrl + S || Ctrl + F8: Cất
      else if (
        ((event.ctrlKey && event.keyCode == code.s) ||
          (event.ctrlKey && event.keyCode == code.f8)) &&
        !event.shiftKey
      ) {
        event.preventDefault();
        this.onClickSave();
      }
      // Ctrl + Shift + N: Cất và thêm
      else if (event.ctrlKey && event.shiftKey && event.keyCode == code.s) {
        event.preventDefault();
        this.onClickSaveAdd();
      }
      // Esc: Đóng form
      else if (event.keyCode == code.esc) {
        event.preventDefault();
        this.onClickCloseForm();
      }
    },
    /**
     * Handle shortcut keys on confirm dialog
     *
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event on confirm dialog
     */
    handleShortKeyConfirmDialog(event) {
      const code = this.$enums.keyCode;
      switch (event.keyCode) {
        // Enter: Cất
        case code.enter:
          event.stopPropagation();
          event.preventDefault();
          this.onClickSaveConfirm();
          break;
        // N: Không
        case code.n:
          event.stopPropagation();
          event.preventDefault();
          this.hideForm();
          break;
        // Esc: Đóng
        case code.esc:
          event.stopPropagation();
          event.preventDefault();
          this.closeConfirmDialog();
          break;
      }
    },
    /**
     * Handle shortcut keys on notice dialog
     *
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event on notice dialog
     */
    handleShortKeyNoticeDialog(event) {
      const code = this.$enums.keyCode;
      // Enter || Esc: Đã hiểu
      if (event.keyCode == code.enter || event.keyCode == code.enter) {
        event.stopPropagation();
        event.preventDefault();
        this.closeNoticeDialog();
      }
    },
    /**
     * Handle shortcut keys on last button
     *
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event on last button
     */
    handleShortKeyLastButton(event) {
      const code = this.$enums.keyCode;
      // Tab: Focus on first input
      if (event.keyCode == code.tab) {
        event.stopPropagation();
        event.preventDefault();
        this.focusFirstInput();
      }
    },
    /**
     * Focus on first input
     *
     * Author: nlnhat (25/07/2023)
     */
    focusFirstInput() {
      this.$refs.EmployeeCode.focus();
    },
    /**
     * Focus on error item
     *
     * Author: nlnhat (04/08/2023)
     * @param {*} ref Ref name of error item
     */
    focusErrorItem(ref) {
      if (this.$refs[ref]) {
        this.$refs[ref].focus();
      }
    },
    /**
     * Set message for error item
     *
     * Author: nlnhat (04/08/2023)
     * @param {*} ref Ref name of error item
     * @param {*} errorMessage Error message
     */
    setErrorMessage(ref, errorMessage) {
      if (this.$refs[ref]) {
        this.$refs[ref].errorMessage = errorMessage;
      }
    },
    /**
     * Validate methods
     */
    validateEmployeeCode,
    validateFullName,
    validateIdentityNumber,
    validateDateOverToday,
    validatePhoneNumber,
    validateDepartment,
    validatePosition,
    validateEmail,
    validateBankAccount,
    /**
     * Format methods
     */
    formatPhoneNumber,
    formatCode,
    formatDate,
    formatStringBySpace,
    reformatDate,
    sortArrayByName,
  },
};
</script>
