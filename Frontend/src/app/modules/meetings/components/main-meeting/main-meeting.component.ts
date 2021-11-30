import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/core/models/user.model';

@Component({
    selector: 'app-main-meeting',
    templateUrl: './main-meeting.component.html',
    styleUrls: ['./main-meeting.component.scss']
})
export class MainMeetingComponent implements OnInit {
    public userFullName!: string;
    public id!: string;

    public userFirstName!: string;

    constructor(private activatedRoute: ActivatedRoute) {
        this.activatedRoute.queryParamMap.subscribe(params => {
            this.userFullName = String(params.get('userFullName'));
            this.id = String(params.get('id'));
        });
    }

    public ngOnInit(): void {
        this.userFirstName = this.userFullName.split(' ')[0];
    }
}
