using UnityEngine;
using CanasSource;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private const float DISAPPEAR_TIME_MAX = 1f;
    [SerializeField] private int fontSize;

    private Vector3 moveVector;
    private static int sortingOrder;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int dmgAmount, bool isCriticalHit)
    {
        if(dmgAmount != 0)
        {
            textMesh.SetText(dmgAmount.ToString());
        }
        else
        {
            textMesh.SetText("Miss");
        }

        if(!isCriticalHit)
        {
            textMesh.fontSize = fontSize;
            //textMesh.color = new Color(1, 0.1367925f, 0.1367925f);
        }
        else
        {
            textMesh.fontSize = fontSize * 2f;
            //textMesh.color = new Color(1, 0.8128629f, 0.1367925f);
        }
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIME_MAX;
        moveVector = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), 0f) * 10f;
    }

    public DamagePopup Create(Transform dmgPopupTransform, int dmgAmount, bool isCriticalHit = false)
    {
        DamagePopup dmgPopup = dmgPopupTransform.GetComponent<DamagePopup>();
        dmgPopup.Setup(dmgAmount, isCriticalHit);
        return dmgPopup;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= 8f * Time.deltaTime * moveVector;

        if(disappearTimer > DISAPPEAR_TIME_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= decreaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a < 0)
            {
                Singleton<PoolDmgPoint>.Instance.RemoveObjectToPool(gameObject, textMesh);
            }
        }
    }
}
