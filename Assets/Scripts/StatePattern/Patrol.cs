using UnityEngine;

public class Patrol : State
{
    Rigidbody rig;
    Player player;

    public Patrol(Animator anim, GameObject nbc) : base(anim, nbc)
    {
        rig = nbc.GetComponent<Rigidbody>();
        player = nbc.GetComponent<Player>();
        lvl = PHASE.PATROL;
    }

    public override void Enter()
    {
        anim.SetBool("Run", true);
        base.Enter();
    }

    public override void Update()
    {
        float speed = 1.5f;


        Director d = player.Director;

        if (d != null)
        {
            player.transform.rotation = Quaternion.LookRotation(player.Direction);
        }
        else
        {
            if (player.RotationDirection != Vector3.zero)
            {
                player.transform.rotation = Quaternion.LookRotation(player.RotationDirection);                

                if (player.positioning)
                {
                    GameObject go = player.Tile();

                    if (go != null)
                    {
                        player.transform.position = new Vector3(go.transform.position.x, player.transform.position.y, go.transform.position.z);

                        player.positioning = false;

                    }

                }
            }
            else
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation
                 (player.Direction), speed * Time.deltaTime);
            }
        }

        rig.MovePosition(rig.position + player.Direction * speed * Time.deltaTime * anim.deltaPosition.magnitude);
    }

    public override void Exite()
    {
        anim.SetBool("Run", false);
        base.Exite();
    }
}
