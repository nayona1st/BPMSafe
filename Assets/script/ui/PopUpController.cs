using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour
{
    public GameObject EscPopUp;
    void Start()
    {
        EscPopUp.SetActive(false);
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        EscPopUpButton();
    }

    void EscPopUpButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscPopUp.activeSelf)
            {
                Cursor.visible = false;
                EscPopUp.SetActive(false);
                Time.timeScale = 1;
            }
            else if (!EscPopUp.activeSelf)
            {
                EscPopUp.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
    }

    public void EscButtonStart()
    {
        Cursor.visible = false;
        EscPopUp.SetActive(false);
        Time.timeScale = 1;
    }
    public void EscButtonExit()
    {
        Application.Quit();
    }
    public void EscbuttonMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScenes");
    }

}
