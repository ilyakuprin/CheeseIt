using UnityEngine;

public class AnimationsName : MonoBehaviour
{
    [HideInInspector] public float TimeStart;
    [HideInInspector] public float TimeJump;
    [HideInInspector] public float TimeSlide;
    [HideInInspector] public float TimeHorizontalMoving;
    [HideInInspector] public float TimeClimp;
    [HideInInspector] public float TimeDeathFromWall;

    [SerializeField] private AnimationClip _start;
    [SerializeField] private AnimationClip _jump;
    [SerializeField] private AnimationClip _slide;
    [SerializeField] private AnimationClip _horizontalMoving;
    [SerializeField] private AnimationClip _climp;
    [SerializeField] private AnimationClip _deathFromWall;

    private void Start()
    {
        TimeStart = _start.length;
        TimeJump = _jump.length;
        TimeSlide = _slide.length;
        TimeHorizontalMoving = _horizontalMoving.length;
        TimeClimp = _climp.length;
        TimeDeathFromWall = _deathFromWall.length;
    }
}
