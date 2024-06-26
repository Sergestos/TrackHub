export class ExerciseItem {
    public exerciseId!: string;
    public totalPlayed!: number; 
    public playDate!: Date;
}

export class ExerciseItemView extends ExerciseItem {
    public isExpanded?: boolean = false;
}

export class ExerciseDetails {
    public exerciseId!: string;
    public exerciseType!: string;
    public duration!: number;
    public itemName!: string;
    public authorName!: string;
    public isRecorded?: boolean;
    public bpm?: number;
}