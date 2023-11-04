using UnityEngine;

public class PointerGenerator : MonoBehaviour
{
    LayerMask layer;
    GamePlayCanvas canvas;

    private void Start()
    {
        layer = LayerMask.GetMask("Ground");
        canvas = FindObjectOfType<GamePlayCanvas>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateDirector();
        }
    }

    void GenerateDirector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            GameObject dir = ObjectPool.instance.Get("Director");
            GameObject tile = hit.collider.gameObject;

            if (dir != null && hit.collider.CompareTag("Ground"))
            {
                dir.SetActive(true);
                dir.transform.position = tile.transform.position + new Vector3(0, 0.1f, 0);
                Enviroment.Instance.AddItem(dir);

                Physics.IgnoreCollision(hit.collider, dir.GetComponent<Collider>());

                canvas.directorsNum -= 1;
                canvas.UpdateDirectorsText();
            }
        }
    }
}
