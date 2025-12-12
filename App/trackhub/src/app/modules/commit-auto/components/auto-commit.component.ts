import { Component, OnInit, inject, signal } from "@angular/core";
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
  public text: string = '';

  public isValid: boolean = false;
  public playDate?: Date;

  public previewRecords?: PreviewRecord[];
  public validationIssues = signal<ValidationIssue[]>([]);

  private input$ = new Subject<string>();

  private autoCommitService = inject(AutoCommitService);

  public get isSaveAllowed(): boolean {
    return this.isValid &&
      this.previewRecords !== undefined &&
      this.previewRecords.length > 0;
  }

  public ngOnInit(): void {
    this.text = this.getInitialDateTemplate();

    this.input$
      .pipe(
        debounceTime(DEBOUNCE_TIME),
        distinctUntilChanged())
      .subscribe(value => {
        this.autoCommitService
          .previewExerice(value)
          .subscribe({
            next: (response: PreviewState) => {
              this.playDate = response.playDate;
              this.previewRecords = response.records;

              this.validationIssues.set(response.validationIssues!);
              this.isValid = this.validationIssues().length > 0;
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
        records: this.previewRecords!.map(record => ({
          ...record,
          recordId: undefined
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

  private getInitialDateTemplate(): string {
    return `--${new Intl.DateTimeFormat('uk-UA').format(new Date())}--`;
  }
}  