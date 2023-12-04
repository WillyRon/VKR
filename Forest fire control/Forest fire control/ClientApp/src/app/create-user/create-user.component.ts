import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserRole, UserRoleDisplay } from '../models/emums/user-role.enum';
import { ObservationService } from '../services/observation.service';
import { Region } from '../models/regoin.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  user: User = {
    email: '',
    name: '',
    lastName: '',
    middleName: '',
    phoneNumber: '',
    region: '',
    role: UserRole.Dispatcher
  };

  regions: Region[] = [];
  filteredRegions: Region[] = [];

  CreatedFailed: boolean = false;
  CreatedSuccess: boolean = false;
  CreatedErrorMessage: string = '';

  public userTypeOptions: { id: number; label: string }[] = Object.entries(UserRoleDisplay)
    .map(([id, label]) => ({ id: Number(id), label }));

  constructor(
    private userService: UserService,
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
    this.user.role = Number(this.user.role);
    this.userService.addUser(this.user).subscribe(
      (response) => {
        this.CreatedSuccess = true;
        this.CreatedFailed = false;
        console.log('User added successfully', response);
      },
      (error) => {
        console.error('Error adding user', error);
        this.CreatedFailed = true;
        this.CreatedSuccess = false;
        this.CreatedErrorMessage = error.error.errorMessage;
      }
    );
  }
}