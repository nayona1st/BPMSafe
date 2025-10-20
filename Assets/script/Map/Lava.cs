using UnityEngine;

public class Lava : MonoBehaviour
{
    public float startDelay = 15f;    // 상승 시작 지연 시간(초)
    public float riseSpeed = 0.2f;   // 상승 속도 (m/s)
    public float maxHeight = 70f;    // 멈출 높이
    public bool stopAtMax = true;    // 최대 높이에서 멈출지 여부

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
