import { Component, OnInit, inject, signal } from "@angular/core";
import { ExerciseAggregation } from "../../models/exercise-aggregation.model";
import { LinearTrendComponent } from "./linear-trend/linear-trend.component";
import { AggregationService } from "../../services/aggregation.service";
import { combineLatest } from "rxjs";

@Component({
  selector: 'thr-progress-cards',
  templateUrl: './progress-cards.component.html',
  imports: [LinearTrendComponent],
  standalone: true,
})
export class ProgressCardsComponent implements OnInit {
  public currentMonth = signal<ExerciseAggregation | null>(null);
  public previousMonth = signal<ExerciseAggregation | null>(null);

  public isPlayedTotalMore?: boolean;
  public hasSongAggregation?: boolean;
  public hasRhythmAggregation?: boolean;
  public hasSoloAggregation?: boolean;

  private aggregationService = inject(AggregationService);

  public ngOnInit(): void {
    const currentMonth = new Date();
    const previousMonth = new Date(currentMonth.getFullYear(), currentMonth.getMonth() - 1, 1);

    const currentMonthRequest$ = this.aggregationService.getMonthAggregation(currentMonth);
    const previousMonthRequest$ = this.aggregationService.getMonthAggregation(previousMonth);

    combineLatest([currentMonthRequest$, previousMonthRequest$])
      .subscribe(([current, previous]) => {
        this.currentMonth.set(current);
        this.previousMonth.set(previous);

        if (this.currentMonth() && this.previousMonth()) {
          this.buildTrends();
        }
      });
  }

  private buildTrends(): void {
    this.isPlayedTotalMore = 
      this.currentMonth()!.totalPlayed > this.previousMonth()!.totalPlayed;

    this.hasSongAggregation = 
      this.currentMonth()!.songAggregation != null && this.previousMonth()!.songAggregation != null;

    this.hasRhythmAggregation = 
      this.currentMonth()!.rhythmAggregation != null && this.previousMonth()!.rhythmAggregation != null;
      
    this.hasSoloAggregation = 
      this.currentMonth()!.soloAggregation != null && this.previousMonth()!.soloAggregation != null;
  }
}