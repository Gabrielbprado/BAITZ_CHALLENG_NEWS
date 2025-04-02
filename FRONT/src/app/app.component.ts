import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserformComponent } from "./shared/components/userform/userform.component";
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserformComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Changed styleUrl to styleUrls
})
export class AppComponent {
  title = 'AmsNews';
}
