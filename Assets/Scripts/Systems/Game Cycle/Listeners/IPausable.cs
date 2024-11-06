namespace Systems.GameCycle
{
    public interface IPausable : IGameListener
    {
        void OnPause();
    }
}