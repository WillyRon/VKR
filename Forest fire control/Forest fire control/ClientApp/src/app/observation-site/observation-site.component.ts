import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ObservationSite } from '../models/observation.model';
import { ObservationService } from '../services/observation.service';
import { Incedent } from '../models/incident.model';
import { VideoArchive } from '../models/video-archiv.model';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { IncedentStatusEnum } from '../models/emums/incident-status.emum';

@Component({
  selector: 'app-observation-site',
  templateUrl: './observation-site.component.html',
  styleUrls: ['./observation-site.component.css'],
})
export class ObservationSiteComponent implements OnInit {
  observation: ObservationSite;
  isShowIncidents = false;
  isShowArchive = false;
  incidents: Incedent[] =[];
  videoArchivs: VideoArchive[] = [];
  videoUrl: SafeResourceUrl;



  constructor(
    private route: ActivatedRoute, 
    private observationService: ObservationService,
    private sanitizer: DomSanitizer,
    private router: Router, 
    private ngZone: NgZone
    ) {}

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
   this.videoUrl  = this.getSafeUrl(this.observation.url)
   this.getIncidents();
   this.getArchives();
  }
  getIncidents(){
    this.observationService.getIncidentObservations(this.observation.longitude, this.observation.latitude).subscribe(
      (data) => {
        this.incidents = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getArchives(){
    this.observationService.getVideoArchiveObservations(this.observation.longitude, this.observation.latitude).subscribe(
      (data) => {
        this.videoArchivs = data;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getSafeUrl(url: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  getStatusString(status: IncedentStatusEnum): string {
    return this.observationService.getStatusString(status);
  }


  viewArchive(): void {
    this.isShowArchive = true;
    this.isShowIncidents = false;
  }

  redirectCreateApplication(): void {
    this.ngZone.run(() => {
       this.router.navigate(['/create-application'], { queryParams: { observation: JSON.stringify(this.observation) } });
    });
  }
  viewIncident(): void {
    this.isShowArchive = false;
    this.isShowIncidents = true;
  }

  goToVideo(): void {
    // Реализуйте логику перехода на видео, используя incident
    console.log('Переход на видео для инцидента с датой:' );
  }

}
