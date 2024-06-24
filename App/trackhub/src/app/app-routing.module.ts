import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './providers/guard/auth.guard';

const routes: Routes = [
    {
        path: 'app/commit',
        loadChildren: () => import('./moduls/commit/commit.module').then(m => m.CommitModule),
 
    },
    {
        path: 'app/list',
        loadChildren: () => import('./moduls/exericse-list/exercise-list.module').then(m => m.ExerciseListModule),
        
    },
    {
        path: 'app/login',
        loadChildren:() => import('./moduls/auth/auth.module').then(m => m.AuthModule)
    },
    {
        path: '', redirectTo: 'app/list', pathMatch: 'full'
    },
    {
        path: '**', redirectTo: 'app/list', pathMatch: 'full'
    }
];
@NgModule({ 
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
