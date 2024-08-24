export class ExerciseItem {
    public exerciseId!: string;
    public records: RecordDetailsItem[] | null = null; 
    public playDate!: Date;
}

export class ExerciseItemView extends ExerciseItem {
    public isExpanded?: boolean = false;
    public isHidden?: boolean = false;
    public IsPlayedDay?: boolean = true;
    public totalPlayed?: number = 0;
}

export class RecordDetailsItem {    
    public recordType!: string;
    public duration!: number;
    public name!: string;
    public author!: string;    
}

export class UserExerciseProfile {
    public firstExerciseDate!: Date; 
}

export class FiltersModel {
    public year!: number;
    public month!: number;
    public showNonPlayed?: boolean;
    public showExpanded?: boolean;
}