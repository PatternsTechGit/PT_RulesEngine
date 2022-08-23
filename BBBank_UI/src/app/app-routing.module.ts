import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransferFundsComponent } from './transfer-funds/transfer-funds.component';

const routes: Routes = [
  { path: '', component: TransferFundsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
