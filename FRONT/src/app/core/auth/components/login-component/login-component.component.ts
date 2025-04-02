import { Component } from '@angular/core';
import { UserformComponent } from "../../../../shared/components/userform/userform.component";

@Component({
  selector: 'app-login-component',
  standalone: true,
  imports: [UserformComponent],
  template: `<app-userform [isLogin]="true"></app-userform>`, 
  styleUrl: './login-component.component.css'
})
export class LoginComponentComponent {

}
