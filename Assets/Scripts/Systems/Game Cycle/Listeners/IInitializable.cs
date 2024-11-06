namespace Systems.GameCycle
{
    public interface IInitializable : IGameListener
    {
        void OnInitialize();
    }
}