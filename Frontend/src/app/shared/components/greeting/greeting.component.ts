import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ProfileService } from 'src/app/core/services/profile.service';
import { EditProfileResultModel } from 'src/app/models/edit-profile-result.model';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss'],
  host: { class: 'app-greeting' }
})
export class GreetingComponent implements OnInit {

  @Input() firstName!: string;
  greetingMessage: string = "";
  
  private unsubscribe$: Subject<void> = new Subject();

  constructor(private readonly profileService: ProfileService) { }

  ngOnInit(): void {
    this.firstName = this.firstName.toUpperCase();

    this.profileService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: EditProfileResultModel) => this.firstName = res.fullName.split(' ')[0]);
  }

}
