import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routes } from '../../app.routes';
import { Router, RouterEvent, RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-side-bar',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css',
})
export class SideBar {
  isSideBarOpen = true;
  ToggleSideBar() {
    this.isSideBarOpen = !this.isSideBarOpen;
  }
  constructor(private auth: AuthService, private route: Router) {}
  LogOut() {
    // this.auth.Logout();
    localStorage.removeItem('token');
    setTimeout(() => {
      alert('Logged Out Sucessfully');
    });
    this.route.navigate(['/Login']);
  }
}
