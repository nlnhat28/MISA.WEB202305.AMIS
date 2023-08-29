<template>
    <div class="mask">
        <img
            class="mask__icon"
            src="@/assets/img/no-content.svg"
        >
        <div class="mask__message">
            {{ message }}
        </div>
    </div>
</template>
<script>
export default {
    name: 'MISANoContent',
    data() {
        return {
            message: this.$resources['vn'].noContent,
        }
    }
}
</script>