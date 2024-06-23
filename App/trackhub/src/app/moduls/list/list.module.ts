import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ListComponent } from './list.component';

@NgModule({
    declarations: [
        ListComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: '', component: ListComponent }
        ])
    ],
    providers: [
        
    ],
    exports: [
        
    ]
})
export class ListModule { }
