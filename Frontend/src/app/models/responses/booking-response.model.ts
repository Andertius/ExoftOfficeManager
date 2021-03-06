import { WorkPlace } from "../work-place.model";
import { UserResponse } from "../../core/models/user-response.model";

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
        workPlace:   WorkPlace;
    }
}
