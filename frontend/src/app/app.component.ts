import { Component } from '@angular/core';

import { LayoutComponent } from './common/layout/layout.component';
import { CommonModule } from '@angular/common';
import { KeycloakAngularModule } from 'keycloak-angular';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [
		CommonModule,
		KeycloakAngularModule,
		LayoutComponent
	],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	title = 'str-dss-ui';
}
