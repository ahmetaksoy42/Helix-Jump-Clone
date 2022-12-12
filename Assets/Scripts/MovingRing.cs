using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingRing : MonoBehaviour
{
    private int a;

    [SerializeField] private int spinSpeed ;

    private void Start()
    {
     a = Random.Range(0, 10);

     spinSpeed = Random.Range(10, 40);
    }
    void Update()
    {
        if (transform!= null)
        {
            if (a == 5)
            {
                
                transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
               // transform.DORotate(new Vector3(0, 90, 0), 2f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
                //transform.DORotate(new Vector3(0, 90, 0), 5, RotateMode.WorldAxisAdd).SetLoops(100, LoopType.Yoyo);
            }
        }
       
        
    }
}
