using UnityEngine;

public class Lava : MonoBehaviour
{
    public float startDelay = 15f;    // ��� ���� ���� �ð�(��)
    public float riseSpeed = 0.2f;   // ��� �ӵ� (m/s)
    public float maxHeight = 70f;    // ���� ����
    public bool stopAtMax = true;    // �ִ� ���̿��� ������ ����

    bool _isRising = false;

    void Start()
    {
        Invoke(nameof(StartRising), startDelay);
    }

    void StartRising()
    {
        _isRising = true;
    }

    void Update()
    {
        if (!_isRising) return;

        Vector3 pos = transform.position;
        pos.y += riseSpeed * Time.deltaTime;

        if (stopAtMax && pos.y >= maxHeight)
        {
            pos.y = maxHeight;
            transform.position = pos;
            _isRising = false;
            return;
        }

        transform.position = pos;
    }
}
