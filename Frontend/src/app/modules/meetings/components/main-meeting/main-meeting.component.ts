import { Component, Input } from '@angular/core';
import { User } from 'src/app/core/models/user.model';

@Component({
    selector: 'app-main-meeting',
    templateUrl: './main-meeting.component.html',
    styleUrls: ['./main-meeting.component.scss']
})
export class MainMeetingComponent {
    @Input() user!: User;
}
