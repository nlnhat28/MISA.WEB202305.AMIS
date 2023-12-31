import { createApp } from "vue";
import App from "./App.vue";

import emitter from "tiny-emitter/instance";
import router from "@/router/index.js";

import resources from "@/constants/resources.js";
import enums from "@/constants/enums.js";
import common from "@/js/common/common.js";

import MISAActionDropdown from "@/components/base/action-dropdown/MISAActionDropdown.vue";
import MISAActionMany from "@/components/base/action-many/MISAActionMany.vue";
import MISAButton from "@/components/base/button/MISAButton.vue";
import MISACheckbox from "@/components/base/checkbox/MISACheckbox.vue";
import MISACheckboxAll from "@/components/base/checkbox/MISACheckboxAll.vue";
import MISACheckboxItem from "@/components/base/checkbox/MISACheckboxItem.vue";
import MISACombobox from "@/components/base/combobox/MISACombobox.vue";
import MISADatePicker from "@/components/base/date-picker/MISADatePicker.vue";
import MISADialog from "@/components/base/dialog/MISADialog.vue";
import MISAFilter from "@/components/base/filter/MISAFilter.vue";
import MISAFilterResult from "@/components/base/filter/MISAFilterResult.vue";
import MISAFormBody from "@/components/base/form/MISAFormBody.vue";
import MISAFormColumn from "@/components/base/form/MISAFormColumn.vue";
import MISAFormGroup from "@/components/base/form/MISAFormGroup.vue";
import MISAFormItem from "@/components/base/form/MISAFormItem.vue";
import MISAFormRow from "@/components/base/form/MISAFormRow.vue";
import MISAIcon from "@/components/base/icon/MISAIcon.vue";
import MISAIconContainer from "@/components/base/icon/MISAIconContainer.vue";
import MISAIconX from "@/components/base/icon/MISAIconX.vue";
import MISALoading from "@/components/base/mask/MISALoading.vue";
import MISAMenuItem from "@/components/base/menu/MISAMenuItem.vue";
import MISANoContent from "@/components/base/mask/MISANoContent.vue";
import MISAPage from "@/components/base/page/MISAPage.vue";
import MISAPagination from "@/components/base/pagination/MISAPagination.vue";
import MISAPageNumber from "@/components/base/pagination/MISAPageNumber.vue";
import MISAPopup from "@/components/base/popup/MISAPopup.vue";
import MISARadioItem from "@/components/base/radio/MISARadioItem.vue";
import MISARadioGroup from "@/components/base/radio/MISARadioGroup.vue";
import MISASelectBar from "@/components/base/select-bar/MISASelectBar.vue";
import MISASearchBox from "@/components/base/search/MISASearchBox.vue";
import MISASearchResult from "@/components/base/search/MISASearchResult.vue";
import MISASpinner from "@/components/base/spinner/MISASpinner.vue";
import MISATable from "@/components/base/table/MISATable.vue";
import MISATd from "@/components/base/table/MISATd.vue";
import MISATh from "@/components/base/table/MISATh.vue";
import MISATr from "@/components/base/table/MISATr.vue";
import MISATextfield from "@/components/base/textfield/MISATextfield.vue";
import MISAToastMessage from "@/components/base/toast-message/MISAToastMessage.vue";
import MISATooltip from "@/components/base/tooltip/MISATooltip.vue";

import clickOutside from "@/directives/click-outside";
import tooltip from "@/directives/tooltip";

const app = createApp(App);

app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$enums = enums;
app.config.globalProperties.$resources = resources;
app.config.globalProperties.$common = common;

app.component("m-action-dropdown", MISAActionDropdown);
app.component("m-action-many", MISAActionMany);
app.component("m-button", MISAButton);
app.component("m-checkbox", MISACheckbox);
app.component("m-checkbox-all", MISACheckboxAll);
app.component("m-checkbox-item", MISACheckboxItem);
app.component("m-combobox", MISACombobox);
app.component("m-date-picker", MISADatePicker);
app.component("m-dialog", MISADialog);
app.component("m-filter", MISAFilter);
app.component("m-filter-result", MISAFilterResult);
app.component("m-form-body", MISAFormBody);
app.component("m-form-column", MISAFormColumn);
app.component("m-form-group", MISAFormGroup);
app.component("m-form-item", MISAFormItem);
app.component("m-form-row", MISAFormRow);
app.component("m-icon", MISAIcon);
app.component("m-icon-x", MISAIconX);
app.component("m-icon-container", MISAIconContainer);
app.component("m-loading", MISALoading);
app.component("m-menu-item", MISAMenuItem);
app.component("m-no-content", MISANoContent);
app.component("m-page", MISAPage);
app.component("m-pagination", MISAPagination);
app.component("m-page-number", MISAPageNumber);
app.component("m-popup", MISAPopup);
app.component("m-radio-item", MISARadioItem);
app.component("m-radio-group", MISARadioGroup);
app.component("m-select-bar", MISASelectBar);
app.component("m-search-box", MISASearchBox);
app.component("m-search-result", MISASearchResult);
app.component("m-spinner", MISASpinner);
app.component("m-table", MISATable);
app.component("m-td", MISATd);
app.component("m-th", MISATh);
app.component("m-tr", MISATr);
app.component("m-textfield", MISATextfield);
app.component("m-toast-message", MISAToastMessage);
app.component("m-tooltip", MISATooltip);

app.directive("click-outside", clickOutside);
app.directive("tooltip", tooltip);

app.use(router);

app.mount("#app");

export default app;
