import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: 'app/commit',
            loadChildren: () => import('./moduls/commit/commit.module').then(m => m.CommitModule)
    },
    {
        path: 'app/list',
            loadChildren: () => import('./moduls/list/list.module').then(m => m.ListModule)
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
export class AppRoutingModule { }
