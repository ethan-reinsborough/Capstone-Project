import { throwError, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

//Import for Http access REST endpoints
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';

/**
 * Reference our environment
 */
export const API_URI = environment.apiUrl;

export abstract class SharedService {

  /**
   * Set up for our Http options for REST API Comms
   */
  protected httpOptions():object {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',//The content type we are sending
        'Accept': 'application/json'//We are expecting JSON from the Web API
      })
    };
    return httpOptions;
  }

  /**
   * Http error handler
   * @param err the HttpErrorResponse from the REST API
   */
  protected handleError(err: HttpErrorResponse): Observable<never> {  
    if(err.error instanceof ProgressEvent) return throwError(['Error connecting to REST API']);
    
    return throwError(err.error);
  };
}