<template>
    <div v-show="show" class="popup" @click="close">
        <div ref="popup" class="popup-modal m-3" @click.stop>
            <div class="popup-modal-title">
                <div class="text">{{ title }}</div>
                <b-button variant="white" class="close" @click="close">
                    <icon- class="m-0 w-auto" i="x" />
                </b-button>
            </div>
            <div class="popup-modal-content">
                <slot v-if="noData || data" :data="data" :close="close" />
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { Component, Prop, mixins } from 'nuxt-property-decorator';
import { ClickawayMixin } from '~/components/mixins/clickaway';

@Component({
    name: 'popup-',
})
export default class extends mixins<ClickawayMixin>(ClickawayMixin) {
    show: boolean = false;
    data: any = null;

    @Prop({ required: true, type: String })
    title!: string;

    @Prop({ default: undefined })
    refs: any;

    @Prop({ default: false, type: Boolean })
    noData!: boolean;

    close() {
        this.data = null;
        this.show = false;
    }

    open(data: any) {
        this.data = data;
        this.show = true;
    }
}
</script>
<style lang="scss">
.popup {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 5;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    background: rgba($black, 0.2);

    > .popup-modal {
        min-width: 300px;
        background: $white;
        border-radius: $border-radius;
        box-shadow: $box-shadow-context;

        > .popup-modal-title {
            display: flex;
            border-bottom: 1.5px solid $border-color;
            opacity: 0.8;

            > .text {
                flex: 1;
                padding: 0 0.75rem;
                font-weight: 500;
                line-height: $input-height;
            }

            > .close {
                display: block;
                width: $input-height;
                min-width: $input-height;
                min-height: $input-height;
                padding: 0;
                border: none;
            }
        }
    }
}
</style>