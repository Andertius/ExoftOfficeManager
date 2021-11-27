import { BookingType } from "../enums/booking-type.enum";

export interface AddBookingRequest {
    workPlaceId: string,
    userId:      string,
    bookingType: BookingType,
    bookingDate: string,
    days:        number | null,
}
