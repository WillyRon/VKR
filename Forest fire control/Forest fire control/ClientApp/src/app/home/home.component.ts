import { Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { ObservationSite } from '../models/observation.model';
import { ObservationService } from '../services/observation.service';
import { Router } from '@angular/router';


declare const ymaps: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  selectedObservation: ObservationSite | null;
  @ViewChild('map', { static: true }) mapRef: ElementRef;
  observations: ObservationSite[] = [];
  yandexMap: any;

  constructor(private observationService: ObservationService, private router: Router, private ngZone: NgZone) {}

  ngOnInit(): void {
    this.loadObservations();
  }

  loadObservations(): void {
    this.observationService.getObservations().subscribe(
      (data) => {
        this.observations = data;
        this.initYandexMap();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  initYandexMap(): void {
    ymaps.ready(() => {
      this.yandexMap = new ymaps.Map(this.mapRef.nativeElement, {
        center: [61.317870, 100.780049],
        zoom: 4,
        controls: ['zoomControl'],
      });

      this.observations.forEach((observation) => {
        const marker = new ymaps.Placemark(
          [observation.latitude, observation.longitude],
          {
            hintContent: observation.name,
          },
          {
            iconLayout: 'default#image',
            iconImageHref: '../../assets/Logo/ObseOk.png',
            iconImageSize: [20, 20],
            iconImageOffset: [-10, -10],
          }
        );

        marker.events.add('click', (event: any) => {
          const target = event.get('target');
          const coords = target.geometry.getCoordinates();
          this.redirectToObservationSite(observation);
        });

        this.yandexMap.geoObjects.add(marker);
      });
    });
  }
  
  redirectToObservationSite(observation: ObservationSite): void {
    this.ngZone.run(() => {
       this.router.navigate(['/observation-site'], { queryParams: { observation: JSON.stringify(observation) } });
    });
  }
}
