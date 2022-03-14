namespace PajdisekGitMirrorer.API;
public static class timeChecker
{
    public static void checkTime(int timeinminutes)
    {
        while(true)
        {
            APIMethods.Run.Get();
            System.Threading.Thread.Sleep(timeinminutes*1000*60);
        }
    }
}