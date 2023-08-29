<template>
  <m-page
    :title="this.$resources['vn'].employeeList"
    @keydown="handleShortKey"
    tabindex="0"
  >
    <template #pageAction>
      <!-- add new -->
      <m-button
        :type="this.$enums.buttonType.primary"
        :click="showEmptyEmployeeForm"
        :text="this.$resources['vn'].create"
        iconLeft="plus--light"
        tooltipContent="Insert"
      >
      </m-button>
    </template>
    <template #pageBody>
      <!-- table -->
      <m-table
        :totalRecord="totalRecord"
        :isLoading="isLoading"
      >
        <template #toolbarLeft>
          <!-- selection bar -->
          <m-select-bar
            :selectLength="this.employeesSelect.length"
            @emitUnselect="onClickUnselectAll(false)"
            @emitDelete="onClickDeleteEmployees()"
            :recordName="this.$resources['vn'].employee"
          >
          </m-select-bar>
          <!-- filter result -->
          <m-filter-result
            v-for="(filterItem, index) in filterItems"
            v-show="filterItem != null"
            :key="index"
            :filterItem="filterItem"
            @emitRemoveFilterItem="removeFilterItem"
            :dateFormat="this.dateFormat"
            :index="index"
          >
          </m-filter-result>
          <m-button
            :type="this.$enums.buttonType.linkDanger"
            :text="this.$resources['vn'].removeAllFilters"
            v-if="this.filterItemsClean.length > 1"
            :click="removeAllFilters"
          ></m-button>
          <!-- search result -->
          <m-search-result
            v-show="this.search.key?.length && !this.isLoading"
            v-model:keySearch="this.search.key"
          >
          </m-search-result>
        </template>
        <template #toolbarRight>
          <!-- search in list -->
          <m-search-box
            v-model="this.search.key"
            :placeholder="this.$resources['vn'].searchInList"
            :tooltipContent="this.$resources['vn'].searchEmployee"
            ref="searchBox"
            @emitFocusFirst="focusRow(0)"
          ></m-search-box>
          <!-- excel -->
          <m-button
            :type="this.$enums.buttonType.secondary"
            tooltipPos="top-center"
            :click="onClickExcel"
            :hasLoading="true"
            :tooltipContent="this.$resources['vn'].exportToExcel"
            iconCenter="excel"
          >
          </m-button>
          <!-- reload -->
          <m-button
            :type="this.$enums.buttonType.secondary"
            tooltipPos="top-left"
            :click="onReloadData"
            :hasLoading="false"
            :tooltipContent="this.$resources['vn'].reloadData"
            iconCenter="refresh"
          >
          </m-button>
        </template>
        <template #thead>
          <!-- table head -->
          <m-th
            textAlign="center"
            widthCell=47
          >
            <m-checkbox-all
              :checked="isCheckedAllComputed"
              @emitOnChange="onChangeCheckAll()"
              :disabled="!employees.length"
              :isCheckSome="isCheckSomeComputed"
            ></m-checkbox-all>
          </m-th>
          <m-th
            v-for="(head, index) in table.heads"
            :key="index"
            :textAlign="head.textAlign"
            :widthCell="head.widthCell"
            :name="head.name"
            :title="head.title"
            :fullTitle="head?.fullTitle"
            :sortType="head.sortType"
            v-model:sortItem="sortItems[index]"
            :filterConfig="head.filterConfig"
            v-model:filterItem="filterItems[index]"
          >
            {{ head.title }}
          </m-th>
        </template>
        <template #tbody>
          <!-- table body -->
          <m-tr
            v-for="(employee, index) in employees"
            :key="employee.EmployeeId"
            :index="index"
            :id="employee.EmployeeId"
            ref="tr"
            :isSelected="isSelected(employee.EmployeeId)"
            @emitUpdate="showEmployeeForm(employee)"
            @emitDelete="handleDeleteOnRow(employee.EmployeeId)"
            @emitDuplicate="duplicateEmployee(employee)"
            @emitFocusNext="focusNextRow"
            @emitFocusPrevious="focusPreviousRow"
            v-model:focusedId="focusedId"
          >
            <template #content>
              <!-- Checkbox -->
              <m-td
                textAlign="center"
                @dblclick="preventDoubleClick"
              >
                <m-checkbox
                  v-model:model="employeesSelect"
                  :id="employee.EmployeeId"
                  :checked="isSelected(employee.EmployeeId)"
                ></m-checkbox>
              </m-td>
              <!-- Mã nhân viên -->
              <m-td
                textAlign="left"
                :content="employee.EmployeeCode"
              >
              </m-td>
              <!-- Tên nhân viên -->
              <m-td
                textAlign="left"
                :content="employee.FullName"
              ></m-td>
              <!-- Giới tính -->
              <m-td
                textAlign="left"
                :content="employee.GenderName"
              ></m-td>
              <!-- Ngày sinh -->
              <m-td
                textAlign="center"
                :content="formatDate(employee.DateOfBirth, this.dateFormat)"
              ></m-td>
              <!-- Số CCCD -->
              <m-td
                textAlign="left"
                :content="formatStringBySpace(employee.IdentityNumber)"
              ></m-td>
              <!-- Tên đơn vị -->
              <m-td
                textAlign="left"
                :content="employee.DepartmentName"
              ></m-td>
              <!-- Chức danh -->
              <m-td
                textAlign="left"
                :content="employee.Position"
              ></m-td>
              <!-- Điện thoại -->
              <m-td
                textAlign="left"
                :content="formatPhoneNumber(employee.PhoneNumber)"
              ></m-td>
              <!-- Email -->
              <m-td
                textAlign="left"
                :content="employee.Email"
              ></m-td>
              <!-- Địa chỉ -->
              <m-td
                textAlign="left"
                :content="employee.Address"
              ></m-td>
              <!-- Tài khoản ngân hàng -->
              <m-td
                textAlign="left"
                :content="formatStringBySpace(employee.BankAccount)"
              ></m-td>
              <!-- Mã số thuế -->
              <m-td
                textAlign="left"
                :content="employee.BankName"
              ></m-td>
              <!-- Chức năng -->
              <m-td
                textAlign="center"
                @dblclick="preventDoubleClick"
                tabindex="0"
              >
                <m-action-dropdown
                  @emitUpdate="showEmployeeForm(employee)"
                  @emitDuplicate="duplicateEmployee(employee)"
                  @emitDelete="onClickDeleteEmployee(employee.EmployeeId)"
                >
                </m-action-dropdown>
              </m-td>
            </template>
          </m-tr>
        </template>
        <template #tfooter>
          <!-- pagination -->
          <m-pagination
            v-model:totalRecord="totalRecord"
            v-model:allRecord="allRecord"
            v-model:pageModel="page"
            @emitUpdatePage="updatePage"
            :canAccessRandom="true"
            :isShowPageNumbers="true"
          >
          </m-pagination>
        </template>
      </m-table>
    </template>
  </m-page>
  <!-- Form employee -->
  <EmployeeForm
    v-if="isShowedEmployeeForm"
    :hideForm="hideEmployeeForm"
    :theEmployee="employeeUpdate"
    :formMode="formMode"
    @emitReloadData="filterEmployees"
    :key="formKey"
    @emitReRenderForm="reRenderForm()"
    @emitUpdateFocusedId="updateFocusedId"
  >
  </EmployeeForm>
  <!-- Dialog delete confiem -->
  <m-dialog
    :type="this.deleteConfirmDialog.type"
    :title="this.deleteConfirmDialog.title"
    :content="this.deleteConfirmDialog.content"
    @emitCloseDialog="this.deleteConfirmDialog.onClickCancel()"
    v-if="this.deleteConfirmDialog.isShowed"
  >
    <template #footer>
      <div class="button-container">
        <m-button
          :type="this.$enums.buttonType.danger"
          :text="this.$resources['vn'].delete"
          ref="buttonFocus"
          :click="this.deleteConfirmDialog.onClickDelete"
        >
        </m-button>
        <m-button
          :type="this.$enums.buttonType.secondary"
          :text="this.$resources['vn'].cancel"
          :click="this.deleteConfirmDialog.onClickCancel"
        >
        </m-button>
      </div>
    </template>
  </m-dialog>
