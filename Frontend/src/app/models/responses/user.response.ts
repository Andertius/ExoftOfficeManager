import { BookingResponse } from "./booking.response";

export class UserResponse {
    id:        string;
    fullName:  string;
    avatarURL: string;
    role:      number;
    bookings:  BookingResponse;
    
    constructor(
        id:        string,
        fullName:  string,
        avatarURL: string,
        role:      number,
        bookings:  BookingResponse) {

        this.id = id;
        this.fullName = fullName;
        this.avatarURL = avatarURL;
        this.role = role;
        this.bookings = bookings;
    }
}
