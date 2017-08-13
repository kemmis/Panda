
import { Component } from "@angular/core";
import { AccountService } from "../../../services/account.service";
import { PasswordChangeRequest, PasswordChangeResult } from "../../../models/password-change-request";
import { MdDialogRef, MdSnackBar } from "@angular/material";

@Component({
    selector: 'change-password',
    templateUrl: './password.component.html',
    styleUrls: ['./password.component.less']
})
export class PasswordComponent {
    constructor(private _accountService: AccountService,
        private _dialog: MdDialogRef<PasswordComponent>,
        private _snackBar: MdSnackBar) { }

    changeRequest: PasswordChangeRequest = new PasswordChangeRequest();
    save() {
        this._accountService.changePassword(this.changeRequest).subscribe((result: PasswordChangeResult) => {
            if (result.succeeded) {
                this._dialog.close();
                this._snackBar.open("Password Changed.", "", { duration: 2000 });
            } else {
                this._snackBar.open(result.errors[0], "", { duration: 2000 });
            }
        });
    }
}