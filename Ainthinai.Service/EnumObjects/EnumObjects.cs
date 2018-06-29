using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.EnumObjects
{
    public class EnumObjects
    {
    }

    public enum EventType
    {
        All,
        Plantation,
        Maintenance,
        Nursery,
        GYOS,
        TG,
        BTR,
        WasteManagement
    }

    public enum TaskStatus
    {
        New,
        Active,
        InProgress,
        Completed,
        PartiallyCompleted,
        Closed
    }
}
