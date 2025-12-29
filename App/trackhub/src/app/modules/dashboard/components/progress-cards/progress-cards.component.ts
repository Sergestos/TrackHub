import { Component, effect, input } from "@angular/core";
import { ExerciseAggregation } from "../../models/exercise-aggregation.model";
import { LinearTrendComponent } from "./linear-trend/linear-trend.component";

@Component({
  selector: 'thr-progress-cards',
  templateUrl: './progress-cards.component.html',
  imports: [LinearTrendComponent],
  standalone: true,
})
export class ProgressCardsComponent {
  public currentMonth = input<ExerciseAggregation | null>();
  public previousMonth = input<ExerciseAggregation | null>();

  public isPlayedTotalMore?: boolean;
  public hasSongAggregation?: boolean;
  public hasRhythmAggregation?: boolean;
  public hasSoloAggregation?: boolean;

  constructor() {
    effect(() => {
      if (this.currentMonth() && this.previousMonth()) {
        this.buildTrands();
      }
    })
  }

  private buildTrands(): void {
    this.isPlayedTotalMore = this.currentMonth()!.total_played > this.previousMonth()!.total_played;
    this.hasSongAggregation = this.currentMonth()!.song_aggregation != null && this.previousMonth()!.song_aggregation != null;
    this.hasRhythmAggregation = this.currentMonth()!.rhythm_aggregation != null && this.previousMonth()!.rhythm_aggregation != null;
    this.hasSoloAggregation = this.currentMonth()!.solo_aggregation != null && this.previousMonth()!.solo_aggregation != null;
  }
}