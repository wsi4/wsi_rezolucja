﻿namespace Resolution.Visitors.ClauseMaker
{
    public interface IClauseMakerFSM
    {
        ClauseMakerState State { get; set; }
    }
}