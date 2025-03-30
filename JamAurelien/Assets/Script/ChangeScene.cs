using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.ProBuilder;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private SetInput setInput;
    [SerializeField] private string sceneName;
    [SerializeField] private bool InMainMenu;
    [SerializeField] private RawImage fade;


    private void Update()
    {
        if (InMainMenu)
        {
            if (setInput._Start.WasPerformedThisFrame())
            {
                fade.DOFade(1, 0.5f).OnComplete(()=>
                {
                    GoToLoadingScene();
                });
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
