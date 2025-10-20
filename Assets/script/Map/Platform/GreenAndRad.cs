using UnityEngine;

public class GreenAndRad : MonoBehaviour
{
    public Renderer platformRenderer; // 플랫폼의 색을 바꿀 Renderer
    Color redColor = Color.red;
    Color greenColor = Color.green;
    public float minSwitchTime = 3f; // 최소 3초
    public float maxSwitchTime = 8f; // 최대 8초

    public bool isGreen = false;
    float timer = 0f;
    float currentSwitchTime;

    void Start()
    {
        if (platformRenderer == null)
            platformRenderer = GetComponent<Renderer>();

        // 처음 시작할 때 랜덤 시간 설정
        currentSwitchTime = 6;
        SetColor();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSwitchTime)
        {
            timer = 0f;
            isGreen = !isGreen;
            SetColor();

            // 다음 전환까지의 시간도 랜덤으로 다시 설정
            currentSwitchTime = 6;
        }
    }



    void SetColor()
    {
        if (isGreen)
        {
            platformRenderer.material.color = greenColor;
        }
        else
        {
            platformRenderer.material.color = redColor;
        }
    }

    public bool CanMove()
    {
        return isGreen;
    }
}
