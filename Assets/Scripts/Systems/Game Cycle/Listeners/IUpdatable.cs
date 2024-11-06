namespace Systems.GameCycle
{
    public interface IUpdatable : IGameListener
    {
        void OnUpdate();
    }
}