import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CoreModule } from './core/core.module';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CoreModule],
  providers: [],
  template: `
    <router-outlet/>
  `,
})
export class AppComponent {
  title = 'app';
}
