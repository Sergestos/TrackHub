import { Component, OnInit, inject } from "@angular/core";
import { AutoCommitService } from "../services/auto-commit.service";
import { Subject, debounceTime, distinctUntilChanged } from "rxjs";

const DEBOUNCE_TIME = 1000;

@Component({
  selector: 'trh-commit',
  templateUrl: './auto-commit.component.html',
  styleUrls: ['./auto-commit.component.scss'],
  standalone: false
})
export class AutoCommitComponent implements OnInit {
  public isValid: boolean = false;
  public playDate?: Date;

  private input$ = new Subject<string>();
  public text: string = '';

  private autoCommitService = inject(AutoCommitService);

  public ngOnInit(): void {
    this.input$
      .pipe(
        debounceTime(DEBOUNCE_TIME),
        distinctUntilChanged())
      .subscribe(value => {
        this.autoCommitService
          .previewExerice(value)
          .subscribe({
            next: (response: boolean) => {
              this.isValid = response;
            }
          })
      });
  }

  public onInput($event: Event): void {
    const text = ($event.target as HTMLTextAreaElement).value;
    this.input$.next(text);
  }

  public onAddClick(): void {
    this.autoCommitService
      .saveExercise(this.text)
      .subscribe({
        next: (response) => {

        }
      })
  }

  public onDeleteClick(): void {

  }
}  