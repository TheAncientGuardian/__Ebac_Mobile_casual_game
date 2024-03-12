using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    //privates
    private  bool _canRun;
    private Vector3 _pos;
    

    private void Start()
    {
        _canRun =true;
    }

    void Update()
    {
        if(!_canRun)return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;


        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            _canRun = false;
        }
    }
}
