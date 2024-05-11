using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [HorizontalLine(color: EColor.Green)]
    [BoxGroup("Load Settings"), SerializeField, Scene] private string _sceneToLoad;
    [BoxGroup("Load Settings"), SerializeField] private bool _loadAsAdditive;

    [HorizontalLine(color: EColor.Red)]
    [BoxGroup("Unload Settings"), SerializeField] private bool _unloadCurrentScene = true;
    [BoxGroup("Unload Settings"), HideIf("_unloadCurrentScene"), SerializeField, Scene]
    private string _sceneToUnload;

    private LoadSceneMode _loadSceneMode;

    private void Awake()
    {
        _loadSceneMode = _loadAsAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
    }

    public void Load()
    {
        SceneManager.LoadScene(_sceneToLoad, _loadSceneMode);
    }

    public void Unload()
    {
        string scene = _unloadCurrentScene ? gameObject.scene.name : _sceneToUnload;
        SceneManager.UnloadSceneAsync(scene);
    }
}