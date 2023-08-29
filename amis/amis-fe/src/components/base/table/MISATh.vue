<template>
    <th
        :style="`text-align: ${textAlign}; width: ${width}px; min-width: ${width}px;`"
        ref="th"
        @mousemove="resize"
        @mouseup="endResize"
    >
        <!-- default slot content -->
        <span v-tooltip:bottom="fullTitle">
            <slot></slot>
        </span>
        <m-icon-container
            v-if="sortType != this.$enums.sortType.disabled"
            :icon="sortIcon"
            :isBtn="true"
            class="th__sort"
            @click="onClickSortType()"
            v-tooltip:left="this.$resources['vn'].sort"
        >
        </m-icon-container>
        <div
            class='filter-wrapper'
            v-if="filterConfig"
            v-click-outside="hideFilter"
        >
            <m-icon-container
                :icon="'filter--blue'"
                :class="{ 'th__filter': true, 'th__filter--active': filterData, 'th__filter--show': isShowFilter }"
                :isBtn="true"
                @click="toggleDisplayFilter"
                v-tooltip:left="this.$resources['vn'].filter"
            >
            </m-icon-container>
            <m-filter
                :filterConfig="filterConfig"
                :filterItem="filterItem"
                :name="name"
                :title="fullTitle ?? title"
                v-show="isShowFilter"
                @emitUpdateFilterItem="updateFilterItem"
                @emitHideFilter="hideFilter"
                ref="filter"
            >
            </m-filter>
        </div>
        <span
            class="th__resize"
            @mousedown="startResize"
        ></span>
    </th>
</template>
<script>
import enums from '@/constants/enums.js'
const sortType = enums.sortType;

export default {
    name: 'MISATh',
    props: {
        /**
         * Width of cell
         */
        widthCell: {
            type: [String, Number],
            default: 160,
        },
        /**
         * Text align of cell
         */
        textAlign: {
            type: String,
            default: 'left',
            validator: (value) => {
                return [
                    'left',
                    'center',
                    'right',
                ].includes(value);
            },
        },
        /**
         * Name of column
         */
        name: {
            type: String
        },
        /**
         * Title of column
         */
        title: {
            type: String
        },
        /**
         * Tooltip string
         */
        fullTitle: {
            type: String,
            default: null
        },
        /**
         * Kiểu sắp xếp
         */
        sortType: {
            type: Number,
            default: null,
            validator: (value) => {
                return [
                    sortType.disabled,
                    sortType.blur,
                    sortType.asc,
                    sortType.desc,
                ].includes(value);
            },
        },
        /**
         * (Prop) Câu truy vấn sort
         */
        sortItem: {
            type: String
        },
        /**
         * Filter config
         */
        filterConfig: {
            type: Object,
            default: null,
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
             * Width of cell
             */
            width: parseInt(this.widthCell),
            /**
             * Is resizing or not
             */
            isResizing: false,
            /**
             * Position X before resize
             */
            startX: 0,
            /**
             * Sort state
             */
            sort: this.sortType,
            /**
             * Sort icon
             */
            sortIcon: 'arrow-blur',
            /**
             * Sort query
             */
            sortQuery: null,
            /**
             * Filter data
             */
            filterData: null,
            /**
             * Show filter or not
             */
            isShowFilter: false,
        }
    },
    watch: {
        /**
         * Watch sort data
         */
        sort() {
            this.sortIcon = this.sortIconComputed;
            this.sortQuery = this.sortQueryComputed;
            this.$emit('update:sortItem', this.sortQuery)
        },
        /**
         * Watch fitlter item to update filter data
         */
        filterItem: {
            handler() {
                this.filterData = this.filterItem;
            },
            deep: true
        }
    },
    computed: {
        /**
         * Đổi icon sort
         * 
         * Author: nlnhat (20/07/2023)
         */
        sortIconComputed() {
            switch (this.sort) {
                case this.$enums.sortType.asc:
                    return 'arrow-up'
                case this.$enums.sortType.desc:
                    return 'arrow-down'
                default:
                    return 'arrow-blur';
            }
        },
        /**
         * Đổi truy vấn sort
         * 
         * Author: nlnhat (20/07/2023)
         */
        sortQueryComputed() {
            switch (this.sort) {
                case this.$enums.sortType.asc:
                    return `+${this.name}`
                case this.$enums.sortType.desc:
                    return `-${this.name}`
                default:
                    return null;
            }
        },
        /**
         * Show filter textfield or not
         * 
         * Author: nlnhat (20/07/2023)
         */
        isShowTextField() {
            return (
                this.filterConfig.filterType == this.$enums.filterType.text ||
                this.filterConfig.filterType == this.$enums.filterType.date)
        },
    },
    methods: {
        /**
         * Start resize when mouse down
         * 
         * Author: nlnhat (13/07/2023)
         * @param {*} event Mouse event
         */
        startResize(event) {
            this.isResizing = true;
            this.startX = event.clientX;
        },
        /**
         * Resize when mouse move
         * 
         * Author: nlnhat (13/07/2023)
         * @param {*} event Mouse event
         */
        resize(event) {
            if (this.isResizing) {
                const deltaX = event.clientX - this.startX;
                this.width = Math.max(100, this.width + deltaX);
                this.startX = event.clientX;
            }
        },
        /**
         * End resize when mouse up
         * 
         * Author: nlnhat (13/07/2023)
         * @param {*} event Mouse event
         */
        endResize() {
            this.isResizing = false;
        },
        /**
         * Return width of th 
         */
        getWidth() {
            return this.$refs.th.offsetWidth;
        },
        /**
         * Change sort type
         * 
         * Author: nlnhat (20/07/2023)
         */
        onClickSortType() {
            try {
                switch (this.sort) {
                    case this.$enums.sortType.blur:
                        this.sort = this.$enums.sortType.asc
                        break;
                    case this.$enums.sortType.asc:
                        this.sort = this.$enums.sortType.desc
                        break;
                    case this.$enums.sortType.desc:
                        this.sort = this.$enums.sortType.blur
                        break;
                    default:
                        break;
                }
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Toggle show or hide filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        toggleDisplayFilter() {
            this.isShowFilter = !this.isShowFilter;
            if (this.isShowFilter) {
                this.$nextTick(() => {
                    this.$refs.filter.focus();
                })
            }
        },
        /**
         * Show filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        showFilter() {
            this.isShowFilter = true;
        },
        /**
         * Hide filter
         * 
         * Author: nlnhat (25/07/2023)
         */
        hideFilter() {
            this.isShowFilter = false;
        },
        /**
         * Hide filter
         * 
         * Author: nlnhat (25/07/2023)
         * @param {*} filterData Filter data from filter component
         */
        updateFilterItem(filterData) {
            this.$emit('update:filterItem', filterData);
        }
    },
}
</script>
<style scoped>
.icon-container {
    display: inline-flex;
}

.icon-container.th__filter {
    display: none;
    position: absolute;
}

.th__filter.icon-container.th__filter--show {
    display: inline-flex;
}

.th__filter.icon-container.th__filter--active {
    display: inline-flex;
}
</style>