import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { lineGraphData } from '../models/line-graph-data';
import { environment } from 'src/environments/environment';
import TransferRequest from '../models/TransferRequest';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private httpclient: HttpClient) { }
  GetLast12MonthBalances(userId: string): Observable<lineGraphData> {
    return this.httpclient.get<lineGraphData>(
      environment.apiUrlBase + 'Transaction/GetLast12MonthBalances/' + userId
    );
  }

  transfer(transferRequest: TransferRequest): Observable<any> {
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.httpclient.post(`${environment.apiUrlBase}Transaction/Transfer`, JSON.stringify(transferRequest), headers);
  }
}
