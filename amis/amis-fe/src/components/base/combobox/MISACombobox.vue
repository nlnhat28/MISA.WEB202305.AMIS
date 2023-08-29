<template>
    <div
        class="combobox"
        v-click-outside="hideSelects"
        @keydown="handleKeyDown"
    >
        <div
            :class="{ 'input-group': true, 'input--error': errorMessage }"
            v-tooltip:bottom="errorMessage"
            @click="toggleDisplaySelects"
        >
            <input
                :placeholder="this.$resources['vn'].selectValue"
                v-model="inputValue.name"
                :readonly="isReadOnly"
                ref="input"
            />
            <span class="divider"></span>
            <div class="input-dropdown">
                <m-icon icon="dropdown"></m-icon>
            </div>
        </div>
        <div
            class="p-relative"
            v-show="isShowSelects"
        >
            <ul
                class="select-list"
                ref="refSelects"
            >
                <m-no-content v-if="selects.length == 0"></m-no-content>
                <li
                    v-for="(select, index) in this.selects"
                    :key="select.id"
                    :class="{
                        'select-item': true,
                        'select-item--selected': select.id == inputValue.id,
                        'select-item--focused': index == focusedSelectIndex,
                    }"
                    @click="onClickSelect(select)"
                >
                    {{ select.name }}
                    <div
                        class="selected-item__icon"
                        v-show="select.id == inputValue.id"
                    >
                        <m-icon icon="check--green"></m-icon>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
import enums from '@/constants/enums.js';
import { isReadOnly } from 'vue';
import { debounce } from '@/js/utils/debounce.js';
import { isNullOrWhiteSpace } from '@/js/utils/string.js'
const direction = enums.direction;

