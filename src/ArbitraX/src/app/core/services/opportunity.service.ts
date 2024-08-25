import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Observable } from 'rxjs';
import { Opportunity } from '../models/Opportunity';
import {
  GetAllResponse,
  OpportunityViewModel,
} from '../ViewModels/OpportunityViewModel';

const url = 'http://localhost:5056/v1/opportunity/';

@Injectable({
  providedIn: 'root',
})
export class OpportunityService {
  constructor(
    private http: HttpClient,
    private securityService: SecurityService
  ) {}

  headers: HttpHeaders;

  getAll(): Observable<GetAllResponse> {
    this.headers = this.securityService.getAuthentication();

    return this.http.get<GetAllResponse>(url, {
      headers: this.headers,
    });
  }
}
