﻿namespace TrackHub.Service.Services.ExerciseServices.Models;

public record UpdateExerciseModel
{
    public required string ExerciseId { get; set; }

    public required IEnumerable<UpdateRecordModel> Records { get; set; }
}
