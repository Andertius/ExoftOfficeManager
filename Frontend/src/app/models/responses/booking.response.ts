import { UserResponse } from "./user.response";

export class BookingResponse {
    id:          string;
    date:        Date;
    type:        number;
    status:      number;
    dayNumber:   number;
    userID:      string;
    user:        UserResponse;
    workPlaceID: string;
    workPlace:   null; //temporary

    constructor(
        id:          string,
        date:        Date,
        type:        number,
        status:      number,
        dayNumber:   number,
        userID:      string,
        user:        UserResponse,
        workPlaceID: string,
        workPlace:   null) {
            
        this.id = id;
        this.date = date;   
        this.type = type;   
        this.status = status;
        this.dayNumber = dayNumber;
        this.userID = userID;
        this.user = user;
        this.workPlaceID = workPlaceID;
        this.workPlace = workPlace;
        
    }
}
