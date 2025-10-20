using UnityEngine;

public class NextStage : MonoBehaviour
{
    public GameObject platform; 
    public float moveDistance = 3f;
    public float moveSpeed = 2f; 

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = false;
    private bool isActivated = false;

    void Start()
    {
        startPos = platform.transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }
    void Update()
    {
        // �̵� ���̸� �� ������ ���� �̵�
        if (isMoving)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPos, moveSpeed * Time.deltaTime);

            // ��ǥ ��ġ�� ���� �����ϸ� �̵� ����
            if (Vector3.Distance(platform.transform.position, targetPos) < 0.01f)
            {
                platform.transform.position = targetPos;
                isMoving = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            isMoving = true;
        }
    }
}
