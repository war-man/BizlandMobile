﻿using Shiny.Locations;

using System;

namespace Bizland.Core.Delegates
{
    public class GpsReadingEventArgs : EventArgs
    {
        public IGpsReading Reading { get; }

        public GpsReadingEventArgs(IGpsReading reading)
        {
            Reading = reading;
        }
    }

    public interface IGpsListener
    {
        event EventHandler<GpsReadingEventArgs> OnReadingReceived;
    }
}