import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetAccountByXResponse } from '../models/AccountByX';


@Injectable({
  providedIn: 'root',
})

export default class AccountsService {
  constructor(private httpClient: HttpClient) { }

  getAccountByUser(userId: string): Observable<GetAccountByXResponse> {
    return this.httpClient.get<GetAccountByXResponse>(`${environment.apiUrlBase}Accounts/GetAccountByUser/${userId}`);
  }

  getAccountByAccountNumber(accountNumber: string): Observable<GetAccountByXResponse> {
    return this.httpClient.get<GetAccountByXResponse>(`${environment.apiUrlBase}Accounts/GetAccountByAccountNumber/${accountNumber}`);
  }
}