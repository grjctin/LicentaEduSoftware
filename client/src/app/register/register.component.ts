import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent{
  //@Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  //model: any = {} era pentru template forms
  registerForm: FormGroup = new FormGroup({}); //pentru reactive forms
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;


  constructor(private accountService: AccountService,
     private toastr: ToastrService, 
     private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, 
        Validators.minLength(4),Validators.maxLength(12)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  //validator pentru a compara parolele introduse in form
  matchValues(matchTo: string): ValidatorFn {
    return(control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

  register() {
    // const dob = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    const values = {...this.registerForm.value}
    this.accountService.register(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/classes')
      },
      error: error => {
        this.validationErrors = error
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  //metoda pentru a returna doar data, nu si timpul si timezoneul
  // private getDateOnly(dob: string | undefined) {
  //   if(!dob) return;
  //   let theDob = new Date(dob);
  //   return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset()))
  //   .toISOString().slice(0,10);
  // }

}
