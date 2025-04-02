import { Routes } from '@angular/router';
import { LoginComponentComponent } from './core/auth/components/login-component/login-component.component';
import { RegisterComponentsComponent } from './core/auth/components/register-components/register-components.component';
import { MainLayoutComponentComponent } from './core/layouts/main-layout-component/main-layout-component.component';
import { AuthLayoutComponentComponent } from './core/layouts/auth-layout-component/auth-layout-component.component';
import { HomePageComponent } from './core/pages/home-page/home-page.component';
import { UserformComponent } from './shared/components/userform/userform.component';
import { NewsFormComponent } from './shared/components/news-form/news-form.component';
import { AuthGuard } from './auth.guard';
import { UserProfileComponent } from './core/pages/user-profile/user-profile.component';
import { MyNewsComponent } from './core/pages/my-news/my-news.component';
import { EditNewsComponent } from './core/pages/edit-news/edit-news.component';
import { NewsDetailsComponent } from './core/pages/news-details/news-details.component';

export const routes: Routes = 
[
  {
    path: '',
    component: MainLayoutComponentComponent, 
    children: [
      { path: 'home', component: HomePageComponent },
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'create-news', component: NewsFormComponent, canActivate: [AuthGuard] },
      { path: 'my-news', component: MyNewsComponent, canActivate: [AuthGuard] },
      { path: 'edit-news/:id', component: EditNewsComponent, canActivate: [AuthGuard] },
      { path: 'news-details/:id', component: NewsDetailsComponent},
      { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuard]},
      { path: 'register', component: RegisterComponentsComponent, canActivate: [AuthGuard] },
    ]
  },
  {
    path: '',
    component: AuthLayoutComponentComponent, 
    children: [
      { path: 'login', component: LoginComponentComponent },
    ]
  }
];
