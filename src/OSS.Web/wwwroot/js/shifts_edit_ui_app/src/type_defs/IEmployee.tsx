export interface IEmployee {
    userId: string;
    username: string;
    displayName: string;
    email: string;
    userRole: string;
    officeId: string;
    gender: string;
    dob?: Date;
    isActive: boolean;
    shiftRole: string;
    shiftGroup: string;
    phoneNumber: string;
}