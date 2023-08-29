<template>
    <div
        class="textfield"
        v-tooltip:bottom="errorMessage"
    >
        <div
            :class="{ 'input-group': true, 'input-group--right': action }"
            @click="focus()"
        >
            <input
                :type="type"
                :class="{ 'input--error': errorMessage }"
                :placeholder="placeholder"
                ref="input"
                v-model="inputValue"
                @input="onChangeValue()"
                :readonly="isReadOnly"
            />
            <div
                v-if="action"
                class="input-icon input-icon--right icon-container"
                @click="onAction"
                v-tooltip="isLoading ? null : action.title"
            >
                <m-icon
                    :icon="action.icon"
                    :isBtn="true"
                    v-if="!isLoading && !isSuccess"
                ></m-icon>
                <m-icon-container
                    icon="check--green"
                    v-if="isSuccess"
                ></m-icon-container>
                <m-spinner v-if="isLoading && !isSuccess"></m-spinner>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MISATextfield',
    props: {
        /**
         * Type of input
         */
        type: {
            type: String,
            default: 'text'
        },
        /**
         * Focused or not
         */
        isFocused: {
            type: Boolean,
            default: false,
        },
        /**
         * Readonly or not
         */
        isReadOnly: {
            type: Boolean,
            default: false,
        },
        /**
         * Model binding
         */
        modelValue: {
            type: [String, Number]
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
            type: Number,
        },
        /**
         * Placeholder
         */
        placeholder: {
            type: String,
        },
        /**
         * Function to validate
         */
        validate: {
            type: Function,
        },
        /**
         * Function to format
         */
        format: {
            type: Function,
        },
        /**
         * Hành động đi kèm
         */
        action: {
            type: Object,
            default: null
        },
    },
    data() {
        return {
            /**
             * Giá trị text fields
             */
            inputValue: this.modelValue ?? null,
            /**
             * Thông báo lỗi nếu có
             */
            errorMessage: null,
            /**
             * Trạng thái thực hiện hành động
             */
            isLoading: {
                type: Boolean,
                default: false
            },
            /**
             * Show icon after success action
             */
            isSuccess: false,
        }
    },
    created() {
        if (this.type == 'date') {
            this.inputValue = this.formatDateComputed;
        };
        this.inputValue = this.formatComputed;
        this.isLoading = false;
        this.isSuccess = false;
    },
    mounted() {
        if (this.isFocused) this.focus();
    },
    beforeUnmount() {
        this.blur();
    },
    expose: ['checkValidate', 'focus', 'select', 'errorMessage', 'clearErrorMessage', 'setErrorMessage'],
    watch: {
        modelValue() {
            this.inputValue = this.modelValue
        },
        inputValue() {
            this.onChangeValue();
        },
    },
    computed: {
        /**
         * Validate value
         * 
         * Author: nlnhat (01/07/2023)
         * @return Error message if invalid, null if valid
         */
        validateComputed() {
            try {
                // Validate length
                if (this.maxLength && this.inputValue?.length > this.maxLength)
                    return `${this.label} ${this.$resources['vn'].mustLessThan} ${this.maxLength + 1} ${this.$resources['vn'].char}`;

                // Custom validate
                if (this.validate) {
                    return this.validate(this.label, this.inputValue);
                }
            } catch (error) {
                console.error(error);
            }
            return null;
        },
        /**
         * Format value
         * 
         * Author: nlnhat (01/07/2023)
         * @return Formated value
         */
        formatComputed() {
            try {
                if (this.format)
                    return this.format(this.inputValue)
            } catch (error) {
                console.error(error);
            }
            return this.inputValue;
        },
        /**
         * Format date 
         *
         * Author: nlnhat (01/07/2023)
         * @return Date format yyyy/MM/dd
         */
        formatDateComputed() {
            try {
                if (this.inputValue != null) {
                    let date = new Date(this.inputValue);
                    const dateOfMonth = date.getDate().toString().padStart(2, "0");
                    const month = (date.getMonth() + 1).toString().padStart(2, "0");
                    const year = date.getFullYear();
                    return `${year}-${month}-${dateOfMonth}`;
                }
            } catch (error) {
                console.error(error);
            }
            return null;
        }
    },
    methods: {
        /**
         * Focus on input
         * 
         * Author: nlnhat (01/07/2023)
         */
        focus() {
            if (this.$refs.input)
                this.$refs.input.focus();
        },
        /**
         * Blur input
         * 
         * Author: nlnhat (01/07/2023)
         */
        blur() {
            if (this.$refs.input)
                this.$refs.input.blur();
        },
        /**
         * Select all text input
         * 
         * Author: nlnhat (30/07/2023)
         */
        select() {
            if (this.$refs.input)
                this.$refs.input.select();
        },
        /**
         * Handle input value change
         * 
         * Author: nlnhat (01/07/2023)
         */
        onChangeValue() {
            try {
                this.checkValidate();
                this.inputValue = this.formatComputed;
                this.$emit('update:modelValue', this.inputValue)
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Check validate value
         * 
         * Author: nlnhat (02/07/2023)
         */
        checkValidate() {
            try {
                return this.errorMessage = this.validateComputed;
            } catch (error) {
                console.error(error);
            }
            return null
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
                if (await func()) {
                    this.isSuccess = true;
                    await new Promise((resolve) => setTimeout(resolve, 1500));
                    this.isSuccess = false;
                }
            } catch (error) {
                console.error(error);
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Handle when click action
         * 
         * Author: nlnhat (23/07/2023)
         */
        onAction() {
            this.makeLoadingEffect(this.action.method)
        },
        /**
         * Set error message
         * 
         * Author: nlnhat (23/07/2023)
         * @param {*} message Message to set for error message
         */
        setErrorMessage(message) {
            this.errorMessage = message;
        },
        /**
         * Clear error message
         * 
         * Author: nlnhat (23/07/2023)
         */
        clearErrorMessage() {
            this.errorMessage = null;
        },
    }
}
</script>
<style scoped>
.icon-container:hover .icon-btn {
    filter: invert(0.4)
}

.icon-container:active .icon-btn {
    filter: brightness(0.6)
}

.icon-container {
    position: absolute;
}

.input-icon {
    display: none;
}

.input-group:hover .input-icon,
input:focus~.input-icon {
    display: flex;
}

.loading-spinner {
    position: absolute;
    scale: 0.28;
}

.icon--xmark {
    scale: 0.7;
}</style>
