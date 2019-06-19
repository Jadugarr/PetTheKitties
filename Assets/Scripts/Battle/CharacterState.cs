public enum CharacterState
{
    Idle = 0,
    Moving = 10,
    MoveEnding = 11,
    JumpStart = 20,
    Jumping = 30,
    JumpEnding = 31,
    Falling = 32,
    Dead = 40,
    WaitingToChoose = 50,
    ChooseAction = 60,
    PreparingAction = 70,
    Acting = 80
}