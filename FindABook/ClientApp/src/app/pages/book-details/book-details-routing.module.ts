import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailPage } from './book-details';

const routes: Routes = [
  {
    path: '',
    component: BookDetailPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookDetailPageRoutingModule { }
