import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './providers/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'app/list',
    loadChildren: () => import('./modules/exercise-list/exercise-list.module').then(m => m.ExerciseListModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'app/commit',
    loadChildren: () => import('./modules/commit/commit.module').then(m => m.CommitModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'app/login',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
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
