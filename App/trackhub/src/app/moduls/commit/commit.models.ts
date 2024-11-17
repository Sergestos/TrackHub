export class RecordModel {
    public recordId?: string;
    public name?: string | null;
    public author?: string | null;
    public recordType?: string;
    public playDuration?: number;
    public bitsPerMinute?: number;
    public playType?: string;
    public isRecorded?: boolean;
}

export class ExerciseModel {
    public exerciseId?: number;
    public playDate?: Date;
    public records: RecordModel[] = [];
}

export class SuggestionResult {
    public result!: string;
    public source!: string;
}