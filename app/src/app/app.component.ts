import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Toast } from 'primeng/toast';
import { CoreModule } from './core/core.module';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Toast, CoreModule],
  providers: [],
  template: `
    <router-outlet/>
    <p-toast />
  `,
})
export class AppComponent {
  title = 'app';
}
