import { Component, OnInit } from '@angular/core';
import { ObservationService } from '../../services/observation.service';
import { Region } from '../../models/regoin.model';
import { ObservationSite } from '../../models/observation.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-observation',
  templateUrl: './edit-observation.component.html',
  styleUrls: ['./edit-observation.component.css']
})
export class EditObservationComponent implements OnInit {
  observation: ObservationSite;

  regions: Region[] = [];
  filteredRegions: Region[] = [];

  CreatedFailed: boolean = false;
  CreatedSuccess: boolean = false;
  CreatedErrorMessage: string = '';


  constructor(
    private observationService: ObservationService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const observation = JSON.parse(params['observation']) as ObservationSite;
      this.observation = observation;
    });
    this.loadRegions();
  }

  loadRegions(): void {
    this.observationService.getRegions().subscribe(
      (data) => {
        this.regions = data;
        this.filteredRegions = this.regions.slice(); 
      },
      (error) => {
        console.error('Error fetching regions', error);
      }
    );
  }

  filterRegions(value: string): void {
    this.filteredRegions = this.regions.filter(region =>
      region.name.toLowerCase().includes(value.toLowerCase())
    );
  }

  onSubmit(): void {
    this.observationService.updateObservation(this.observation).subscribe(
      (response) => {
        this.CreatedSuccess = true;
        this.CreatedFailed = false;
        console.log(response);
      },
      (error) => {
        console.error(error);
        this.CreatedFailed = true;
        this.CreatedSuccess = false;
        this.CreatedErrorMessage = error.error.errorMessage;
      }
    );
  }
}