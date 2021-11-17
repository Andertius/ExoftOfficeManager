export class BookingModel {
    date: Date;
    userFullName: string;
    bookingType: number;

    constructor(date: Date, userFullName: string, bookingType: number)
    {
        this.date = date;
        this.userFullName = userFullName;
        this.bookingType = bookingType;
    }
}
