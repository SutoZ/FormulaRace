import { enableProdMode } from '@angular/core';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { platformBrowser } from '@angular/platform-browser';

if (environment.production) {
  enableProdMode();
}

const providers = [{ provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }];

export function getBaseUrl() {
  const base = document.getElementsByTagName('base')[0];
  return base ? base.href : '/';
}

platformBrowser(providers)
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
