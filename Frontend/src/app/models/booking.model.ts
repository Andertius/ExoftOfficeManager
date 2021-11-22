import { BookingType } from "./enums/booking-type.enum";

export interface BookingModel {
    date: string;
    userFullName: string;
    bookingType: BookingType;
    tableNumber: number;
    floorNumber: number;
}
