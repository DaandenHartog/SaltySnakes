using UnityEngine;
using System.Collections;

public class GlobalSelectedCharacters : MonoBehaviour
{

    public static GlobalSelectedCharacters instance { get { return _instance ?? (_instance = FindObjectOfType<GlobalSelectedCharacters>()); } }
    private static GlobalSelectedCharacters _instance;

    public enum CharacterType
    {
        None = 0,
        Daan = 1,
        Jeroen = 2,
        Inge = 3,
        Saskia = 4,
        Thom = 5
    }

    public CharacterType[] chosenCharacters { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        chosenCharacters = new CharacterType[4];
    }

    private void SetPlayerCharacter(int player, int character)
    {
        chosenCharacters[player - 1] = (CharacterType)character;
    }

}
