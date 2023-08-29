<template>
    <div class="icon-container">
        <i :class="['icon', `icon--${icon}`, { 'icon-btn': isBtn }]"></i>
    </div>
</template>
<script>
export default {
    name: 'MISAIconContainer',
    props: {
        /**
         * Icon name
         */
        icon: {
            type: String
        },
        /**
         * Is a button icon or not
         */
        isBtn: {
            type: Boolean,
            default: false
        }
    }
}
</script>