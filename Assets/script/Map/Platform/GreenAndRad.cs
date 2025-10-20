using UnityEngine;

public class GreenAndRad : MonoBehaviour
{
    public Renderer platformRenderer; // �÷����� ���� �ٲ� Renderer
    Color redColor = Color.red;
    Color greenColor = Color.green;
    public float minSwitchTime = 3f; // �ּ� 3��
    public float maxSwitchTime = 8f; // �ִ� 8��

    public bool isGreen = false;
    float timer = 0f;
    float currentSwitchTime;

    void Start()
    {
        if (platformRenderer == null)
            platformRenderer = GetComponent<Renderer>();

        // ó�� ������ �� ���� �ð� ����
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

            // ���� ��ȯ������ �ð��� �������� �ٽ� ����
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
