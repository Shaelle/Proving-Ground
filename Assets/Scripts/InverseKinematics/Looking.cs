using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

public class Looking : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Rig rig;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rig.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LookingAnim();
        }
    }


    void LookingAnim()
    {
        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 1, 0.5f));
        sequence.AppendInterval(0.2f);
        sequence.Append(DOTween.To(() => rig.weight, x => rig.weight = x, 0, 0.5f)).OnComplete(() => animator.SetTrigger("Victory"));

        sequence.Play();

    }

}
