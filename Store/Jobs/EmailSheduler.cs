using Quartz;
using Quartz.Impl;

namespace WebApplicationL5.Jobs;

public class EmailScheduler
{
    public static async void Start()
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();

        IJobDetail job = JobBuilder.Create<EmailSender>().Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(c => c
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}