import { Component } from '@angular/core';
import { ConnectionService } from '../Services/connection.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    countriesList: string[];
    PenaltyForm: FormGroup;
    constructor(private interactionService: ConnectionService) { }

    ngOnInit(): void {
        this.interactionService.getCountriesName()
            .pipe(map((data: string[]) => {
                this.countriesList = data;
            })
            )
            .subscribe();
        this.PenaltyForm = new FormGroup({
            country: new FormControl('', Validators.required),
            checkIn: new FormControl(Date, Validators.required),
            return: new FormControl(Date, Validators.required)

        });
    }

    onSubmit()
    {
        console.log("Button Pressed!");
        if (this.PenaltyForm.valid) {
            console.log("Form Submitted");
            console.log(this.PenaltyForm);

        }
        else {
            console.log(this.PenaltyForm);
            alert('There is Something Wrong!');

        }
        
    }
}