</template>
<script>
import EmployeeForm from './EmployeeForm.vue';
import {
  formatDate,
  formatPhoneNumber,
  formatMoney,
  formatStringByDot,
  formatStringBySpace,
} from "@/js/utils/format.js";
import { sortArrayByName } from '@/js/utils/sort.js'
import { download } from '@/js/utils/file.js'
import employeeService from '@/services/employee.js';
import departmentService from '@/services/department.js';

export default {
  name: "EmployeesList",
  components: {
    EmployeeForm,
  },
  data() {
    return {
      /**
       * Show form or not 
       */
      isShowedEmployeeForm: false,
      /**
       * Mode of form create or update
       */
      formMode: null,
      /**
       * List of employees to show on table
       */
      employees: [],
      /**
       * List of selected employees
       */
      employeesSelect: [],
      /**
       * Employee showed on form to update
       */
      employeeUpdate: {},
      /**
       * Id of employee to focus
       */
      focusedId: null,
      /**
       * Delete confirm dialog object
       */
      deleteConfirmDialog: {
        type: this.$enums.dialogType.warning,
        title: `${this.$resources['vn'].delete} ${this.$resources['vn'].employee}`,
        content: '',
        isShowed: false,
        onClickCancel: () => {
          this.deleteConfirmDialog.isShowed = false;
        }
      },
      /**
       * 
       */
      page: {
        pageNumber: 1,
        pageSize: 20,
      },
      search: {
        key: '',
      },
      /**
       * Filter object
       */
      filter: {},
      /**
       * Total record on table
       */
      totalRecord: 0,
      /**
       * All record in database
       */
      allRecord: 0,
      /**
       * Loading flag
       */
      isLoading: true,
      /**
       * Key of employee form
       */
      formKey: 0,
      /**
       * Câu truy vấn sort
       */
      sortQuery: null,
      /**
       * Các chuỗi sort query mới từ các cột
       */
      sortItems: [],
      /**
       * Các chuỗi sort query cũ từ các cột 
       */
      oldSortItems: [],
      /**
       * Hàng đợi sort
       */
      queueSortItems: [],
      /**
       * Departments list from API
       */
      departments: [],
      /**
       * Departments list show on combobox
       */
      departmentsSelects: [],
      /**
       * Table data
       */
      table: {
        heads: [
          {
            title: 'MÃ NV',
            fullTitle: 'Mã nhân viên',
            textAlign: 'left',
            widthCell: 140,
            name: "EmployeeCode",
            sortType: this.$enums.sortType.blur,
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'TÊN NHÂN VIÊN',
            textAlign: 'left',
            widthCell: 240,
            name: "FullName",
            sortType: this.$enums.sortType.blur,
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'GIỚI TÍNH',
            textAlign: 'left',
            widthCell: 140,
            name: "Gender",
            filterConfig: {
              filterType: this.$enums.filterType.selectId,
              selects: this.$common.selects.genders,
            }
          },
          {
            title: 'NGÀY SINH',
            textAlign: 'center',
            widthCell: 160,
            name: "DateOfBirth",
            sortType: this.$enums.sortType.blur,
            filterConfig: {
              filterType: this.$enums.filterType.date,
            }
          },
          {
            title: 'SỐ CMND',
            fullTitle: "Số chứng minh nhân dân",
            textAlign: 'left',
            widthCell: 240,
            name: "IdentityNumber",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'PHÒNG BAN',
            textAlign: 'left',
            widthCell: 240,
            name: "DepartmentName",
            sortType: this.$enums.sortType.blur,
            filterConfig: {
              filterType: this.$enums.filterType.selectName,
              selects: this.departmentsSelects,
            }
          },
          {
            title: 'CHỨC DANH',
            textAlign: 'left',
            widthCell: 200,
            name: "Position",
            sortType: this.$enums.sortType.blur,
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'SỐ DI ĐỘNG',
            textAlign: 'left',
            widthCell: 160,
            name: "PhoneNumber",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'EMAIL',
            textAlign: 'left',
            widthCell: 240,
            name: "Email",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'ĐỊA CHỈ',
            textAlign: 'left',
            widthCell: 240,
            name: "Address",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'SỐ TÀI KHOẢN',
            textAlign: 'left',
            widthCell: 160,
            name: "BankAccount",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'TÊN NGÂN HÀNG',
            textAlign: 'left',
            widthCell: 200,
            name: "BankName",
            filterConfig: {
              filterType: this.$enums.filterType.text,
            }
          },
          {
            title: 'CHỨC NĂNG',
            textAlign: 'center',
            widthCell: 124,
          }
        ]
      },
      /**
       * Câu truy vấn filter
       */
      filterQuery: null,
      /**
       * Các filter item từ các cột
       */
      filterItems: [],
      /**
       * Các filter item không rỗng từ các cột
       */
      filterItemsClean: [],
      /**
       * Các filter queries
       */
      filterQueries: [],
      /**
       * Flag
       */
      isFilterSuccess: true,
      /**
       * Date format
       */
      dateFormat: 'dd/MM/yyyy'
    };
  },
  async created() {
    document.title = `${this.$resources['vn'].titleEmployees} - ${this.$resources['vn'].accountingAmis}`;
    await Promise.all([
      this.filterEmployees(),
      this.getDepartments()])
    this.isLoading = false
  },
  mounted() {
    this.focusOnSearchBox();
  },
  watch: {
    /**
     * Reload data if search changes
     * 
     * Author: nlnhat (05/07/2023)
     */
    search: {
      handler() {
        this.reloadEmployees();
      },
      deep: true
    },
    /**
     * Generate sort query when sort items change
     * 
     * Author: nlnhat (05/07/2023)
     */
    sortItems: {
      handler() {
        const newItem = this.sortItems.find(item => item != null && !this.oldSortItems.includes(item)) ?? null;
        const oldItem = this.oldSortItems.find(item => item != null && !this.sortItems.includes(item)) ?? null;

        if (oldItem) {
          const oldIndex = this.queueSortItems.indexOf(oldItem);
          if (oldIndex != -1) this.queueSortItems[oldIndex] = newItem
          else this.queueSortItems.push(newItem);
        }
        else this.queueSortItems.push(newItem);

        this.queueSortItems = this.queueSortItems.filter(item => item != oldItem);

        this.oldSortItems = [...this.sortItems];
        this.sortQuery = this.sortQueryComputed;
      },
      deep: true
    },
    /**
     * Reload when sort query changes
     * 
     * Author: nlnhat (05/07/2023)
     */
    async sortQuery() {
      await this.reloadEmployees()
    },
    /**
     * What filter items change
     * 
     * Author: nlnhat (26/07/2023)
     */
    filterItems: {
      handler() {
        this.filterItemsClean = this.filterItems.filter(item => item && item != null);
      },
      deep: true
    },
    /**
     * Generate filter query when filter items change
     * 
     * Author: nlnhat (26/07/2023)
     */
    filterItemsClean: {
      handler() {
        this.filterQueries = [];
        this.filterItemsClean.forEach(item => {
          let query = ''
          switch (item.compareType) {
            case this.$enums.compareType.contain: {
              query = `${item.column}=%${item.values[0].key}%`;
              break;
            };
            case this.$enums.compareType.equal: {
              query = `${item.column}=${item.values[0].key}`;
              break;
            };
            case this.$enums.compareType.startWith: {
              query = `${item.column}=${item.values[0].key}%`;
              break;
            };
            case this.$enums.compareType.endWith: {
              query = `${item.column}=%${item.values[0].key}`;
              break;
            };
            case this.$enums.compareType.empty: {
              query = `${item.column}=`;
              break;
            };
            case this.$enums.compareType.less: {
              query = `${item.column}<${item.values[0].key}`;
              break;
            };
            case this.$enums.compareType.more: {
              query = `${item.column}>${item.values[0].key}`;
              break;
            };
            case this.$enums.compareType.month: {
              query = `Month(${item.column})=${item.values[0].key}`;
              break;
            };
            case this.$enums.compareType.year: {
              query = `Year(${item.column})=${item.values[0].key}`;
              break;
            };
            default:
              break;
          }
          this.filterQueries.push(query);
        })
        this.filterQuery = this.filterQueryComputed;
      },
      deep: true
    },
    /**
     * Reload when sort query changes
     * 
     * Author: nlnhat (05/07/2023)
     */
    async filterQuery() {
      await this.reloadEmployees()
    },
  },
  computed: {
    /**
     * Check all records in page are selected or not
     * 
     * Author: nlnhat (05/07/2023)
     * @return True if are selected, otherwise false
     */
    isCheckedAllComputed() {
      if (!this.employees || this.employees.length === 0) {
        return false;
      }
      return this.employees.every(employee => this.employeesSelect.includes(employee.EmployeeId));
    },
    /**
     * Check some records (has check but not all) or not
     * 
     * Author: nlnhat (05/07/2023)
     * @return True if are selected, otherwise false
     */
    isCheckSomeComputed() {
      const isIncludes = this.employeesSelect.every(id =>
        this.employees.some(employee => employee.EmployeeId == id));

      return (this.employees
        && this.employeesSelect.length > 0
        && this.employeesSelect.length < this.employees.length
        && isIncludes)
    },
    /**
     * Tạo câu truy vấn từ các sort item
     * 
     * Author: nlnhat (5/07/2023)
     * @return Câu truy vấn sort
     */
    sortQueryComputed() {
      this.queueSortItems = this.queueSortItems.filter(item => item != null && item != '');
      const sortQuery = this.queueSortItems.join(',');
      return sortQuery
    },
    /**
     * Tạo câu truy vấn từ các filter item
     * 
     * Author: nlnhat (25/07/2023)
     * @return Câu truy vấn sort
     */
    filterQueryComputed() {
      this.filterQueries = this.filterQueries.filter(item => item != null);
      const filterQuery = this.filterQueries.join(',');
      return filterQuery
    }
  },
  methods: {
    // }, 500),
    /**
     * Handle checkbox check all records on table in a page
     * 
     * Author: nlnhat (05/07/2023)
     */
    onChangeCheckAll() {
      // Uncheck all records in a page
      if (this.isCheckedAllComputed) {
        this.employeesSelect = this.employeesSelect.filter(id => {
          return !this.employees.some(employee => employee.EmployeeId === id);
        });
      }
      // Check all records in a page
      else {
        this.employees.forEach(employee => {
          if (!this.employeesSelect.includes(employee.EmployeeId)) {
            this.employeesSelect.push(employee.EmployeeId)
          }
        })
      }
    },
    /**
     * Handle on click un select allk
     * 
     * Author: nlnhat (05/07/2023)
     */
    onClickUnselectAll() {
      this.employeesSelect = []
    },
    /**
     * Check id in selected list or not
     * 
     * Author: nlnhat (05/07/2023)
     * @return True if selected, otherwise false
     */
    isSelected(id) {
      return this.employeesSelect.includes(id)
    },
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
        this.departments.forEach(department => {
          selects.push({
            id: department.DepartmentId,
            name: department.DepartmentName
          });
        });
        this.departmentsSelects = this.sortArrayByName(selects);
        this.table.heads[5].filterConfig.selects = this.departmentsSelects;
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Get all employees
     * 
     * Author: nlnhat (26/06/2023)
     */
    async getEmployees() {
      await this.makeLoadingEffect(async () => {
        const response = await employeeService.get();
        if (response?.status == this.$enums.status.ok)
          this.employees = response.data;
      }
      )
    },
    /**
     * Get filtered employees
     * 
     * Author: nlnhat (26/06/2023)
     */
    async filterEmployees() {
      try {
        this.filter = {
          ...this.page,
          ...this.search,
          sort: this.sortQuery,
          filter: this.filterQuery
        };
        const response = await employeeService.filter(this.filter);
        if (response?.status == this.$enums.status.ok) {
          this.employees = response.data.Data;
          this.totalRecord = response.data.TotalRecord;
          this.allRecord = response.data.AllRecord;
          this.page.pageNumber = response.data.PageNumber;
          this.isFilterSuccess = true;
        } else if (response?.status == this.$enums.status.noContent) {
          this.employees = []
          this.totalRecord = 0
          this.allRecord = response.data.AllRecord;
          this.isFilterSuccess = true;
        } else this.isFilterSuccess = false;
      } catch (error) {
        console.error(error);
        this.isFilterSuccess = false;
      }
    },
    /**
     * Reload data 
     * 
     * Author: nlnhat (26/06/2023)
     */
    async reloadEmployees() {
      await this.makeLoadingEffect(this.filterEmployees)
    },
    /**
     * On click reload data button
     * 
     * Author: nlnhat (29/06/2023)
     */
    async onReloadData() {
      await this.reloadEmployees();
      if (this.isFilterSuccess) this.showToastReloadSuccess();
    },
    /**
     * Delete one employee
     * 
     * Author: nlnhat (26/06/2023)
     * @param {string} employeeId Id of employee
     */
    async deleteEmployee(employeeId, employeeCode) {
      try {
        const response = await employeeService.delete(employeeId);
        const refFocus = this.$refs.tr.find(tr => tr.id == employeeId);

        if (response?.status == this.$enums.status.ok) {
          if (refFocus) {
            refFocus.vanish();
          }
          setTimeout(async () => {
            this.employeesSelect = this.employeesSelect.filter(id => id != employeeId);
            this.employees = this.employees.filter(id => id != employeeId);
            await this.filterEmployees();
          }, 300);
          this.showToastDeleteSuccess(`${this.$resources['vn'].deleted} ${this.$resources['vn'].employee} <${employeeCode}>`);
        }
        else {
          if (refFocus) {
            refFocus.clearFocus();
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Delete selected employees
     * 
     * Author: nlnhat (26/06/2023)
     */
    async deleteEmployees() {
      await this.makeLoadingEffect(async () => {
        try {
          const response = await employeeService.deleteMany(this.employeesSelect);

          if (response?.status == this.$enums.status.ok) {
            const deletedCount = response.data;

            // Nếu xoá được > 0 thì thông báo thành công
            if (deletedCount > 0) {
              this.showToastDeleteSuccess(`${this.$resources['vn'].deleted} ${deletedCount} ${this.$resources['vn'].employee}`);

              // Nếu không xoá được hết thì báo lỗi
              if (deletedCount < this.employeesSelect.length) {
                const errorDeleteCount = this.employeesSelect.length - deletedCount;
                this.showToastDeleteError(`${this.$resources['vn'].cannotDelete} ${errorDeleteCount} ${this.$resources['vn'].employee}`);
              }

              this.employeesSelect = [];
              await this.filterEmployees();
            }
          }
        } catch (error) {
          console.error(error)
        }
      })
    },
    /**
     * On click delete one employee
     * 
     * Author: nlnhat (29/06/2023)
     * @param {*} employeeId Id of selected employee
     */
    onClickDeleteEmployee(employeeId) {
      try {
        const employee = this.employees.find(employee => employee.EmployeeId === employeeId);
        const employeeCode = employee.EmployeeCode;
        const refFocus = this.$refs.tr.find(tr => tr.id == employeeId);
        if (refFocus) {
          refFocus.setFocus();
        }

        this.deleteConfirmDialog.content = `${this.$resources['vn'].deleteConfirm} ${this.$resources['vn'].employee} <${employeeCode}>`;
        this.deleteConfirmDialog.onClickDelete = () => {
          this.deleteConfirmDialog.isShowed = false;
          this.deleteEmployee(employeeId, employeeCode);
        }
        this.deleteConfirmDialog.onClickCancel = () => {
          this.deleteConfirmDialog.isShowed = false;
          if (refFocus) {
            refFocus.clearFocus();
          }
        }
        this.deleteConfirmDialog.isShowed = true;
        this.focusOnButton();
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * On click delete all employees
     * 
     * Author: nlnhat (29/06/2023)
     */
    onClickDeleteEmployees() {
      try {
        const oldLength = this.employeesSelect.length;

        this.deleteConfirmDialog.content = `${this.$resources['vn'].deleteConfirm} ${oldLength} ${this.$resources['vn'].selectedEmployees}`;
        this.deleteConfirmDialog.onClickDelete = () => {
          this.deleteConfirmDialog.isShowed = false;
          this.deleteEmployees();
        }
        this.deleteConfirmDialog.onClickCancel = () => {
          this.deleteConfirmDialog.isShowed = false;
          if (refFocus) {
            refFocus.clearFocus();
          }
        }
        this.deleteConfirmDialog.isShowed = true;
        this.focusOnButton();
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Show empty employee form to add new 
     * 
     * Author: nlnhat (26/06/2023)
     */
    showEmptyEmployeeForm() {
      this.employeeUpdate = {};
      this.formMode = this.$enums.formMode.create;
      this.isShowedEmployeeForm = true;
    },
    /**
     * Show employee form
     * 
     * Author: nlnhat (26/06/2023)
     * 
     * @param {*} employee Selected employee
     */
    showEmployeeForm(employee) {
      this.employeeUpdate = employee;
      this.formMode = this.$enums.formMode.update;
      this.isShowedEmployeeForm = true;
    },
    /**
     * Hide employee form
     * 
     * Author: nlnhat (26/06/2023)
     */
    hideEmployeeForm() {
      this.isShowedEmployeeForm = false;
    },
    /**
     * Duplicate employee
     * 
     * Author: nlnhat (29/07/2023)
     * 
     * @param {*} employee Selected employee
     */
    duplicateEmployee(employee) {
      this.employeeUpdate = employee;
      this.formMode = this.$enums.formMode.duplicate;
      this.isShowedEmployeeForm = true;
    },
    /**
     * Show toast message after reload
     * 
     * Author: nlnhat (29/06/2023)
     */
    showToastReloadSuccess() {
      const content = this.$resources['vn'].reloadedData;
      this.$emitter.emit("showToastSuccess", content);
    },
    /**
     * Show toast message after delete success
     * 
     * Author: nlnhat (29/06/2023)
     * @param {string} content Message added
     */
    showToastDeleteSuccess(content) {
      this.$emitter.emit("showToastSuccess", content);
    },
    /**
     * Show toast message after delete error
     * 
     * Author: nlnhat (29/06/2023)
     * @param {string} content Message added
     */
    showToastDeleteError(content) {
      this.$emitter.emit("showToastError", content);
    },
    /**
     * Re render form
     *
     * Author: nlnhat (29/06/2023)
     */
    reRenderForm() {
      this.formMode = this.$enums.formMode.create;
      this.employeeUpdate = {}
      this.formKey += 1;
    },
    /**
     * Handle event click on button Excel
     * 
     * Author: nlnhat (22/07/2023)
     */
    async onClickExcel() {
      await this.exportToExcel();
    },
    /**
     * Handle export to excel
     * 
     * Author: nlnhat (22/07/2023)
     */
    async exportToExcel() {
      try {
        const response = await employeeService.export();
        if (response?.status == this.$enums.status.ok) {
          const blob = new Blob(
            [response.data],
            { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }
          );
          const fileName = `${this.$resources['vn'].employeeListFileName}_${formatDate(new Date(), 'ddMMyyyy')}.xlsx`;
          this.download(blob, fileName)
        }
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Handle shortcut keys
     * 
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event
     */
    handleShortKey(event) {
      try {
        const code = this.$enums.keyCode;

        // Handle shortcut keys on delete confirm dialog
        if (this.deleteConfirmDialog.isShowed == true) {
          this.handleShortKeyDeleteDialog(event);
        }
        // Insert: Thêm mới
        else if (event.keyCode == code.insert) {
          event.preventDefault();
          this.showEmptyEmployeeForm();
        }
        // Ctrl + F || Ctrl + F3: Focus ô tìm kiếm
        else if ((event.ctrlKey && event.keyCode == code.f) || event.keyCode == code.f3) {
          event.preventDefault();
          this.focusOnSearchBox();
        }
        // Ctrl + R: Reload
        else if (event.ctrlKey && event.keyCode == code.r) {
          event.preventDefault();
          this.onReloadData();
        }
        // Ctrl + E: Export to excel
        else if (event.ctrlKey && event.keyCode == code.e) {
          event.preventDefault();
          this.exportToExcel();
        }
        // Ctrl + D || Delete: Delete
        else if (((event.ctrlKey && event.keyCode == code.d) || event.keyCode == code.delete)
          && this.employeesSelect.length > 0) {
          event.preventDefault();
          this.onClickDeleteEmployees();
        }
        // Ctrl + A: Chọn tất
        else if (event.ctrlKey && event.keyCode == code.a) {
          event.preventDefault();
          this.onChangeCheckAll();
        }
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Handle shortcut keys on delete confirm dialog
     * 
     * Author: nlnhat (25/07/2023)
     * @param {*} event Keydown event
     */
    handleShortKeyDeleteDialog(event) {
      const code = this.$enums.keyCode;
      if (event.keyCode == code.enter) {
        event.stopPropagation();
        event.preventDefault();
        this.deleteConfirmDialog.onClickDelete();
      }
      else if (event.keyCode == code.n || event.keyCode == code.esc) {
        event.stopPropagation();
        event.preventDefault();
        this.deleteConfirmDialog.onClickCancel();
      }
    },
    /**
     * Prevent double click
     */
    preventDoubleClick(event) {
      event.stopPropagation();
    },
    /**
     * Focus on search box
     * 
     * Author: nlnhat (25/07/2023)
     */
    focusOnSearchBox() {
      this.$nextTick(() => {
        this.$refs.searchBox.focus();
      })
    },
    /**
     * Remove filter in a column
     * 
     * Author: nlnhat (25/07/2023)
     * @param {number} index Index of column 
     */
    removeFilterItem(index) {
      this.filterItems[index] = null;
    },
    /**
     * Remove all filters in columns
     * 
     * Author: nlnhat (25/07/2023)
     */
    removeAllFilters() {
      this.filterItems = this.filterItems.fill(null);
    },
    /**
     * Focus on button
     * 
     * Author: nlnhat (25/07/2023)
     */
    focusOnButton() {
      this.$nextTick(() => {
        this.$refs.buttonFocus.focus();
      })
    },
    /**
     * Focus on next row in table
     * 
     * Author: nlnhat (25/07/2023)
     * @param {number} index Index of row 
     */
    focusNextRow(index) {
      let newIndex = index + 1;
      if (newIndex >= this.employees.length)
        newIndex = 0;
      this.focusRow(newIndex);
    },
    /**
     * Focus on previous row in table
     * 
     * Author: nlnhat (25/07/2023)
     * @param {number} index Index of row 
     */
    focusPreviousRow(index) {
      let newIndex = index - 1;
      if (newIndex < 0)
        newIndex = this.employees.length - 1;
      this.focusRow(newIndex);
    },
    /**
     * Focus on a row by id
     * 
     * Author: nlnhat (08/08/2023)
     * @param {number} index Index of row 
     */
    focusRow(index) {
      const refFocus = this.$refs.tr.find(tr => tr.index == index);
      if (refFocus)
        refFocus.focus();
    },
    /**
     * Update focused id
     * 
     * Author: nlnhat (08/08/2023) 
     * @param {*} id Focused id
     */
    updateFocusedId(id) {
      this.focusedId = id;
    },
    /**
     * Handle delete on a row
     * 
     * Author: nlnhat (04/08/2023)
     * @param {*} employeeId Id of employee on this row
     */
    handleDeleteOnRow(employeeId) {
      this.onClickDeleteEmployee(employeeId);
    },
    /**
     * Update page property from pagination component
     * 
     * Author: nlnhat (25/07/2023)
     * @param {*} page Page object from pagination component
     */
    async updatePage(page) {
      this.page = page;
      await this.reloadEmployees();
    },
    /**
     * Imported methods
     */
    formatDate,
    formatMoney,
    formatPhoneNumber,
    formatStringByDot,
    formatStringBySpace,
    sortArrayByName,
    download,
  }
}
</script>
<style>
@import "@/css/views/employees/index.css";
</style>