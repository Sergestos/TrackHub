import { ChangeDetectorRef, Component, OnInit, inject } from "@angular/core";
import { AutoCommitService } from "../services/auto-commit.service";
import { Subject, debounceTime, distinctUntilChanged } from "rxjs";
import { PreviewRecord, PreviewState, ValidationIssue } from "../models/preview-state.model";
import { Exercise } from "../../../models/exercise";
import { ExerciseRecord } from "../../../models/exercise-record";

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

  public get isSaveAllowed(): boolean {
    return this.isValid &&
      this.previewRecords !== undefined &&
      this.previewRecords.length > 0;
  }

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
              this.playDate = response.playDate;
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

  public onAddClick($event: any): void {
    if (this.isSaveAllowed) {
      const exercise: Exercise = {
        exerciseId: undefined,
        playDate: this.playDate,
        records: this.previewRecords!.map((x: PreviewRecord) => ({
          recordId: undefined,
          name: x.name,
          author: x.author,
          recordType: x.recordType,
          playDuration: x.playDuration,
          bitsPerMinute: x.bitsPerMinute,
          playType: x.playType,
          isRecorded: x.isRecorded
        }) as ExerciseRecord)
      };

      this.autoCommitService
        .saveExercise(exercise)
        .subscribe({
          next: (_) => {
            window.location.reload()
          }
        })
    }
  }
}  