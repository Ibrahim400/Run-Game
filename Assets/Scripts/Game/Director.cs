using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour, IPickuple
{
    [SerializeField] float time;
    [SerializeField] float speed = 20;

    public enum TYPE { STATIC, EDITABLE, ROTATIONAL }

    public TYPE directorType;

    Vector2 deltaPos;
    Vector2 currentPos;
    bool isInDirecting;
    float timer;
    float angle;
    bool isRotating;


    private void OnEnable()
    {
        if (directorType != TYPE.EDITABLE) return;
        isInDirecting = true;
    }

    private void Start()
    {
        timer = time;
    }

    private void Update()
    {
        switch (directorType)
        {
            case TYPE.EDITABLE:
                EditeDirector();
                break;
            case TYPE.ROTATIONAL:
                RotationalDirector();
                break;
            default:
                return;                
        }
    }

    void EditeDirector()
    {
        if (isInDirecting)
        {
            DetectMouse();
            Vector3 turn = DirectorAngle();

            Quaternion look = Quaternion.LookRotation(turn);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speed * Time.deltaTime);            
        }

        if (Input.GetMouseButtonUp(0))
        {
            isInDirecting = false;
        }
    }

    void RotationalDirector()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            angle += 90;
            isRotating = true;
            timer = 0;            
        }

        if (isRotating)
        {
            StartCoroutine(StopRotation());
            float smooth = 20;

            Quaternion turn = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, turn, smooth * Time.deltaTime);


            Debug.LogError("Waiting to rotate");
        }
    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(0.5f);
        isRotating = false;
    }

    void DetectMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButtonDown(0))
        {
            deltaPos = new Vector2(mouseX, mouseY);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = new Vector2(mouseX, mouseY);

            if (mousePos.magnitude > 0.4f)
                currentPos = mousePos - deltaPos;            
        }
    }

    Vector3 DirectorAngle()
    {
        bool forward = currentPos.x > 0 && currentPos.y > 0 || currentPos.y > 0 && currentPos.x == 0;
        bool right = currentPos.x > 0 && currentPos.y < 0;
        bool left = currentPos.y > 0 && currentPos.x < 0 || currentPos.y == 0 && currentPos.x < 0;
        bool backward = currentPos.x < 0 && currentPos.y < 0 || currentPos.x == 0 && currentPos.y < 0;

        if (forward) return Vector3.forward;
        else if (right) return Vector3.right;
        else if (left) return Vector3.left;
        else if (backward) return Vector3.back;

        return Vector3.zero;
    }

    private void OnMouseDown()
    {
        if (directorType != TYPE.EDITABLE) return;

        StartCoroutine(Destroying());
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    public void Excute(Player player)
    {
        player.SetPointer(this);
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
