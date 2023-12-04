import { Component, NgZone, OnInit } from "@angular/core";
import { Application } from "../models/application.model";
import { UserService } from "../services/user.service";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "../models/user.model";
import { ObservationSite } from "../models/observation.model";



@Component({
    selector: 'app-application',
    templateUrl: './application.component.html',
    styleUrls: ['./application.component.css']
  })
  export class ApplicationComponent implements OnInit {
    application: Application;
    user: User;


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private ngZone: NgZone,
      ) {}

      ngOnInit(): void {
        this.route.queryParams.subscribe((params) => {
          const application = JSON.parse(params['application']) as Application;
          this.application = application;
    
          this.userService.getUserInfo(this.application.userEmail).subscribe(
            (user) => {
              this.user = user;         
            },
            (error) => {
              console.error(error);
            }
          );
        });
      }

      redirectToObservationSite(): void {
        this.ngZone.run(() => {
           this.router.navigate(['/observation-site'], { queryParams: { observation: JSON.stringify(this.application.observationSite) } });
        });
      }
    }