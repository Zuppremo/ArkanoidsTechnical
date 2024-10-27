public interface ISceneLoader
{
    string CurrentSceneName { get; }
    void ReloadCurrentScene();
    void LoadSceneByName(string sceneName);
}