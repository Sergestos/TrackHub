export class RecordModel {
    public id?: number;
    public name?: string | null;
    public author?: string | null;
    public recordType?: string;
    public duration?: number;
    public bpm?: number;
    public playType?: string;
    public isRecorded?: boolean;
}

export class ExerciseModel {
    public playDate?: Date;
    public records?: RecordModel[];
}