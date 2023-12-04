import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Application } from 'src/app/models/application.model';
import { IncedentStatusEnum } from 'src/app/models/emums/incident-status.emum';
import { ObservationService } from 'src/app/services/observation.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-application-table',
  templateUrl: './application-table.component.html',
  styleUrls: ['./application-table.component.css'] // Добавьте свои стили здесь
})
export class ApplicationTableComponent implements OnInit {
  applications: Application[] = [];

  constructor(private userService: UserService,
    private observationService: ObservationService,
     private router: Router) {}

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    this.userService.getApplications().subscribe(
      (data) => {
        this.applications = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getStatusString(status: IncedentStatusEnum): string {
    return this.observationService.getStatusString(status);
  }

  goToApplicationDetails(application: Application): void {
    this.router.navigate(['/application'], {
      queryParams: { application: JSON.stringify(application) },
    });
  }
}