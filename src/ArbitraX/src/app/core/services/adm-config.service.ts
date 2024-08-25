import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Observable } from 'rxjs';
import { AdmConfig } from '../models/AdmConfig';

const url = 'http://localhost:5056/v1/admconfig/';

@Injectable({
  providedIn: 'root',
})
export class AdmConfigService {
  constructor(
    private http: HttpClient,
    private securityService: SecurityService
  ) {}

  headers: HttpHeaders;

  get(): Observable<AdmConfig> {
    this.headers = this.securityService.getAuthentication();

    return this.http.get<AdmConfig>(url, {
      headers: this.headers,
    });
  }
}
