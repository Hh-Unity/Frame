namespace HFrame2022
{
    public interface IModel : IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanSendEvent
    {
        void Init();
    }
}