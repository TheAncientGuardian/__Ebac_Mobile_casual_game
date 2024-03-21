using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation IsAnimation;
    public AnimationClip run;
    public AnimationClip idle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(idle);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            PlayAnimation(run);
        }
    }

    private void PlayAnimation(AnimationClip c)
    {
        /*IsAnimation.clip = c;
        IsAnimation.Play();*/
        IsAnimation.CrossFade(c.name);
    }
}
