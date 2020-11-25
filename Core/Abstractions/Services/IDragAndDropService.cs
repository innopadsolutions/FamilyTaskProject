using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Abstractions.Services
{
    public interface IDragAndDropService
    {
        object Data { get; set; }
        string Zone { get; set; }

        void StartDrag(object data, string zone);
        bool Accepts(string zone);
        
    }
}
