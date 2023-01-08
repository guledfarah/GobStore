import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProductCreateComponent } from './product/product-create/product-create/product-create.component';
import { ProductListComponent } from './product/product-list/product-list.component';
import { AuthGuard } from './services/guard/auth/auth.guard';
import { RoleGuard } from './services/guard/role/role.guard';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  {
    path: "product", component: ProductListComponent,
    children: [{
      path: "", component: ProductListComponent
    },
    { path: "create", component: ProductCreateComponent, canActivate: [RoleGuard] },
    { path: "Edit/:id", component: ProductCreateComponent }
    ], canActivate: [AuthGuard]
  },
  { path: "login/:code", component: LoginComponent },
  { path: "login", component: LoginComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
