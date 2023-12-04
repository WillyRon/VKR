import { Component, OnInit } from '@angular/core';
import { ObservationService } from '../services/observation.service';
import { Region } from '../models/regoin.model';
import { ObservationSite } from '../models/observation.model';

@Component({
  selector: 'app-create-observation',
  templateUrl: './create-observation.component.html',
  styleUrls: ['./create-observation.component.css']
})
export class CreateObservationComponent implements OnInit {
  observation: ObservationSite = {
    name: '',
    longitude: null,
    latitude: null,
    address: '',
    region: '',
    url: '',
    isActiveIncident: false
  };

  regions: Region[] = [];
  filteredRegions: Region[] = [];

  CreatedFailed: boolean = false;
  CreatedSuccess: boolean = false;
  CreatedErrorMessage: string = '';


  constructor(
    private observationService: ObservationService
  ) {}

  ngOnInit(): void {
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
    this.observationService.addObservation(this.observation).subscribe(
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