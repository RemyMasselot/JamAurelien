using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public void GoToLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
