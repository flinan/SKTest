import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { WebApiService } from './services/web-api-service.service';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Observable, catchError, map } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public users: User[] = [];
  public selectedUser: number | undefined;
  public showFormErrors: boolean = false;

  public userForm: FormGroup = new FormGroup({
    userID: new FormControl({ value: "", disabled: true }),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('')
  });

  constructor(private webApiService: WebApiService, private fb: FormBuilder) { }

  ngOnInit() {
    this.refreshUsersList(true);
  }

  get f(): { [key: string]: AbstractControl } {
    return this.userForm.controls;
  } 

  onSelectUserId(userId: number) {
    this.selectedUser = this.selectedUser === userId ? undefined : userId;
    let user = this.users.find(u => u.userId === userId);

    if (this.selectedUser === undefined || user === undefined) {
      this.clearForm();
      return;
    }

    this.userForm.setValue(user);
    this.userForm.markAsPristine();
  }

  clearForm() {
    this.showFormErrors = false;
    this.selectedUser = 0;

    this.userForm = this.fb.group({
      userId: ['',],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', {
        validators: [Validators.required, Validators.email],
        asyncValidators: [this.validateExistingEmail()],
        updateOn: 'blur'
      }]
    });
    return;
  }

  addUserFromForm() {
    this.showFormErrors = true;
    if (this.userForm.invalid) { return; }
    this.webApiService.addUser(this.userForm.value as User, () => this.refreshUsersList(true));
  }

  updateUserFromForm() {
    this.showFormErrors = true;
    if (this.userForm.invalid) { return; }
    this.webApiService.updateUser(this.userForm.value as User, () => { this.refreshUsersList(false), this.userForm.markAsPristine(); });
  }

  deleteSelectedUser() {
    if (this.selectedUser == undefined) { return; }
    this.webApiService.deleteUser(this.selectedUser, () => this.refreshUsersList(true));
  }

  refreshUsersList(clearForm: boolean) {
    if (clearForm) {
      this.clearForm();
    }
    this.webApiService.getUsers((result: User[]) => this.users = result, () => this.users = []);
  }

  validateExistingEmail(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return this.webApiService.canUseEmail(control.value, this.selectedUser ?? 0).pipe(
        map((exists) => (exists ? { emailExistsInDb: true } : null)),
        catchError(async (err) => null)
      );
    }
  }


  title = 'User management';
}
