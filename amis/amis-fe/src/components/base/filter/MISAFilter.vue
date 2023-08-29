<template>
    <div class="p-relative">
        <div
            class="filter"
            @keydown="onKeyDown"
        >
            <div class="filter__header">
                {{ this.$resources['vn'].filter }} {{ title.toLowerCase() }}
            </div>
            <div class="filter__body">
                <m-combobox
                    v-if="isShowCompareSelects"
                    :theSelects="compareTypeSelects"
                    v-model:id="compareType.id"
                    v-model:name="compareType.name"
                    :isReadOnly="true"
                    :canSearch="false"
                    :canUnselect="false"
                >
                </m-combobox>
                <m-combobox
                    v-if="isShowSelectsId"
                    :theSelects="selects"
                    v-model:id="filterValueKey"
                    v-model:name="filterValueName"
                    :isReadOnly="true"
                    :canSearch="false"
                    :canUnselect="false"
                    @emitSelected="focusOnApply()"
                    ref="combobox"
                >
                </m-combobox>
                <m-combobox
                    v-if="isShowSelectsName"
                    :theSelects="selects"
                    v-model:id="filterValueKey"
                    v-model:name="filterValueName"
                    :isReadOnly="true"
                    :canSearch="false"
                    :canUnselect="false"
                    @emitSelected="focusOnApply()"
                    ref="combobox"
                >
                </m-combobox>
                <m-textfield
                    v-if="isShowTextField"
                    ref="textfield"
                    v-model="filterValueKey"
                    :placeholder="`${this.$resources['vn'].typeFilterValue}`"
                    :validate="validateCannotEmpty"
                    :maxLength=255
                    :label="this.$resources['vn'].filterValue"
                >
                </m-textfield>
                <m-date-picker
                    v-if="isShowDatePicker"
                    ref="datepicker"
                    v-model:dateModel="filterValueKey"
                    :validate="validateCannotEmpty"
                    :label="capitalizeFirstChar(title)"
                    @emitEnterInput="onClickApply()"
                >
                </m-date-picker>
                <m-textfield
                    v-if="isShowMonthField"
                    type="number"
                    ref="textfield"
                    v-model="filterValueKey"
                    :placeholder="this.$resources['vn'].typeMonth"
                    :validate="validateMonthField"
                    :maxLength=255
                    :label="this.$resources['vn'].month"
                >
                </m-textfield>
                <m-textfield
                    v-if="isShowYearField"
                    type="number"
                    ref="textfield"
                    v-model="filterValueKey"
                    :placeholder="this.$resources['vn'].typeYear"
                    :validate="validateYearField"
                    :maxLength=255
                    :label="this.$resources['vn'].year"
                >
                </m-textfield>
            </div>
            <div class="filter__footer">
                <m-button
                    :type="this.$enums.buttonType.primary"
                    :text="'Áp dụng'"
                    ref="apply"
                    :click="onClickApply"
                >
                </m-button>
                <m-button
                    :type="this.$enums.buttonType.secondary"
                    :text="'Bỏ lọc'"
                    :click="onClickRemove"
                >
                </m-button>
            </div>
        </div>
    </div>
</template>
<script>
import { isNullOrEmpty, capitalizeFirstChar } from "@/js/utils/string.js";
import { validateMonth, validateYear } from "@/js/form/validate.js";
import enums from '@/constants/enums.js'
import resources from '@/constants/resources.js'
const compareType = enums.compareType;
const filterType = enums.filterType;

