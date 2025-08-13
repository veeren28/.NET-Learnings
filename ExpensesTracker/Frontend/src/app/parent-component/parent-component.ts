import { Component } from '@angular/core';
import { SideBar } from '../Components/side-bar/side-bar';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-parent-component',
  imports: [SideBar, RouterOutlet, RouterModule],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.css',
})
export class ParentComponent {}
