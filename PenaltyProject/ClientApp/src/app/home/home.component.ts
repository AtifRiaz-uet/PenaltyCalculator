import { Component, EventEmitter, Output } from '@angular/core';
import { ConnectionService } from '../Services/connection.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { datePenlaty } from '../Model/dataPenalty';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    countriesList: string[];
    PenaltyForm: FormGroup;
    daysPenlaty:string[];
    cIN:Date;
    countryName:string;
    @Output() newdayPen = new EventEmitter<number[]>();
    trigger:Boolean=false

    cOU:Date;
    constructor(private interactionService: ConnectionService,private router: Router) { }

    ngOnInit(): void {
        this.interactionService.getCountriesName()
            .pipe(map((data: string[]) => {
                this.countriesList = data;
            })
            )
            .subscribe();
        this.PenaltyForm = new FormGroup({
            country: new FormControl('', Validators.required),//new formControl of country
            checkIn: new FormControl('', Validators.required),//formControl of checkin date
            checkOut: new FormControl('', Validators.required)//formControl of checkout date

        });
    }

    onSubmit()
    {
        console.log("Button Pressed!");
        if (this.PenaltyForm.valid) {
            this.cIN=this.PenaltyForm.value.checkIn; //storing input value of checkIn
            this.cOU=this.PenaltyForm.value.checkOut;//storing input value of checkOut
            this.countryName=this.PenaltyForm.value.country;//storing input value of Country Selected
            if (this.cOU >= this.cIN)
            {
                this.interactionService.postDates(this.cIN,this.cOU,this.countryName)
                .pipe(map((dayP: string[]) => {
                    this.interactionService.sendreq(this.daysPenlaty = dayP);//sending daysandPenalty to service to get access on any other components
                })
                )
                .subscribe();//posting checkin , checkoutout dates and country to api and in return getting working days and penalty price
                this.trigger=true;
                console.log("Form Submitted");

            }
            else
            {
                alert('There is Some Error')
            }
            
            console.log(this.PenaltyForm);

        }
        else {
            console.log(this.PenaltyForm);
            alert('There is Something Missing!');//throw alert if user try to calculate without adding any input

        }
        
    }
}