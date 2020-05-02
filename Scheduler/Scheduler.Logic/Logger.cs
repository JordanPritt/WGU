using System;
using System.IO;

namespace Scheduler.Logic
{
    public static class Logger
    {
        private static string LogFile = "userActivityLog.txt";

        public static void LogUserActivity(string username)
        {
            try
            {
                string ActivityTimeStamp = $"username: {username} -- signed in on: {DateTime.UtcNow} -- UTC time";

                using (var file = File.AppendText(LogFile))
                {
                    file.WriteLine(ActivityTimeStamp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Coulnd't log user activity because: {ex}");
            }
        }
    }
}
