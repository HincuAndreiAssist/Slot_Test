using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slot_Test.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double Win { get; set; }
        public double Value { get; set; }
    }
}
