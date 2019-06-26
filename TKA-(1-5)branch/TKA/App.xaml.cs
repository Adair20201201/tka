using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TKA.Business;

namespace TKA
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        ISchedulerFactory sf = new StdSchedulerFactory();
        IScheduler sched = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           
            sched = sf.GetScheduler();
            //job详情
            IJobDetail myJob1 = JobBuilder.Create<TimeDeleteJob>().WithIdentity("job1", "group1").Build();

            #region SimpleTriggerImpl
            //SimpleTriggerImpl trigger = new SimpleTriggerImpl("simpleTrig", "group1", 10, DateTime.Now.AddSeconds(2) - DateTime.Now); 
            #endregion
            #region CronTrigger（0/5 * * * * ?：每隔5秒）
            //59 59 23 L * ?每个月最后一天23:59:59
            IOperableTrigger trigger1 = new CronTriggerImpl("trigName1", "group1", "59 59 23 L * ?");
            #endregion

            //关联job和触发器
            sched.ScheduleJob(myJob1, trigger1);
            //执行
            sched.Start();
        }
    }
}
