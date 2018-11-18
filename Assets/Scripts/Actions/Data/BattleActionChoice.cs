public struct BattleActionChoice
{
    public ActionType ActionType;
    // Defines if this action is currently available to the character.
    // This is used to flexibly disable actions (because of afflictions for example) while still keeping it in the list of choices
    // So that way, we can grey out the choice in a visual list
    public bool IsAvailable;
}