export default {
    name: 'MISACombobox',
    props: {
        /**
         * (Props) Id of object
         */
        id: {
            type: [String, Number],
            default: null,
        },
        /**
         * (Props) Name of object or input value
         */
        name: {
            type: [String, Number],
            default: null,
        },
        /**
         * Tên nhãn
         */
        label: {
            type: String,
        },
        /**
         * Độ dài tối đa
         */
        maxLength: {
            type: Number
        },
        /**
         * (Props) List select in dropdown
         */
        theSelects: {
            type: Array,
        },
        /**
         * Focused or not
         */
        isFocused: {
            type: Boolean,
            default: false,
        },
        /**
         * Readonly input or not
         */
        isReadOnly: {
            type: Boolean,
            default: false,
        },
        /**
         * Function to validate
         */
        validate: {
            type: Function,
        },
        /**
         * Type of direction
         */
        direction: {
            type: Number,
            default: direction.down,
            validator: (value) => {
                return [
                    direction.down,
                    direction.up,
                ].includes(value);
            },
        },
        /**
         * Allow select value
         */
        canSearch: {
            type: Boolean,
            default: true,
        },
        /**
         * Allow un select
         */
        canUnselect: {
            type: Boolean,
            default: true,
        }
    },
    data() {
        return {
            /**
             * (Data) Selected input object includes id and name
             */
            inputValue: {
                id: this.id ?? null,
                name: this.name ?? null,
            },
            /**
             * (Data) List of selects in dropdown
             */
            selects: this.theSelects,
            /**
             * Show or hide dropdown
             */
            isShowSelects: false,
            /**
             * Error message when check validate
             */
            errorMessage: null,
            /**
             * Index of select is focused
             */
            focusedSelectIndex: 0,
        }
    },
    created() {
        this.onChangeName = this.debounce(this.handleChangeName, this.canSearch ? 200 : 0);
    },
    mounted() {
        if (this.isFocused) this.focus();
        this.scrollToSelectedItem();
    },
    beforeUnmount() {
        this.blur();
        this.$refs.refSelects.scrollTop = 0;
    },
    expose: ['checkValidate', 'focus', 'errorMessage'],
    watch: {
        /**
         * Watch change of the selects, id, name received from parent components
         * 
         * Author: nlnhat (01/07/2023)
         */
        theSelects: {
            handler(newValue) {
                this.selects = newValue;
                this.scrollToSelectedItem();
            },
            deep: true
        },
        id() {
            this.inputValue.id = this.id;
        },
        name() {
            this.inputValue.name = this.name;
        },
        /**
         * Watch change of input name
         * 
         * Author: nlnhat (01/07/2023)
         */
        "inputValue.name": function () {
            this.onChangeName()
        },
        /**
         * Watch change of focused index to scroll
         * 
         * Author: nlnhat (01/07/2023)
         */
        focusedSelectIndex() {
            if (this.focusedSelectIndex >= 0 && this.$refs.refSelects) {
                this.$refs.refSelects.scrollTop = this.scrollTopComputed;
            }
        },
    },
    computed: {
        /**
         * Get id of select by its name
         * 
         * Author: nlnhat (01/07/2023)
         */
        getIdByName() {
            try {
                const matchingSelect = this.selects.find(select => {
                    return (select.name == this.inputValue.name)
                });
                if (matchingSelect) {
                    this.selects = this.theSelects;
                    return matchingSelect.id;
                }
            } catch (error) {
                console.error(error);
            }
            return null;
        },
        /**
         * Validate value
         * 
         * Author: nlnhat (01/07/2023)
         * @returns Error message if invalid, null if valid
         */
        validateComputed() {
            try {
                if (this.maxLength) {
                    if (this.inputValue?.length > this.maxLength)
                        return `${this.label} ${resources['vn'].mustLessThan} ${++this.maxLength} ${resources['vn'].char}`;
                }
                if (this.validate) {
                    return this.validate(this.label, this.inputValue.name, this.selects)
                }
            } catch (error) {
                console.error(error);
            }
            return null
        },
        /**
         * Search in list
         * 
         * Author: nlnhat (06/07/2023)
         * @returns Selects include input
         */
        searchInList() {
            if (isNullOrWhiteSpace(this.inputValue.name)) return this.theSelects
            return this.theSelects.filter(select => {
                return select.name.toString().toLowerCase().includes(this.inputValue.name.toString().toLowerCase())
            });
        },
        /**
         * Search in list
         * 
         * Author: nlnhat (06/07/2023)
         * @returns Selects include input
         */
        scrollTopComputed() {
            if (this.selects.length > 0)
                return this.focusedSelectIndex / this.selects.length * 200
            return 0;
        }
    },
    methods: {
        debounce,
        /**
         * Focus on input
         * 
         * Author: nlnhat (01/07/2023)
         */
        focus() {
            this.$refs.input.focus();
        },
        /**
         * Blur input
         * 
         * Author: nlnhat (01/07/2023)
         */
        blur() {
            this.$refs.input.blur();
        },
        /**
         * Handle name change
         * 
         * Author: nlnhat (01/07/2023)
         */
        handleChangeName() {
            try {
                if (this.canSearch) this.selects = this.searchInList;
                this.inputValue.id = this.getIdByName;
                this.checkValidate();
                this.$emit('update:id', this.inputValue.id);
                this.$emit('update:name', this.inputValue.name);
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Handle when press up, down, enter from keyboard
         */
        handleKeyDown(event) {
            try {
                const keyCode = event.keyCode
                switch (keyCode) {
                    // Đi xuống
                    case this.$enums.keyCode.down:
                        if (this.isShowSelects == false)
                            this.showSelects();
                        if (this.focusedSelectIndex < this.selects.length - 1)
                            this.focusedSelectIndex++;
                        else
                            this.focusedSelectIndex = 0
                        break;
                    // Đi lên
                    case this.$enums.keyCode.up:
                        if (this.focusedSelectIndex > 0)
                            this.focusedSelectIndex--;
                        else
                            this.focusedSelectIndex = this.selects.length - 1
                        break;
                    // Enter: chọn
                    case this.$enums.keyCode.enter:
                        if (this.isShowSelects) {
                            const select = this.selects[this.focusedSelectIndex]
                            this.onClickSelect(select)
                        };
                        break;
                    // Tab: Đóng
                    case this.$enums.keyCode.tab:
                        this.isShowSelects = false;
                        break;
                    default:
                        break;
                }
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Check validate value
         * 
         * Author: nlnhat (01/07/2023)
         * @param {*} value Value to validate 
         */
        checkValidate() {
            try {
                return this.errorMessage = this.validateComputed;
            } catch (error) {
                console.error(error);
                return null
            }
        },
        /**
         * Handle event click on a select
         * 
         * Author: nlnhat (01/07/2023)
         * @param {*} select Select on clicked
         */
        onClickSelect(select) {
            try {
                // Un select
                if (select.id == this.inputValue.id) {
                    if (this.canUnselect) {
                        this.inputValue.name = null;
                    }
                    this.focus();
                }
                // Select
                else {
                    this.inputValue.name = select.name;
                    this.onSelected();
                }
                this.isShowSelects = false;
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Show select list 
         * 
         * Author: nlnhat (01/07/2023)
         */
        showSelects() {
            this.isShowSelects = true;
            this.scrollToSelectedItem();
        },
        /**
         * Hide select list 
         * 
         * Author: nlnhat (01/07/2023)
         */
        hideSelects() {
            this.isShowSelects = false;
        },
        /**
         * Toggle show or hide select list
         * 
         * Author: nlnhat (01/07/2023)
         */
        toggleDisplaySelects() {
            if (this.isShowSelects) {
                this.isShowSelects = false;
            }
            else {
                this.showSelects();
            }
            this.focus();
        },
        /**
         * Handle after selected
         * 
         * Author: nlnhat (01/07/2023)
         */
        onSelected() {
            this.$emit('emitSelected');
        },
        /**
         * Scroll to selected item
         * 
         * Author: nlnhat (01/07/2023)
         */
        scrollToSelectedItem() {
            const index = this.selects.findIndex(select => select.id == this.inputValue.id);
            this.focusedSelectIndex = index ?? 0;
            this.$nextTick(() => {
                this.$refs.refSelects.scrollTop = this.scrollTopComputed;
            })
        }
    },
}
</script>
<style>
.select-list {
    max-height: 200px;
}

.select-list:has(.mask) {
    min-height: 200px;
}
</style>