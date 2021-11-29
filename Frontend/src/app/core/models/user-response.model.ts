import { BookingResponse } from "../../models/responses/booking-response.model";

export interface UserResponse {
    id:        string;
    fullName:  string;
    avatar:    string;
    role:      number;
    bookings:  BookingResponse;
}
