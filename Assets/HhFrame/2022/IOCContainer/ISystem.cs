namespace HFrame2022
{
    public interface ISystem : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanGetUtility,ICanSendEvent,ICanRegisterEvent,ICanGetSystem
    {
        void Init();
    }
}