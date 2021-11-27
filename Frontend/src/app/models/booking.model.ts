import { BookingType } from "./enums/booking-type.enum";

export interface Booking {
    date:         string;
    userFullName: string;
    bookingType:  BookingType;
    tableNumber:  number;
    floorNumber:  number;
}
