using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.Services
{
    public class NotificationService
    {

        public NotificationService()
        {
            SetupMorningNotification();
            SetupEveningNotification();
        }

        private void SetupMorningNotification()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 1000,
                Title = "Check your chores!",
                Description = "Open Homie to see what needs doing today",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 0, 0)
                }
            };
            LocalNotificationCenter.Current.Show(notification);
        }

        private void SetupEveningNotification()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 1000,
                Title = "Have you logged your chores?",
                Description = "Don't miss out on your points!",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0)
                }
            };
            LocalNotificationCenter.Current.Show(notification);
        }
    }
}
