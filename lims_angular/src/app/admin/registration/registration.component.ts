import { Component, OnInit, Output, EventEmitter } from "@angular/core";

import { AuthService } from "src/app/services/auth.service";

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styleUrls: ["./registration.component.css"]
})
export class RegistrationComponent implements OnInit {
  @Output() registeringUser = new EventEmitter<boolean>();
  waitingForResponse: boolean;
  errorMessage: string;

  constructor(private auth: AuthService) {}

  ngOnInit() {
    this.waitingForResponse = false;
    this.errorMessage = "";
  }

  registerUser(
    username: HTMLInputElement,
    password: HTMLInputElement,
    firstname?: HTMLInputElement,
    lastname?: HTMLInputElement,
    email?: HTMLInputElement
  ) {
    if (username.value.length < 1 || password.value.length < 1) {
      alert("Username and Password are required");
      return;
    }
    this.waitingForResponse = true;
    this.errorMessage = "";
    this.auth
      .registerNewUser(
        username.value,
        password.value,
        "unknown",
        "unknown",
        "unknown"
      )
      .subscribe(response => {
        this.handleRegisterResponse(response);
      });
  }

  handleRegisterResponse(response): void {
    // this endpoint returns null on success
    this.waitingForResponse = false;
    if (response) {
      if (response.error) {
        this.errorMessage = "Failed to register user!";
      } else {
        console.log("this is the registration response: " + response);
        this.cancel();
      }
    } else {
      this.errorMessage = "Failed to register user!";
      this.cancel();
    }
  }

  cancel(): void {
    this.registeringUser.emit(false);
  }
}
