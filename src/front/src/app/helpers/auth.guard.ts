import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from'rxjs';
import { map, take} from'rxjs/operators';

import { TokenStorageService } from '../services/token-storage.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private tokenStorageService: TokenStorageService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        const currentUser = this.tokenStorageService.getUser();

        return this.tokenStorageService.isLoggedIn
        .pipe (
            take (1),
            map((isLoggedIn: boolean) => {
            if (!isLoggedIn) {
                this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
                return false;
            } else 

                return true;
            })
        )
    }
}