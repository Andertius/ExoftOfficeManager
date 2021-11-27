import { BookingResponse } from "./booking-response.model";

export interface UserResponse {
    id:        string;
    fullName:  string;
    avatarURL: string;
    role:      number;
    bookings:  BookingResponse;
}
