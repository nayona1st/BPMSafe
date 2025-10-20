using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioSource;
    PlayerController pc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        pc = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
        AudioSource.volume = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.pitch = pc.BPM / 65;
    }
}
