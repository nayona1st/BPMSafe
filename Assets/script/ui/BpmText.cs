using System.Text;
using TMPro;
using UnityEngine;

public class BpmText : MonoBehaviour
{
    PlayerController player;

    string text;
    StringBuilder textHeart;

    string DeadText;

    public TextMeshProUGUI BpmUiText;

    public TextMeshProUGUI DeadTimeText;

    private void Start()
    {
        player = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
        textHeart = new StringBuilder();

        textHeart.Append("½É¹Ú¼ö : ");
        DeadTimeText.color = Color.red;
        DeadTimeText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (player.BPM < 50 || player.BPM > 130)
        {
            BpmUiText.color = Color.red;

            if (!player.Immortality)
            {
                DeadTimeText.gameObject.SetActive(true);
                DeadText = player.deathTimer.ToString("F2");
                DeadTimeText.text = DeadText;
            }
        }
        else
        {
            DeadTimeText.gameObject.SetActive(false);
            BpmUiText.color = Color.white;
        }
        text = textHeart + player.BPM.ToString("F0");
        BpmUiText.text = text;
    }
}
