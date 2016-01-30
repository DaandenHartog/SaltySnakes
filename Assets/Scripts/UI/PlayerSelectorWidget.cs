using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSelectorWidget : MonoBehaviour
{

    public int PlayerIndex;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void SetSelectedCharacter(GameObject character)
    {
        if (!image.enabled) { image.enabled = true; }
        transform.SetParent(character.transform, false);
        Bounce();
    }

    private void Bounce()
    {
        gameObject.PunchScale(Vector3.one * 1.5f, .3f, 0);
    }
}
