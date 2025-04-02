import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { User } from '../../auth/auth.service';
import { UserService } from './service/user.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  user: User =
  {
    name: '',
    email: '',
    password: '',
  }

  constructor(private route: Router,private userService: UserService) { }
  
  ngOnInit(): void {
    this.userService.getUserProfile().subscribe(
      (response: any) => {
        this.user = response;
      }
    );
  }
  
  logOut() 
  {
    localStorage.removeItem('token');
    console.log('Saindo...');
    this.route.navigate(['/login']);
  }
}
