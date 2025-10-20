using Unity.VisualScripting;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject Camera;
    public Camera mainCamera;

    public GameObject Player;
    PlayerController playerController;

    public Collider Hospital;

    private void Start()
    {
        playerController = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Camera.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Player.transform.localRotation = Quaternion.identity;
            Camera.SetActive(true);
            playerController.Ending = true;
            playerController.die = true;
            Hospital.isTrigger = true;
        }
    }
}
