using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElementPulse : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float size = 1f;

    private IEnumerator Start()
    {

        gameObject.ScaleTo(Vector3.one * size, speed, 0);
        gameObject.ScaleTo(Vector3.one, speed, 0);
        yield return new WaitForSeconds(speed);
    }

}
