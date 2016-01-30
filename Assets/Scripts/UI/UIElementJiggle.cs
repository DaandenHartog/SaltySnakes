using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElementJiggle : MonoBehaviour
{
    [SerializeField]
    private float angle = 30f;
    [SerializeField]
    private float speed = 1f;

    private IEnumerator Start()
    {
        while (true)
        {
            iTween.RotateTo(gameObject, new Vector3(0, 0, angle), speed);
            yield return new WaitForSeconds(speed);
            angle = -angle;
        }
    }

}
