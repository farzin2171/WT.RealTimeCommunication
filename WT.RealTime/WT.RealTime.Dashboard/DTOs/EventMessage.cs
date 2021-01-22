﻿using System;

namespace WT.RealTime.Dashboard.DTOs
{
    public class EventMessage
    {
        public string Id { get; }
        public string Title { get; }
        public DateTime CreatedDateTime { get; }

        public EventMessage(string id, string title, DateTime createdDateTime)
        {
            Id = id;
            Title = title;
            CreatedDateTime = createdDateTime;
        }
    }
}
