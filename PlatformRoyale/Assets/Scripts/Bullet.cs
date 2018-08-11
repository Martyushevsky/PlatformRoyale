using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 420f;
    public int predictionStepsPerFrame = 6;
    public Vector2 bulletVelocity;
    public float yGravity = -50;
    private Vector2 gravity;


    void OnValidate()
    {
        gravity = new Vector2(0, yGravity);
    }

    private void Start()
    {
        bulletVelocity = transform.right * speed;
    }

    void Update()
    {
        Vector2 point1 = transform.position;
        float stepSize = 1f / predictionStepsPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            bulletVelocity += gravity * stepSize * Time.deltaTime;
            Vector2 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(point1, point2 - point1, (point2 - point1).magnitude);
            if (hit)
            {
                Debug.Log("Hit " + hit.collider.name);
            }

            point1 = point2;
        }

        transform.position = point1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 point1 = transform.position;
        Vector2 predictedBulletVelocity = bulletVelocity;
        float stepSize = 0.01f;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedBulletVelocity += gravity * stepSize;
            Vector2 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
