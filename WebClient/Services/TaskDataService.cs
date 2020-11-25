using Domain.Commands;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;
using Core.Extensions.ModelConversion;
using Domain.ViewModel;
using Domain.Queries;
using System.Text.Json;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            ListTasks = new List<TaskModel>();
            //Tasks = new List<TaskModel>();
            //tasks = new List<TaskVm>();
            LoadTasks();
        }
        private IEnumerable<TaskVm> tasks;
        public IEnumerable<TaskVm> Tasks => tasks;
        //public IEnumerable<TaskVm> Tasks => tasks;

        public List<TaskModel> ListTasks { get; private set; }
        public TaskModel SelectedTask { get; private set; }

        public event EventHandler TasksChanged;
        public event EventHandler TasksUpdated;
        public event EventHandler<string> UpdateTaskFailed;

        public async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = ListTasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public async void ToggleTask(Guid id)
        {
            foreach (var taskModel in ListTasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsDone = !taskModel.IsDone;

                    TaskVm model = new TaskVm();
                    model.AssignedToId = taskModel.Member;
                    model.Id = taskModel.Id;
                    model.IsComplete = taskModel.IsDone;
                    model.Subject = taskModel.Text;

                    await UpdateTask(model);
                }
            }
            TasksUpdated?.Invoke(this, null);
        }

        public async void AssignTask(Guid taskId, Guid memberId)
        {
            foreach (var taskModel in ListTasks)
            {
                if (taskModel.Id == taskId)
                {
                    taskModel.Member = memberId;
                    TaskVm model = new TaskVm();
                    model.AssignedToId = memberId;
                    model.Id = taskModel.Id;
                    model.IsComplete = taskModel.IsDone;
                    model.Subject = taskModel.Text;
                    await UpdateTask(model);
                }
            }
            TasksUpdated?.Invoke(this, null);
        }

        public async Task OnItemDelete(Guid id)
        {
            await Delete(id);
            var updatedList = (await GetAllTasks()).Payload;
            if (updatedList != null)
            {
                tasks = updatedList;
                TasksChanged?.Invoke(this, null);
                return;
            }
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        private async Task<DeleteTaskCommandResult> Delete(Guid id)
        {
            return await httpClient.GetJsonAsync<DeleteTaskCommandResult>($"tasks/{id}");
        }

        public async Task UpdateTask(TaskVm model)
        {
            var result = await Update(model.ToUpdateTaskCommand());

            Console.WriteLine(JsonSerializer.Serialize(result));

            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksChanged?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The save was successful.");
            }
            UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
        }


        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }
        public async void LoadTasks()
        {
            tasks = (await GetAllTasks()).Payload;
            //ListTasks = (List<TaskModel>)(await GetAllTasks()).Payload;
            foreach (var itm in tasks)
            {
                var data = new TaskModel();
                data.Id = itm.Id;
                data.IsDone = itm.IsComplete;
                data.Member = itm.AssignedToId;
                data.Text = itm.Subject;
                ListTasks.Add(data);
            }
            TasksChanged?.Invoke(this, null);
            return;
        }
        public async Task AddTask(TaskModel model)
        {
            ListTasks.Add(model);
            TasksUpdated?.Invoke(this, null);

            var newModel = new TaskVm();
            newModel.Id = model.Id;
            newModel.AssignedToId = model.Member;
            newModel.IsComplete = model.IsDone;
            newModel.Subject = model.Text;

            var result = await Create(newModel.ToCreateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksChanged?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateTaskFailed?.Invoke(this, "Unable to create record.");

        }
    }
}