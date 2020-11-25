﻿using Core.Abstractions.Repositories;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class TaskRepository : BaseRepository<Guid, Task_, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        { }



        ITaskRepository IBaseRepository<Guid, Task_, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Task_, ITaskRepository>.Reset()
        {
            return base.Reset();
        }


    }
}
