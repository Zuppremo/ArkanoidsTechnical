public interface IGameControllerForState
{
    GameState GameState { get; }
    void SetPause();
}

