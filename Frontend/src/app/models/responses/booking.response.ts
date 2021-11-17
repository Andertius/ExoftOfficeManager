import { UserResponse } from "./user.response";

export interface BookingResponse {
    booking: {
        id:          string;
        date:        Date;
        type:        number;
        status:      number;
        dayNumber:   number;
        userID:      string;
        user:        UserResponse;
        workPlaceID: string;
        workPlace:   null; //temporary
    }
}
