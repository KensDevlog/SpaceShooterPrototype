using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;

    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
