import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommitComponent } from './commit/commit.component';

const routes:Routes = [
    {
        path: 'app/commit', component: CommitComponent         
    },
    {
        path: '', redirectTo: 'app/commit', pathMatch: 'full'
    },
    {
        path: '**', redirectTo: 'app/commit', pathMatch: 'full'
    }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class SpaRoutingModule { }
