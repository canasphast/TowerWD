using CanasSource;
using UnityEngine;
using TMPro;

public class PoolDmgPoint : ObjectPool
{
    protected override void Awake()
    {
        Singleton<PoolDmgPoint>.Instance = this;
    }

    public override void GetObjectFromPool(Vector3 position, params object[] arrObject)
    {
        var a = GetObject(position);
        a.GetComponent<DamagePopup>().Create(a.transform, (int)arrObject[0], (bool)arrObject[1]);
    }

    public override void RemoveObjectToPool(GameObject theGO, params object[] arrObject)
    {
        TextMeshPro textMesh = (TextMeshPro) arrObject[0];
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
        textMesh.transform.localScale = Vector3.one;
        ReturnObject(theGO);
    }
}
