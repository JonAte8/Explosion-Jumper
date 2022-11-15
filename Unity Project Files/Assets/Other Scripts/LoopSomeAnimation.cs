using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSomeAnimation : MonoBehaviour
{
    Animator anim;
    public string AnimationName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
        {
            anim.Play(AnimationName, 0, 0);
        }
        if(!anim.GetCurrentAnimatorStateInfo(0).loop && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            anim.Play(AnimationName, 0, 0);
        }
    }
}
