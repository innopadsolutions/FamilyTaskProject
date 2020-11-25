﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataModels
{
    public class Task_
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? AssignedToId { get; set; }
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
    }
}
