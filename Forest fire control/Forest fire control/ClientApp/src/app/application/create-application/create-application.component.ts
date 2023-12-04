import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Application } from 'src/app/models/application.model';
import { IncedentStatusEnum } from 'src/app/models/emums/incident-status.emum';
import { ObservationSite } from 'src/app/models/observation.model';
import { AuthService } from 'src/app/services/auth.service';
import { ObservationService } from 'src/app/services/observation.service';
import { UserService } from 'src/app/services/user.service';



@Component({
  selector: 'app-create-application',
  templateUrl: './create-application.component.html',
  styleUrls: ['./create-application.component.css']
})
export class CreateApplicationComponent implements OnInit {
  observation: ObservationSite;
  application: Application = {
    userEmail: '',
    observationSite: null,
    data: new Date(),
    description: '',
    status: IncedentStatusEnum.New
  };

  CreatedFailed: boolean = false;
  CreatedErrorMessage: string = '';
  CreatedSuccess: boolean = false;
  observations: ObservationSite[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private observationService: ObservationService,
    private usetService: UserService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (params['observation']) {
        this.observation = JSON.parse(params['observation']);
        this.application.observationSite = this.observation;   
      }
    });
    this.authService.email$.subscribe(getEmail => {
        this.application.userEmail = getEmail;
      });
      this.loadObservations()
  }

  loadObservations(): void {
    this.observationService.getObservations().subscribe(
      (data) => {
        this.observations = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  onSubmit(): void {
   this.application.data = new Date();
   this.usetService.addApplication(this.application).subscribe(
      (response) => {
        this.CreatedSuccess = true;
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 2000);
      },
      (error) => {
        this.CreatedFailed = true;
        this.CreatedErrorMessage = 'Произошла ошибка при создании заявки.';
      }
    );
  }
}