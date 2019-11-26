using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private enum Scenes
    {
        START,
        GAME
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.LoadScene((int)Scenes.GAME);
    }
}
