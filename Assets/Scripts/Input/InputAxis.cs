// Define custom axis here for use in InputConfiguration and InputSystem
public static class InputAxis
{
    public static string Cancel = "Cancel";
    public static string Pause = "Pause";
    public static string Move = "Move";
    public static string Interact = "Interact";
    public static string Jump = "Jump";
    public static string RaycastTest = "RaycastTest";

    public static readonly string[] AxisList =
    {
        Cancel,
        Pause,
        Move,
        Interact,
        Jump,
        RaycastTest
    };
}