using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";
    public  bool invencible = false;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    //privates
    private  bool _canRun;
    private Vector3 _pos;
    public GameObject endScreen;
    public GameObject startScreen;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedAnimation = 7;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }
    void Update()
    {
        if(!_canRun)return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;


        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            if(!invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible)
            {
                EndGame();
            }

        }
    }

    private void MoveBack()
    {// method to move the animation of dead back and dont fall in the obstacule.
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }
    public void StartToRun()
    {
        _canRun =true;
        startScreen.SetActive(false);
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedAnimation);
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
        Debug.Log("Fast and Furious");
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);a
        Invoke(nameof(ResetHeight), duration);
    }


    public void ResetHeight(float animationDuration)
    {
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/
        transform.DOMoveY(_startPosition.y, animationDuration);

    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion 
}
