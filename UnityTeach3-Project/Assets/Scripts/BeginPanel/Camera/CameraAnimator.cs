using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator _animator;
    private UnityAction _action;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void TurnLeft(UnityAction action)
    {
        _animator.SetTrigger("Left");
        _action = action;
    }
    public void TurnRight(UnityAction action)
    {
        _animator.SetTrigger("Right");
        _action = action;
    }
    public void PlayOver()
    {
        _action?.Invoke();
        _action = null;
    }
}
