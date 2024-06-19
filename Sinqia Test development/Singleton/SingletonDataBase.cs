namespace Sinqia_Test_development.Singleton
{
    public class SingletonDataBase
    {
        private static SingletonDataBase _instance;
        private static readonly object _lock = new object();

        private SingletonDataBase()
        {

        }

        public static SingletonDataBase Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonDataBase();
                    }
                    return _instance;
                }
            }
        }

        public string Data { get; set; }

    }
}
