﻿using Domain.Commands;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ClientSideModels;
using Domain.DataModels;

namespace Core.Extensions.ModelConversion
{
    public static class ModelConversionExtensions
    {
        public static CreateMemberCommand ToCreateMemberCommand(this MemberVm model)
        {
            var command = new CreateMemberCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }
        public static CreateTaskCommand ToCreateTaskCommand(this TaskVm model)
        {
            var command = new CreateTaskCommand()
            {
                Id = model.Id,
                Subject = model.Subject,
                AssignedToId = model.AssignedToId,
                IsComplete = model.IsComplete
            };
            return command;
        }

        public static MenuItem[] ToMenuItems(this IEnumerable<MemberVm> models)
        {
            return models.Select(m => new MenuItem()
            {
                iconColor = m.Avatar,
                isActive = false,
                label = $"{m.LastName}, {m.FirstName}",
                referenceId = m.Id
            }).ToArray();
        }

        public static Task_[] ToTaskItems(this IEnumerable<TaskVm> models)
        {
            return models.Select(m => new Task_()
            {
                AssignedToId = m.AssignedToId,
                Id = m.Id,
                IsComplete = m.IsComplete,
                Subject = m.Subject
            }).ToArray();
        }

        public static UpdateMemberCommand ToUpdateMemberCommand(this MemberVm model)
        {
            var command = new UpdateMemberCommand()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }
        public static UpdateTaskCommand ToUpdateTaskCommand(this TaskVm model)
        {
            var command = new UpdateTaskCommand()
            {
                Id = model.Id,
                AssignedToId = model.AssignedToId,
                IsComplete = model.IsComplete,
                Subject = model.Subject
            };
            return command;
        }
    }
}
