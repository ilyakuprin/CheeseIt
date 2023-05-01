using UnityEngine;

public class HeashingAnimationName : MonoBehaviour
{
    [HideInInspector] public int Right;
    [HideInInspector] public int HitRight;
    [HideInInspector] public int Left;
    [HideInInspector] public int HitLeft;
    [HideInInspector] public int Up;
    [HideInInspector] public int Down;
    [HideInInspector] public int Climp;
    [HideInInspector] public int TripOver;
    [HideInInspector] public int HitSlide;
    [HideInInspector] public int Death;
    [HideInInspector] public int Drop;
    [HideInInspector] public int StartRun;

    private void Start()
    {
        Right = Animator.StringToHash("Right");
        HitRight = Animator.StringToHash("HitRight");
        Left = Animator.StringToHash("Left");
        HitLeft = Animator.StringToHash("HitLeft");
        Up = Animator.StringToHash("Up");
        Down = Animator.StringToHash("Down");
        Climp = Animator.StringToHash("Climp");
        TripOver = Animator.StringToHash("TripOver");
        HitSlide = Animator.StringToHash("HitSlide");
        Death = Animator.StringToHash("Death");
        Drop = Animator.StringToHash("Drop");
        StartRun = Animator.StringToHash("Start");
    }
}
