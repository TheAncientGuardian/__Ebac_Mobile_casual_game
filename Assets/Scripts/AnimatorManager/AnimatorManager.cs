using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;
    public enum AnimationType
    {
        IDLE,
        RUN, 
        DEAD
    }
    
    // Declara um método público chamado Play que aceita um parâmetro do tipo AnimationType
public void Play(AnimationType type, float currentSpeedFactor = 1f)
{
    // Inicia um loop foreach que percorre cada elemento na coleção animatorSetups
    foreach(var animation in animatorSetups)
    {
        // Verifica se o tipo da animação atual corresponde ao tipo fornecido ao método
        if(animation.type == type)
        {
            // Aciona a animação no Animator usando o gatilho especificado em animation.trigger
            animator.SetTrigger(animation.trigger);
            animator.speed = animation.speed * currentSpeedFactor;
            // Sai do loop, interrompendo a iteração, pois a animação correspondente foi encontrada e acionada
            break;
        }
    }
    // Fim do método Play
}


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Play(AnimationType.RUN);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            Play(AnimationType.DEAD);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            Play(AnimationType.IDLE);
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}
