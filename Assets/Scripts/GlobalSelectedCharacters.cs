using UnityEngine;
using System.Collections;

public class GlobalSelectedCharacters : MonoBehaviour
{

    public enum CharacterType
    {
        Daan = 1,
        Jeroen = 2,
        Inge = 3,
        Saskia = 4,
        Thom = 5
    }

    public CharacterType Player1 { get; private set; }
    public CharacterType Player2 { get; private set; }
    public CharacterType Player3 { get; private set; }
    public CharacterType Player4 { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void SetPlayerCharacter(int player, int character)
    {

    }

}
