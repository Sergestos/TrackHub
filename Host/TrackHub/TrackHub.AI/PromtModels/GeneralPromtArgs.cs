﻿namespace TrackHub.AI.PromtModels;

public class GeneralPromptArgs
{
    public int ExpectedLength { get; set; } = 5;

    public string? SearchPattern { get; set; }
}
