import { ExerciseRecord } from "./exercise-record";

export class Exercise {
  public exerciseId?: number;
  public playDate?: Date;
  public records: ExerciseRecord[] = [];
}