using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float speed;
    [SerializeField] Bullet bullet;
    [SerializeField] Transform spawnPoint;

    float timer;
    float angle;
    bool isRotating;

    public enum State { Rotational, StaticEnemy }
    public State state;

    private void Update()
    {
        switch (state)
        {
            case State.Rotational:
                TurnEnemy();
                break;
            case State.StaticEnemy:
                StaticEnemy();
                break;
        }
    }

    void TurnEnemy()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            angle += 90;
            isRotating = true;
            Shoot();
            timer = 0;
        }

        if (isRotating)
        {
            StartCoroutine(StopRotation());

            Quaternion turn = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z), speed * Time.deltaTime);
            transform.rotation = turn;            
        }       
    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(0.5f);
        isRotating = false;
    }

    void StaticEnemy()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, spawnPoint.position, Quaternion.Euler(0, 90, 0));
    }
}
