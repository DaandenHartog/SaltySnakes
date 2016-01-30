using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScaleButtonOnPress : MonoBehaviour
{
    [SerializeField]
    private float scaleFactor = 1.4f;

    private float duration = .1f;

    private void Awake()
    {
        Button b = GetComponent<Button>();
        if (b == null) { Debug.Log("No button"); return; }
        b.onClick.AddListener(delegate
        {
            gameObject.ScaleTo(Vector3.one * scaleFactor, duration, 0f);
            gameObject.ScaleTo(Vector3.one, duration, duration);
        });
    }
}
