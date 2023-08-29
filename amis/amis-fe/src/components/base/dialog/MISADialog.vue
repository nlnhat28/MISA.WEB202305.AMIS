<template>
    <div class="popup-container">
        <div class="dialog">
            <div class="dialog__header">
                <div class="dialog__title">
                    {{ title }}
                </div>
                <div
                    class="icon-container icon-close"
                    @click="onClose()"
                    v-tooltip="'Đóng (Esc)'"
                >
                    <m-icon-x></m-icon-x>
                </div>
            </div>
            <div class="dialog__content">
                <div class="icon-container">
                    <m-icon :icon="icon"></m-icon>
                </div>
                {{ content }}
            </div>
            <div class="dialog__footer">
                <slot name="footer"></slot>
            </div>
        </div>
    </div>
</template>
<script>
import enums from '@/constants/enums.js'
import resources from '@/constants/resources.js'
const dialogType = enums.dialogType;

export default {
    name: 'MISADialog',
    props: {
        /**
         * Type of dialog
         */
        type: {
            type: Number,
            default: resources['vn'].info,
            validator: (value) => {
                return [
                    dialogType.question,
                    dialogType.danger,
                    dialogType.error,
                    dialogType.warning,
                    dialogType.info,
                ].includes(value);
            },
        },
        /**
          * Title of dialog
          */
        title: {
            type: String,
            default: resources['vn'].info,
        },
        /**
         * Content of dialog
         */
        content: {
            type: String,
            default: null
        },
    },
    data() {
        return {
            /**
             * Icon class
             */
            icon: ''
        }
    },
    created() {
        this.setClass();
    },
    methods: {
        /**
         * Close dialog
         * 
         * Author: nlnhat (04/07/2023)
         */
        onClose() {
            this.$emit('emitCloseDialog')
        },
        /**
         * Set class by type of dialog
         * 
         * Author: nlnhat (04/07/2023)
         */
        setClass() {
            switch (this.type) {
                case dialogType.question:
                    this.icon = 'question';
                    break;
                case dialogType.danger:
                    this.icon = 'danger';
                    break;
                case dialogType.error:
                    this.icon = 'danger';
                    break;
                case dialogType.warning:
                    this.icon = 'warning--lg';
                    break;
                default:
                    this.icon = 'info--lg';
                    break;
            }
        }
    }
}
</script>
<style scoped>
.dialog__header .icon-container.icon-close {
    padding: 4px;
    box-sizing: content-box;
}</style>