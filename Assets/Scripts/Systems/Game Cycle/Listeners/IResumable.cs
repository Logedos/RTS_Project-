namespace Systems.GameCycle
{
    public interface IResumable : IGameListener
    {
        void OnResume();
    }
}