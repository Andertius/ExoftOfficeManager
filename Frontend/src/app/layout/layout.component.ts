import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ProfileService } from '../core/services/profile.service';
import { ProfileComponent } from '../shared/components/profile/profile.component';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, OnDestroy {

  @Input() userFullName!: string;
  
  private unsubscribe$: Subject<void> = new Subject();

  links: Array<string> = [
    'book-place',
    'work-place',
  ];

  activeLink = "";

  constructor(public dialog: MatDialog, private readonly profileService: ProfileService) { }

  ngOnInit(): void {
    this.profileService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: any) => {
        this.userFullName = res.fullName;
      });

      this.userFullName = 'Alissa White-Gluz';
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(ProfileComponent, {
      width: '500px',
      data: {
        firstName: "Alissa",
        lastName: "White-Gluz",
        email: "alissa@archenemy.info",
        status: "Status",
        avatar: ""
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}
