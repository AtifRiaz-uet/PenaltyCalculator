import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';
import { dateModel } from '../Model/dates';
import { datePenlaty } from '../Model/dataPenalty';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  private _callforBtn = new Subject<any>()
  gm$ = this._callforBtn.asObservable()
  
  
  sendreq(dPlist:string[]){
    this._callforBtn.next(dPlist);
  }

    private _url: string = "https://localhost:44380/api/PenaltyCalculator/GetCountriesData";//API getCountriesData access url
    private _urlPost: string = "https://localhost:44380/api/PenaltyCalculator/Post";//API post CountriesData access url
    newDates:dateModel;


    constructor(private http: HttpClient) { }

    getCountriesName(): Observable<String[]> {

        return this.http.get<String[]>(this._url)

    }

    
    
    postDates(checkIn:Date,returnDate:Date,countryName:string):Observable<string[]>

    {
      this.newDates={// creating new object of dateModel to send data
        checkIn:checkIn,
        checkOut:returnDate,
        countryName:countryName
      }
      return this.http.post<string[]>(this._urlPost,this.newDates);

    }

    
}