namespace AnalyticsTools
{
    public interface IAnalyticsTools
    {
        void SendMessage(string nameEvent);
        void SendMessage(string nameEvent, (string key, object value) valueTuple);
    }
}
