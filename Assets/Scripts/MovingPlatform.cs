using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;             
    public float moveDistance = 2f;      
    public bool moveUp = true;          

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;

        if (moveUp)
            targetPos = startPos + Vector3.up * moveDistance;
        else
            targetPos = startPos - Vector3.up * moveDistance;
    }

    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

       
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (moveUp)
                targetPos = startPos - Vector3.up * moveDistance;
            else
                targetPos = startPos + Vector3.up * moveDistance;

            moveUp = !moveUp;
        }
    }
}
