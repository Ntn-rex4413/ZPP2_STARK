using Hangfire;
using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.NotificationServices
{
    public interface INotificationService
    {
        void CreateConditionNotify(string userId, Condition condition);
    }
}
