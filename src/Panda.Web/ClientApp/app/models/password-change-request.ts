export class PasswordChangeRequest {
    currentPassword: string;
    newPassword: string;
    newPasswordConfirm: string;
}

export class PasswordChangeResult {
    succeeded: boolean;
    errors: string[];
}