using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using UnityEngine.Events;

public class Looking : MonoBehaviour
{

    [SerializeField] Transform targetPointer;
    [SerializeField] Rig rig;
    [SerializeField] Animator animator;

    public UnityEvent onTurning;
    public UnityEvent onFinished;

    // Start is called before the first frame update
    void Start() => rig.weight = 0;

    private void OnEnable() => GazeTarget.OnLooking += SelectTarget;
    private void OnDisable() => GazeTarget.OnLooking -= SelectTarget;


    void LookingAnim()
    {

        onTurning.Invoke();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 1, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.05f);
        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 0, 0.4f).SetEase(Ease.InOutQuad)).OnComplete(() => animator.SetTrigger("Victory"));

        sequence.Play();
    }


    void SelectTarget(Transform target)
    {
        targetPointer.position = new Vector3(target.position.x, target.position.y, target.position.z);

        LookingAnim();
    }


    public void VictoryEvent() => onFinished.Invoke();

}
