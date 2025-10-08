import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-menu-component',
  imports: [RouterModule],
  templateUrl: './nav-menu-component.html',
  styleUrl: './nav-menu-component.css',
})
export class NavMenuComponent {
  isMenuCollapsed: boolean = true;
}
