import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConnectionService } from '../Services/connection.service';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  constructor(private interactionService: ConnectionService,private route: ActivatedRoute) {}
  sub: any;
  

  daysPenalty:number[];
  ngOnInit() {
    this.sub = this.route.params.subscribe(param1 => {
      this.sub=param1;
    });

  

 
}
}
