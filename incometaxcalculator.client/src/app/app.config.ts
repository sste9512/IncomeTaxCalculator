import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { TaxClient, API_BASE_URL } from './api-client';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(withFetch()),
    importProvidersFrom(HttpClientModule),

    {
      provide: API_BASE_URL,
      useFactory: () => {
        // In production or when hosted by ASP.NET Core, relative URLs work fine
        // During local development, you might need to specify the full URL
        const isLocalDev = window.location.port === '4200' || window.location.port === '57652'; // Angular dev server ports

        // Check if we're in development mode and return the appropriate base URL
        if (isLocalDev) {
          console.log('Running in development mode, using explicit API URL');
          return 'https://localhost:7216';
        }

        console.log('Running in production mode, using relative URLs');
        return '';
      }
    },
    TaxClient
  ]
};
