﻿namespace TrackHub.AiCrawler.PromtModels;

public class SongPromptArgs : GeneralPromptArgs
{    
    public IEnumerable<string>? AlbumsToInclude { get; set; }

    public IEnumerable<string>? AlbumsToExclude { get; set; }
}
