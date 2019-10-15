using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(ScenesStruct.MainMenu);
    }

    public void AttractorExample()
    {
        SceneManager.LoadScene(ScenesStruct.AttractorExample);
    }

    public void OrbiterExample()
    {
        SceneManager.LoadScene(ScenesStruct.OrbiterExample);
    }
}
