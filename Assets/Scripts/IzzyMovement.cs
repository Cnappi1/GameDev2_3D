using UnityEngine;

public class IzzyMove : MonoBehaviour
{
    public float speed = 3f;
    public Transform[] waypoints;

    int currentIndex = 0;


    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                currentIndex = waypoints.Length - 1;
            }
        }
    }
}
