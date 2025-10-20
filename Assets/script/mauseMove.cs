using UnityEngine;

public class mauseMove : MonoBehaviour
{
    Vector3 clickPos;
    Vector3 c;

    Vector3 from;
    Vector3 to;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            c = Input.mousePosition;
            Debug.Log(c);
        }

        from = transform.position;
        to = clickPos;

        transform.position = to;
    }
}
