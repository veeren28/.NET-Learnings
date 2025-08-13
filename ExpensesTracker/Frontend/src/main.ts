import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { RouterModule } from '@angular/router';
import { routes } from './app/app.routes';

import 'zone.js';
import { authGuard } from './app/auth-guard';

bootstrapApplication(App, {
  providers: [
    provideHttpClient(),
    importProvidersFrom(RouterModule.forRoot(routes)),
  ],
}).catch((err) => console.error(err));
bootstrapApplication(App, appConfig).catch((err) => console.error(err));
