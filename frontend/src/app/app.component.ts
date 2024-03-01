import { Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { LayoutComponent } from './common/layout/layout.component';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [
		// RouterOutlet,
		// BrowserModule,
		CommonModule,
		// BrowserAnimationsModule,
		LayoutComponent
	],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	title = 'str-dss-ui';
}
