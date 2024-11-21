using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public bool moveOnX = true; // True for X-axis movement, False for Y-axis
    public int direction = 1; // 1 for positive direction, -1 for negative direction

    void Update()
    {
        Vector3 movement = moveOnX 
            ? new Vector3(direction * speed * Time.deltaTime, 0, 0) 
            : new Vector3(0, direction * speed * Time.deltaTime, 0);

        transform.position += movement;

        AdjustScale();
    }

    void AdjustScale()
    {
        if (moveOnX)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * direction;
            transform.localScale = localScale;
        }
    }
}
