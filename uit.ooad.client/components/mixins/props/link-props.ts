import { Prop } from 'nuxt-property-decorator';
import Vue from 'vue';
import { Mixin } from 'vue-mixin-decorator';

// Copy and convert from bootstrap-vue/es/components/link/link.js
@Mixin
export class LinkProps extends Vue {
    @Prop({ default: null })
    href: string;

    @Prop({ default: null })
    rel: string;

    @Prop({ default: '_self' })
    target: string;

    @Prop({ default: false })
    active: boolean;

    @Prop({ default: 'active' })
    activeClass: string;

    @Prop({ default: false })
    append: boolean;

    @Prop({ default: false })
    disabled: boolean;

    @Prop({ default: 'click' })
    event: [string, any[]];

    @Prop({ default: false })
    exact: boolean;

    @Prop({ default: 'active' })
    exactActiveClass: string;

    @Prop({ default: false })
    replace: boolean;

    @Prop({ default: 'a' })
    routerTag: string;

    @Prop({ default: null })
    to: [string, Record<string, any>];
}