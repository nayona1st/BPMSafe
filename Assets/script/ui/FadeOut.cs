using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    PlayerController player;
    public Image FadeOutImage;

    public GameObject EndFadeOutText;

    Color FadeOutColor;
    bool FadeOutStop = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
        FadeOutImage = GetComponent<Image>();
        EndFadeOutText.SetActive(false);
    }
    void Update()
    {
        FadeOutColor = FadeOutImage.color;
        if (player.die)
        {
            if (FadeOutColor.a < 0.95 && !FadeOutStop)
                FadeOutColor.a += Time.deltaTime;
            else if (FadeOutColor.a >= 0.95)
                FadeOutStop = true;
            if (Input.GetKeyDown(KeyCode.Space) && FadeOutStop)
            {
                FadeOutColor.a -= 0.95f / 14;
            }
        }
        if(player.Ending)
        {
            if (FadeOutColor.a < 1)
                FadeOutColor.a += Time.deltaTime * 0.15f;
            else if(FadeOutColor.a >= 1)
                EndFadeOutText.SetActive(true);
        }
        FadeOutImage.color = FadeOutColor;
    }
}