export default {
    name: 'MISAFilter',
    props: {
        /**
         * Display name
         */
        title: {
            type: String,
        },
        /**
         * Column name
         */
        name: {
            type: String,
        },
        /**
         * Filter config contains type of filter, select list
         */
        filterConfig: {
            type: Object
        },
        /**
         * Filter item output
         */
        filterItem: {
            type: Object
        },
    },
    data() {
        return {
            /**
             * List of compare type when filter data type is text
             */
            textCompareSelects: [
                {
                    id: compareType.contain,
                    name: resources['vn'].contain
                },
                {
                    id: compareType.equal,
                    name: resources['vn'].equal
                },
                {
                    id: compareType.startWith,
                    name: resources['vn'].startWith
                },
                {
                    id: compareType.endWith,
                    name: resources['vn'].endWith
                },
                {
                    id: compareType.empty,
                    name: resources['vn'].empty
                },
            ],
            /**
             * List of compare type when filter data type is date
             */
            dateCompareSelects: [
                {
                    id: compareType.equal,
                    name: resources['vn'].equal
                },
                {
                    id: compareType.less,
                    name: resources['vn'].before
                },
                {
                    id: compareType.more,
                    name: resources['vn'].after
                },
                {
                    id: compareType.month,
                    name: resources['vn'].monthEqual
                },
                {
                    id: compareType.year,
                    name: resources['vn'].yearEqual
                },
                {
                    id: compareType.empty,
                    name: resources['vn'].empty
                }
            ],
            /**
             * List of compare type when filter data type is date
             */
            selectOneCompareSelects: [
                {
                    id: compareType.contain,
                    name: resources['vn'].equal
                }
            ],
            /**
             * Compare type of filter
             */
            compareType: {
                id: null,
                name: null,
            },
            /**
             * Filter value contain condition filter (key, name)
             */
            filterValues: [{ key: '', name: '' }],
            /**
             * Select list in case filter type is choice
             */
            selects: [],
            /**
             * Key of filter value
             */
            filterValueKey: '',
            /**
             * Name of filter value
             */
            filterValueName: '',
            /**
             * Number field config
             */
            numberField: {
                label: null,
                placeholder: null,
                validate: null,
            }
        }
    },
    created() {
        const compares = this.compareTypeSelects;
        this.compareType = { ...compares[0] };
        this.mapSelects();
    },
    mounted() {

    },
    expose: ['focus', 'focusOnApply'],
    computed: {
        /**
         * Load compare type to combobox
         * 
         * Author: nlnhat (25/07/2023)
         */
        compareTypeSelects() {
            switch (this.filterConfig.filterType) {
                case filterType.text:
                    return this.textCompareSelects;
                case filterType.date:
                    return this.dateCompareSelects;
                case filterType.selectOne:
                    return this.selectOneCompareSelects;
                default:
                    return this.dateCompareSelects;;
            }
        },
        /**
         * Show combobox select id or not 
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowSelectsId() {
            return (this.filterConfig.filterType == filterType.selectId
                && this.compareType.id != compareType.empty)
        },
        /**
         * Show combobox select name or not
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowSelectsName() {
            return (this.filterConfig.filterType == filterType.selectName
                && this.compareType.id != compareType.empty)
        },
        /**
         * Show textfield or not
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowTextField() {
            return (this.filterConfig.filterType == filterType.text
                && this.compareType.id != compareType.empty)
        },
        /**
         * Show datepicker or not 
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowDatePicker() {
            return (this.filterConfig.filterType == filterType.date
                && this.compareType.id != compareType.empty
                && this.compareType.id != compareType.month
                && this.compareType.id != compareType.year)
        },
        /**
         * Show month field or not
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowMonthField() {
            return (this.compareType.id == compareType.month)
        },
        /**
         * Show year field or not
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowYearField() {
            return (this.compareType.id == compareType.year)
        },
        /**
         * Show or not compare types select
         * 
         * Author: nlnhat (25/07/2023)
         */
        isShowCompareSelects() {
            return (this.filterConfig.filterType == filterType.text
                || this.filterConfig.filterType == filterType.date)
        },
        /**
         * Return filter data from compare type and filter value
         *
         * Author: nlnhat (25/07/2023)
         */
        filterDataComputed() {
            if (this.filterConfig.filterType == filterType.selectName)
                this.filterValueKey = this.filterValueName;

            this.filterValues[0].key = this.filterValueKey;
            this.filterValues[0].name = this.filterValueName;

            return {
                title: this.title,
                column: this.name,
                compareType: this.compareType.id,
                compareName: this.compareType.name.toLowerCase(),
                filterType: this.filterConfig.filterType,
                values: (this.compareType.id == compareType.empty) ? [] : [...this.filterValues],
            }
        }
    },
    watch: {
        /**
         * What compare type
         */
        compareType: {
            handler() {
                this.$nextTick(() => {
                    this.focus();
                })
            },
            deep: true
        },
        /**
         * What select from config to update select data
         */
        "filterConfig.selects": function () {
            this.mapSelects();
        },
    },
    methods: {
        /**
         * Handle when click apply filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        onClickApply() {
            const errorMessage = this.checkValidate();
            if (errorMessage == null) {
                const data = this.filterDataComputed;
                this.$emit('emitUpdateFilterItem', data);
                this.hideFilter();
            }
        },
        /**
         * Handle when click remove filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        onClickRemove() {
            if (this.filterItem != null)
                this.$emit('emitUpdateFilterItem', null);
            this.hideFilter();
        },
        /**
         * Hide this filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        hideFilter() {
            this.$emit('emitHideFilter');
        },
        /**
         * Focus input
         * 
         * Author: nlnhat (25/07/2023)
         */
        focus() {
            if (this.$refs.textfield != null) {
                this.$refs.textfield.focus();
                this.$refs.textfield.select();
            }
            else if (this.$refs.datepicker != null) {
                this.$refs.datepicker.focus();
                this.$refs.datepicker.select();
            }
            else if (this.$refs.combobox != null) {
                this.$refs.combobox.focus();
            }
            else this.focusOnApply();
        },
        /**
         * Clear input
         * 
         * Author: nlnhat (25/07/2023)
         */
        clearInput() {
            this.filterValues = [];
        },
        /**
         * Validate filter value cannot empty
         * 
         * Author: nlnhat (25/07/2023)
         * @param {*} label Label of filter
         * @param {*} value Filter value to check
         */
        validateCannotEmpty(label, value) {
            if (this.compareType.id == compareType.empty)
                return null;
            if (isNullOrEmpty(value)) {
                return `${label} ${this.$resources['vn'].cannotEmpty}`
            }
            return null;
        },
        /**
         * Validate month filter
         * 
         * Author: nlnhat (25/07/2023)
         * @param {*} label Label of filter
         * @param {*} value Filter value to check
         */
        validateMonthField(label, value) {
            if (this.compareType.id == compareType.empty)
                return null;
            if (isNullOrEmpty(value)) {
                return `${label} ${this.$resources['vn'].cannotEmpty}`
            }
            return this.validateMonth(label, value);
        },
        /**
         * Validate year filter
         * 
         * Author: nlnhat (25/07/2023)
         * @param {*} label Label of filter
         * @param {*} value Filter value to check
         */
        validateYearField(label, value) {
            if (this.compareType.id == compareType.empty)
                return null;
            if (isNullOrEmpty(value)) {
                return `${label} ${this.$resources['vn'].cannotEmpty}`
            }
            return this.validateYear(label, value);
        },
        /**
         * Check validate filter value
         * 
         * Author: nlnhat (25/07/2023)
         */
        checkValidate() {
            let errorMessage = null;

            // Validate date
            if (this.isShowDatePicker && this.$refs.datepicker != null) {
                errorMessage = this.$refs.datepicker.checkValidate();
                if (errorMessage != null) {
                    this.$refs.datepicker.focus()
                    return errorMessage;
                }
                return null
            }

            // Validate textfield
            if (this.isShowTextField || this.isShowMonthField || this.isShowYearField
                && this.$refs.textfield != null) {
                errorMessage = this.$refs.textfield.checkValidate();
                if (errorMessage != null) {
                    this.$refs.textfield.focus()
                    return errorMessage;
                }
                return null;
            }

            return null;
        },
        /**
         * Map select from props to data
         * 
         * Author: nlnhat (25/07/2023)
         */
        mapSelects() {
            if (this.filterConfig.selects) {
                this.selects = this.filterConfig.selects;
                this.filterValueKey = this.selects[0].id;
                this.filterValueName = this.selects[0].name;
            }
        },
        /**
         * Focus on button apply
         * 
         * Author: nlnhat (25/07/2023)
         */
        focusOnApply() {
            this.$nextTick(() => {
                this.$refs.apply.focus();
            })
        },
        /**
         * Handle short key on filter
         * 
         * Author: nlnhat (29/07/2023)
         * @param {*} event Keydown event on filter
         */
        onKeyDown(event) {
            // Enter: Apply
            if (event.keyCode == this.$enums.keyCode.enter) {
                event.preventDefault();
                event.stopPropagation();
                this.onClickApply();
            }
        },
        /**
         * Imported methods
         */
        capitalizeFirstChar,
        validateMonth,
        validateYear,
    }
}
</script>
<style scoped>.textfield {
    line-height: 35px;
}</style>