import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AccordionModule } from 'primeng/accordion';
import { AnimateOnScrollModule } from 'primeng/animateonscroll';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { CheckboxModule } from 'primeng/checkbox';
import { ChipModule } from 'primeng/chip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { DividerModule } from 'primeng/divider';
import { DrawerModule } from 'primeng/drawer';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { FieldsetModule } from 'primeng/fieldset';
import { FileUploadModule } from 'primeng/fileupload';
import { FloatLabelModule } from 'primeng/floatlabel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputIconModule } from 'primeng/inputicon';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { MessageModule } from 'primeng/message';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { PaginatorModule } from 'primeng/paginator';
import { PasswordModule } from 'primeng/password';
import { ProgressBarModule } from 'primeng/progressbar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RippleModule } from 'primeng/ripple';
import { ScrollerModule } from 'primeng/scroller';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { ScrollTopModule } from 'primeng/scrolltop';
import { SelectModule } from 'primeng/select';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SplitButtonModule } from 'primeng/splitbutton';
import { StyleClassModule } from 'primeng/styleclass';
import { TableModule } from 'primeng/table';
import { TabsModule } from 'primeng/tabs';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { ToastModule } from 'primeng/toast';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { ToolbarModule } from 'primeng/toolbar';
import { TooltipModule } from 'primeng/tooltip';
import { LoaderComponent } from './components/loader/loader.component';

@NgModule({
  declarations: [
    LoaderComponent,
  ],
  imports: [
    CommonModule,
    ProgressSpinnerModule,
    DialogModule
  ],
  exports: [
    RadioButtonModule,
    ScrollPanelModule,
    ScrollTopModule,
    ToggleSwitchModule,
    ProgressSpinnerModule,
    ConfirmDialogModule,
    InputNumberModule,
    ScrollerModule,
    DynamicDialogModule,
    PaginatorModule,
    ChipModule,
    TieredMenuModule,
    InputMaskModule,
    FileUploadModule,
    MessageModule,
    CheckboxModule,
    TabsModule,
    FieldsetModule,
    TextareaModule,
    SplitButtonModule,
    AccordionModule,
    ToastModule,
    ChartModule,
    CheckboxModule,
    TooltipModule,
    DialogModule,
    TagModule,
    IconFieldModule, 
    InputIconModule,
    FloatLabelModule,
    DatePickerModule,
    ButtonModule,
    InputTextModule,
    PasswordModule,
    DividerModule,
    DrawerModule,
    ToolbarModule,
    MenuModule,
    AvatarModule,
    RippleModule,
    SplitButtonModule,
    BadgeModule,
    OverlayBadgeModule,
    MenubarModule,
    StyleClassModule,
    ProgressBarModule,
    CardModule,
    BreadcrumbModule,
    InputGroupAddonModule,
    InputGroupModule,
    TableModule,
    AnimateOnScrollModule,
    SelectButtonModule,
    AutoCompleteModule,
    SelectModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    LoaderComponent
  ],
  providers: []
})
export class SharedModule { }
