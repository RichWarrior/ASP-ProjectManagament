namespace TaskScheduler
{
    public class Startup
    {
        public Startup()
        {
            PasswordNotification _password = new PasswordNotification();
            WorkDefNotification _workDef = new WorkDefNotification();
        }
        public static void OnStart()
        {
            PasswordNotification _password = new PasswordNotification();
            WorkDefNotification _workDef = new WorkDefNotification();
        }
    }
}
