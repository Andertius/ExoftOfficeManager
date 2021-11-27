import { Booking } from "../booking.model";

export interface WorkPlaceResponse {
    workPlace: {
        id:          string,
        floorNumber: number,
        placeNumber: number,
        bookings:    Booking[],
    }
}
