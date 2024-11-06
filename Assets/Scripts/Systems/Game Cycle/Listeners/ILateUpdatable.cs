namespace Systems.GameCycle
{
    public interface ILateUpdatable : IGameListener
    {
        void OnLateUpdate();
    }
}