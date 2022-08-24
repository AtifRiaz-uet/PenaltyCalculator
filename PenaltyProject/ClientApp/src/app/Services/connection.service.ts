import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

    private _url: string = "https://localhost:44380/api/PenaltyCalculator/GetCountriesData";


    constructor(private http: HttpClient) { }

    getCountriesName(): Observable<string[]> {

        return this.http.get<string[]>(this._url)

    }
}
