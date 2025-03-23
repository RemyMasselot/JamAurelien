using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private SetInput setInput;
    [SerializeField] private string sceneName;
    [SerializeField] private bool InMainMenu;


    private void Update()
    {
        if (InMainMenu)
        {
            if (setInput._Start.WasPerformedThisFrame())
            {
                GoToLoadingScene();
            }
        }
        else
        {
            if (setInput.Back.WasPerformedThisFrame())
            {
                GoToLoadingScene();
            }
        }
    }

    public void GoToLoadingScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
