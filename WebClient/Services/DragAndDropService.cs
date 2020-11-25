using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Services
{
    public class DragAndDropService : IDragAndDropService
    {
        private ITaskDataService _taskService;
        public DragAndDropService(ITaskDataService taskService)
        {
            _taskService = taskService;
        }
        public object Data { get; set; }
        //public string Zone { get; set; }
        public Guid Zone { get; set; }

        //public void StartDrag(object data, string zone)
        public void StartDrag(object data, Guid zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        //public bool Accepts(string zone)
        public void Accepts(Guid zone, Guid data)
        {
            try
            {
                _taskService.AssignTask(data, zone);
                //return this.Zone == zone;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
