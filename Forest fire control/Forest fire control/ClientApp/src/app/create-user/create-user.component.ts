import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import { UserRole, UserRoleDisplay } from '../models/user-role.enum';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
  
})
export class CreateUserComponent {
  user: User = {
    email: '',
    name: '',
    lastName: '',
    middleName: '',
    phoneNumber: '',
    region: '',
    role: UserRole.Dispatcher
  };
  CreatedFailed: boolean = false;
  CreatedSuccess: boolean = false;
  CreatedErrorMessage: string = '';
  public userTypeOptions: { id: number, label: string }[] = Object.entries(UserRoleDisplay)
  .map(([id, label]) => ({ id: Number(id), label }));

  constructor(private authService: AuthService) {}

  onSubmit() {
    this.user.role = Number(this.user.role);
    this.authService.addUser(this.user).subscribe(
      response => {
        this.CreatedSuccess = true;
        this.CreatedFailed = false;
        console.log('User added successfully', response);
      },
      error => {
        console.error('Error adding user', error);
        this.CreatedFailed = true;
        this.CreatedSuccess = false;
        this.CreatedErrorMessage = error.error.errorMessage;
      }
    );
  }
}