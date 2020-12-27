namespace Entitas.World
{
    public enum CharacterGroundState
    {
        Undefined = 0,
        OnGround = 10,
        OnSlopeAhead = 20,
        OnSlopeBehind = 21,
        Airborne = 30,
        Grappled = 40,
        Planted = 41,
    }
}