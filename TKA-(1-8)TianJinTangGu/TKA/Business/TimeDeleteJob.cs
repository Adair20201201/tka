using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TKA.Business
{
    public class TimeDeleteJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Access access = new Access();
            access.DeleteBeforeToday();
        }
    }
}
