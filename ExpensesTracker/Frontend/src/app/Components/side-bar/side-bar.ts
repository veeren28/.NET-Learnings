import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routes } from '../../app.routes';
import { RouterEvent, RouterModule } from '@angular/router';
@Component({
  selector: 'app-side-bar',
  imports: [CommonModule, RouterModule],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css',
})
export class SideBar {
  isSideBarOpen = true;
  ToggleSideBar() {
    this.isSideBarOpen = !this.isSideBarOpen;
  }
}
