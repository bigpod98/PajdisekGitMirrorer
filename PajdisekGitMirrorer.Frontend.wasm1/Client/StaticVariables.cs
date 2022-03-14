namespace PajdisekGitMirrorer.Frontend.wasm1.Client
{
    public static class StaticVariables
    {
        public static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://192.168.1.65")
        };
    }
}
