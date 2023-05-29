using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] SpriteRenderer hover;
    [SerializeField] SpriteRenderer click;

    private void OnMouseEnter()
    {
        hover.enabled = true;
    }

    private void OnMouseExit()
    {
        hover.enabled = false;
    }
}
