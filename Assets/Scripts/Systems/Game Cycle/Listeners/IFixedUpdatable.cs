namespace Systems.GameCycle
{
    public interface IFixedUpdatable : IGameListener
    {
        void OnFixedUpdate();
    }
}