import { APP_INITIALIZER, ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { KeycloakService } from 'keycloak-angular';

export const appConfig: ApplicationConfig = {
	providers: [provideRouter(routes),
		// 	KeycloakService,
		// {
		// 	provide: APP_INITIALIZER,
		// 	useFactory: initializeKeycloak,
		// 	multi: true,
		// 	deps: [KeycloakService]
		// }
	],
};

function initializeKeycloak(keycloak: KeycloakService) {
	return () => keycloak.init({
		config: {
			url: 'http://localhost:8080',
			realm: 'your-realm',
			clientId: 'your-client-id'
		},
		initOptions: {
			onLoad: 'check-sso',
			silentCheckSsoRedirectUri:
				window.location.origin + '/assets/silent-check-sso.html'
		}
	});

}