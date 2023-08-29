<template>
    <div
        class="popup-container"
        @mousemove="onMove"
        @mouseup="endMove"
    >
        <div
            class="popup"
            ref="refPopup"
        >
            <slot name="mask"></slot>
            <div
                class="popup__header"
                @mousedown="startMove"
                @dblclick="resetPosition"
            >
                <div class="popup__header--left">
                    <div class="popup__title">{{ title }}</div>
                    <slot name="headLeft"></slot>
                </div>
                <div
                    class="icon-container icon-close"
                    @click="onClose()"
                    v-tooltip="'Đóng (Esc)'"
                >
                    <m-icon-x></m-icon-x>
                </div>
            </div>
            <div class="popup__body">
                <slot name="body"></slot>
            </div>
            <div class="popup__footer">
                <slot name="footer"></slot>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MISAPopup',
    props: {
        /**
         * Function to hide popup
         */
        closePopup: {
            type: Function,
        },
        /**
         * Title of popup
         */
        title: {
            type: String,
        }
    },
    data() {
        return {
            /**
             * Đang di chuyển hay không
             */
            isMoving: false,
            /**
             * Offset toạ độ mới so với toạ độ cũ
             */
            offset: [0, 0],
            /**
             * Popup content
             */
            refPopup: null,
        }
    },
    mounted() {
        this.refPopup = this.$refs.refPopup;
    },
    methods: {
        /**
         * Đóng popup
         * 
         * Author: nlnhat (01/07/2023)
         */
        onClose() {
            this.$emit('emitClose')
        },
        /**
         * Bắt đầu di chuyển popup
         * 
         * Author: nlnhat (01/07/2023)
         * @param {*} event Sự kiện ấn chuột
         */
        startMove(event) {
            this.isMoving = true;
            this.offset = [
                this.refPopup.offsetLeft - event.clientX,
                this.refPopup.offsetTop - event.clientY
            ];
        },
        /**
         * Di chuyển popup
         * 
         * Author: nlnhat (01/07/2023)
         * @param {*} event Sự kiện di chuyển chuột
         */
        onMove(event) {
            if (this.isMoving) {
                event.preventDefault();
                this.refPopup.style.left = (event.clientX + this.offset[0]) + 'px';
                this.refPopup.style.top = (event.clientY + this.offset[1]) + 'px';
            }
        },
        /**
         * Thả popup
         * 
         * Author: nlnhat (01/07/2023)
         */
        endMove() {
            this.isMoving = false;
        },
        /**
         * Đưa popup về vị trí ban đầu (giữa màn hình)
         * 
         * Author: nlnhat (01/07/2023)
         */
        resetPosition() {
            this.refPopup.style.left = '50%';
            this.refPopup.style.top = '50%';
        },
    }
}
</script>
<style scoped>.popup__header .icon-container.icon-close {
    padding: 8px;
    box-sizing: content-box;
}</style>