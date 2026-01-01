import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { ApiService } from "../../../providers/services/api.service";
import { Observable } from "rxjs";
import { SuggestionResult } from "../models/suggestion-result.model";
import { HttpParams } from "@angular/common/http";

@Injectable()
export class SuggestionService {
  readonly suggestionUrl: string = environment.apiUrl + '/api/suggestions';

  private apiService = inject(ApiService);

  public getSongSuggestrions(pattern: string, author?: string | null): Observable<SuggestionResult[]> {
    const params = new HttpParams()
      .set('pattern', pattern);

    if (author) {
      params.set('author', author);
    }

    return this.apiService.get<SuggestionResult[]>(
        `${this.suggestionUrl}/songs`, 
        params
    );
  }

  public getAuthorSuggestrions(pattern: string): Observable<SuggestionResult[]> {
    const params = new HttpParams()
      .set('pattern', pattern)

    return this.apiService.get<SuggestionResult[]>(
        `${this.suggestionUrl}/authors`, 
        params
    );
  }
}