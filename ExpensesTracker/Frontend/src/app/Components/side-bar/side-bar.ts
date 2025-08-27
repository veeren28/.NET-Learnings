import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routes } from '../../app.routes';
import { Router, RouterEvent, RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { FormsModule } from '@angular/forms';
import { Token } from '@angular/compiler';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css',
})
export class SideBar implements OnInit {
  userName: string | null = null;
  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      const decode: any = jwtDecode(token);
      console.log(decode);
      this.userName =
        decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ??
        null;
      console.log('Sidebar');
      console.log(this.userName);
    }
  }

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
