using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class DeleteTaskCommand
    {
        public Guid Id { get; set; }
    }
}
