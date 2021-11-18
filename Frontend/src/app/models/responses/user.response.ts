import { BookingResponse } from "./booking.response";

export interface UserResponse {
    id:        string;
    fullName:  string;
    avatarURL: string;
    role:      number;
    bookings:  BookingResponse;
}
