import { BookingResponse } from "./bookingResponse.model";

export interface UserResponse {
    id:        string;
    fullName:  string;
    avatarURL: string;
    role:      number;
    bookings:  BookingResponse;
}
