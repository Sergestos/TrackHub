import { ChangeDetectorRef, Component, OnInit, inject } from "@angular/core";
import { AutoCommitService } from "../services/auto-commit.service";
import { Subject, debounceTime, distinctUntilChanged } from "rxjs";
import { PreviewRecord, PreviewState, ValidationIssue } from "../models/preview-state.model";

const DEBOUNCE_TIME = 1000;

@Component({
  selector: 'trh-auto-commit',
  templateUrl: './auto-commit.component.html',
  standalone: false
})
export class AutoCommitComponent implements OnInit {
  public isValid: boolean = false;
  public playDate?: Date;

  public previewRecords?: PreviewRecord[];
  public validationIssues?: ValidationIssue[];

  private input$ = new Subject<string>();
  public text: string = '';

  private autoCommitService = inject(AutoCommitService);
  private changeDetector = inject(ChangeDetectorRef);

  public ngOnInit(): void {
    this.input$
      .pipe(
        debounceTime(DEBOUNCE_TIME),
        distinctUntilChanged())
      .subscribe(value => {
        this.autoCommitService
          .previewExerice(value)
          .subscribe({
            next: (response: PreviewState) => {
              this.isValid = response.isValid;
              this.previewRecords = response.records;
              this.validationIssues = response.validationIssues;
              
              this.changeDetector.detectChanges();
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