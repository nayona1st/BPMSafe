using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void GoingPlayScenes()
    {
        SceneManager.LoadScene("PlayScenes");
    }
}
