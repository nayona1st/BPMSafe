using UnityEngine;

public class PlayerPovController : MonoBehaviour
{
    PlayerController playerController;

    public GameObject Capsule;
    public GameObject Target;
    public GameObject CameraArm;
    public Camera TPSCamera;

    public float ArmLength = 35f;
    public float SpeedRot = 360f;

    public float _xRot;
    public float _yRot;

    private Vector3 _originPos;
    private Quaternion _originRot;

    private void Init()
    {
        CameraArm.transform.localPosition = _originPos;
        CameraArm.transform.localRotation = _originRot;
        _xRot = 0f;
        _yRot = 0f;
    }

    private void CameraUpdate()
    {
        Ray ray = new Ray(CameraArm.transform.position, -CameraArm.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ArmLength))
        {
            TPSCamera.transform.position = hit.point;
        }
        else
        {
            TPSCamera.transform.position = CameraArm.transform.position + (-CameraArm.transform.forward * ArmLength);
        }
    }

    private void Rotate()
    {
        //회전

        var xMove = Input.GetAxis("Mouse X");
        var yMove = -Input.GetAxis("Mouse Y");

        _xRot += xMove * SpeedRot * Time.deltaTime;
        _yRot += yMove * SpeedRot * Time.deltaTime;

        _yRot = Mathf.Clamp(_yRot, -45f, 30f);

        Target.transform.localRotation = Quaternion.Euler(0, _xRot, 0f);
        Capsule.transform.localRotation = Quaternion.Euler(_yRot, 0, 0f);


    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
        _originPos = CameraArm.transform.localPosition;
        _originRot = CameraArm.transform.localRotation;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerController.die && !playerController.Ending)
        {
            Rotate();
            CameraUpdate();
        }
    }
}
