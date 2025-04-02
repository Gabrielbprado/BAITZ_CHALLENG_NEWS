import { Component } from '@angular/core';
import { HeaderComponent } from "../../../shared/components/header/header.component";
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "../../../shared/components/footer/footer.component";

@Component({
  selector: 'app-main-layout-component',
  standalone: true,
  imports: [HeaderComponent, RouterOutlet, FooterComponent],
  templateUrl: './main-layout-component.component.html',
  styleUrl: './main-layout-component.component.css'
})
export class MainLayoutComponentComponent {

}
