using UnityEngine;

public class Player : MonoBehaviour
{
    Director director;
    State patrol;
    Animator anim;
    Vector3 rotationDirection = Vector3.zero;

    [HideInInspector] public bool positioning;

    public Director Director { get { return director; } }
    public Vector3 RotationDirection { get { return rotationDirection; } }

    public Vector3 Direction
    {
        get
        {
            if (director != null)
                return director.transform.forward;
            else
                return transform.forward;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.applyRootMotion = true;        
        patrol = new Patrol(anim, this.gameObject);
    }

    private void Update()
    {
        patrol.Process();
    }

    public void SetPointer(Director director)
    {
        this.director = director;
    }

    Vector3 CheckObstackle()
    {
        Vector3[] directions = new Vector3[2];
        Vector3 pickedDirection, lastPickedDirection;
        Ray[] rays = new Ray[2];
        RaycastHit hit;

        float range = 1f;

        rays[0] = new Ray(transform.position,transform.right);
        rays[1] = new Ray(transform.position, -transform.right);
      
        for (int i = 0; i < rays.Length; i++)
        {
            if (!Physics.Raycast(rays[i], out hit, range))
            {
                directions[i] = rays[i].direction;
            }
            else
            {
                directions[i] = Vector3.zero;
            }
        }

        pickedDirection = (directions[0] == transform.right) ? directions[0] : directions[1];
        lastPickedDirection = (pickedDirection != Vector3.zero) ? pickedDirection : -transform.forward;

        return lastPickedDirection;
    }

    public GameObject Tile()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.gameObject.tag == "Ground")
                return hit.collider.gameObject;
        }

        return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            director = null;
            positioning = true;
            rotationDirection = CheckObstackle();
            GameObject tile = Tile();
            transform.position = new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickuple p = other.gameObject.GetComponent<IPickuple>();

        if (p != null)
        {
            p.Excute(this);
        }
    }   
}
