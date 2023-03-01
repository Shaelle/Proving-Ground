using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

public class Looking : MonoBehaviour
{

    [SerializeField] Transform targetPointer;
    [SerializeField] Rig rig;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start() => rig.weight = 0;

    private void OnEnable() => GazeTarget.OnLooking += SelectTarget;
    private void OnDisable() => GazeTarget.OnLooking -= SelectTarget;


    void LookingAnim()
    {        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 1, 0.7f).SetEase(Ease.OutCubic));
        sequence.AppendInterval(0.1f);
        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 0, 0.5f).SetEase(Ease.OutCubic)).OnComplete(() => animator.SetTrigger("Victory"));

        sequence.Play();
    }


    void SelectTarget(Transform target)
    {
        targetPointer.position = new Vector3(target.position.x, target.position.y, target.position.z);

        LookingAnim();
    }

}