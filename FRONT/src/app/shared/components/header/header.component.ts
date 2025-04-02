import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  private ownerTokenKey = 'token';

  isAuthenticated(): boolean {
    const token = localStorage.getItem("token");
    console.log('isAuthenticated',token !== null);
    return token !== null;
  }

  isOwner(): boolean {
    const result = this.isAuthenticated();
    console.log('isOwner called, result:', result);
    return result;
}


}
