using UnityEngine;

public class Selection : MonoBehaviour
{
    /*public Color highlightColor;
    public Color selectionColor;

    private Color originalColor;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private void Update()
    {*/
    /*if (highlight != null)
    {
        highlight.GetComponent<SpriteRenderer>().color = originalColor;
        highlight = null;
    }*/

    /*Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Debug.Log(worldMousePosition);*/
    /*if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
    {
        highlight = raycastHit.transform;
        if (highlight.CompareTag("Selectable") && highlight != selection)
        {
            if (highlight.GetComponent<SpriteRenderer>().color != highlightColor)
            {
                originalColor = highlight.GetComponent<SpriteRenderer>().color;
                highlight.GetComponent<SpriteRenderer>().color = highlightColor;
            }
        }
        else
        {
            highlight = null;
        }
    }*/
    /*if (Input.GetKey(KeyCode.Mouse0))
    {
        if (selection != null)
        {
            selection.GetComponent<SpriteRenderer>().color = originalColor;
            selection = null;
        }
        Debug.Log("1: " + raycastHit);
        if (Physics.Raycast(ray, out raycastHit))
        {
            Debug.Log("2: " + raycastHit.transform.name);
            selection = raycastHit.transform;
            if (selection.CompareTag("Selectable"))
            {
                selection.GetComponent<SpriteRenderer>().color = selectionColor;
            }
            else
            {
                selection = null;
            }
        }
    }*/
    //}

    public Ray ray;
    public RaycastHit hit;
    public Vector2 mousePos;
    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
        if (hit)
        {
            //Debug.Log(hit.transform.name);
        }
        else
        {
            //Debug.Log("nothing");
        }

    }
}
