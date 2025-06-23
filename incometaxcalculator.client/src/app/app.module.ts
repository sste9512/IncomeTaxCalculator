import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { API_BASE_URL, TaxClient } from './api-client';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [
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
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
