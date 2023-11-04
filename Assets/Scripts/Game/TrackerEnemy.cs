using System.Collections;
using UnityEngine;

public class TrackerEnemy : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    [SerializeField] float speed = 15;
    [SerializeField] Transform eye;
    [SerializeField] float eyeSpeed = 10;

    int index;
    bool permitedToMove;
    int angle;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (index == targets.Length) return;
        if (!permitedToMove) return;

        if (transform.position != targets[index].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed * Time.deltaTime);
        }
        else
        {
            index++;
        }

       // RotateEye();
    }

    void RotateEye()
    {
        angle = (angle == 45) ? -45 : 45;
        
        Quaternion euler = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, euler, eyeSpeed * Time.deltaTime);
    }

    IEnumerator StartTimer()
    {
        permitedToMove = false;

        yield return new WaitForSeconds(3);

        permitedToMove = true;
    }
}
