using Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DragAndDropService : IDragAndDropService
    {
        public object Data { get; set; }
        public string Zone { get; set; }

        public void StartDrag(object data, string zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}
