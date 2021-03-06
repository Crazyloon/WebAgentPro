import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MessageService } from './message.service';
import { Quote } from '../../data/models/domain/quote';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CalculationEngineService {
  private calcUrl: string;

  constructor(private http: HttpClient, private messageService: MessageService) {
    this.calcUrl = "api/quotes/calculate"
  }

  private log(message: string) {
    this.messageService.add(`QuoteService: ${message}`);
  }

  calculateQuote(quote: Quote): Observable<number> {
    const params = new HttpParams().append('quoteId', quote.id.toString());

    return this.http.get<number>(this.calcUrl, {params: params}).pipe(
      tap(result => this.log(`Calc Engine: Got final price: ${result}!`)),
      catchError(this.handleError<number>('calculateQuote'))
    );
  }

/**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
