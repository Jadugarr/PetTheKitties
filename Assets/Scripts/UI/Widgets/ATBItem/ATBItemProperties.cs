using UnityEngine;

public class ATBItemProperties : IWidgetProperties
{
    public Sprite CharacterSprite;
    public int CharacterId;

    public ATBItemProperties(Sprite characterSprite, int characterId)
    {
        CharacterSprite = characterSprite;
        CharacterId = characterId;
    }
}