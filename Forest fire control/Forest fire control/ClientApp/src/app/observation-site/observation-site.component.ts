import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ObservationSite } from '../models/observation.model';

@Component({
  selector: 'app-observation-site',
  templateUrl: './observation-site.component.html',
  styleUrls: ['./observation-site.component.css'],
})
export class ObservationSiteComponent implements OnInit {
  observation: ObservationSite;
  isShowIncidents = false;
  isShowArchive = false;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if ('observation' in params) {
        try {
          this.observation = JSON.parse(params['observation']);
        } catch (error) {
          console.error('Error parsing observation parameter:', error);
        }
      } else {
        console.warn('Observation parameter is missing.');
      }
    });
    
  }

  viewArchive(): void {
    this.isShowArchive = true;
    this.isShowIncidents = false;
  }

  viewIncident(): void {
    this.isShowArchive = false;
    this.isShowIncidents = true;
  }
}
