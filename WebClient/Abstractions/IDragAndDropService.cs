using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Abstractions
{
    public interface IDragAndDropService
    {
        object Data { get; set; }
        //string Zone { get; set; }
        Guid Zone { get; set; }

        //void StartDrag(object data, string zone);
        void StartDrag(object data, Guid zone);
        //bool Accepts(string zone);
        void Accepts(Guid zone, Guid data);
    }
}
