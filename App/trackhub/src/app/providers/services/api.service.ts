import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class ApiService {
  private httpClient = inject(HttpClient);

  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public get<T>(url: string): Observable<T> {
    return this.httpClient.get<T>(
      url,
      { headers: this.headers }
    );
  }

  public post<T>(url: string, body: any): Observable<T> {
    return this.httpClient.post<T>(
      url,
      body,
      { headers: this.headers }
    );
  }

  public put<T>(url: string, body: any): Observable<T> {
    return this.httpClient.put<T>(
      url,
      body,
      { headers: this.headers }
    );
  }

  public delete<T>(url: string): Observable<T> {
    return this.httpClient.delete<T>(
      url,
      { headers: this.headers }
    );
  }
}