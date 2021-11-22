import { WorkPlaceModel } from "../work-place.model";
import { UserResponse } from "./userResponse.model";

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
        workPlace:   WorkPlaceModel;
    }
}
