import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ObservationSite } from 'src/app/models/observation.model';
import { AuthService } from 'src/app/services/auth.service';
import { ObservationService } from 'src/app/services/observation.service';

@Component({
  selector: 'app-observation-table',
  templateUrl: './observation-table.component.html',
  styleUrls: ['./observation-table.component.css']
})
export class ObservationTableComponent implements OnInit {
  observations: ObservationSite[] = [];
    isAdmin = false;
    isDeleteModalVisible = false;
    observationToDelete: ObservationSite | null = null;

  constructor(
    private observationService: ObservationService,
    private authService: AuthService,
    private ngZone: NgZone,
    private router: Router,
    ) {}

  ngOnInit(): void {
    this.loadObservations();
    this.authService.isAdmin$.subscribe(isAdmin => {
        this.isAdmin = isAdmin;
      });
  }

  loadObservations(): void {
    this.observationService.getObservations().subscribe(
      (data) => {
        this.observations = data;
        this.observations.sort((a, b) => a.isActiveIncident === b.isActiveIncident ? 0 : a.isActiveIncident ? -1 : 1);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  goToDetails(observation: ObservationSite): void {
    this.ngZone.run(() => {
       this.router.navigate(['/observation-site'], { queryParams: { observation: JSON.stringify(observation) } });
    });
  }

  editObservation(observation: ObservationSite): void {
    this.ngZone.run(() => {
       this.router.navigate(['/edit-observation'], { queryParams: { observation: JSON.stringify(observation) } });
    });
  }

  deleteObservation(observation: ObservationSite): void {
    this.observationService.deleteObservation(observation)
      .subscribe(
        (response) => {
          console.log('Observation deleted successfully:', response);
          // Тут можно обновить список наблюдений, если нужно
          this.loadObservations();
        },
        (error) => {
          // Обработка ошибки
          console.error('Error deleting observation:', error);
        }
      );
  }
  deleteObservationModal(observation: ObservationSite): void {
    this.observationToDelete = observation;
  }

}