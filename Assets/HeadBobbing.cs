using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{

    [SerializeField] private float timer = 0.0f; 
    [SerializeField] private float bobbingSpeed = 0.18f; 
    [SerializeField] private float bobbingAmount = 0.2f; 
    [SerializeField] private float midpoint = 2.0f; 
    [SerializeField] private bool isMoving;
 
    void Update () { 
        float waveslice = 0.0f; 

        if (!isMoving) { 
            timer = 0.0f; 
        } 
        else { 
            waveslice = Mathf.Sin(timer); 
            timer = timer + bobbingSpeed; 
            
            if (timer > Mathf.PI * 2.0f) { 
                timer = timer - (Mathf.PI * 2.0f); 
            } 
        } 
        if (waveslice != 0.0f) { 
            float translateChange = waveslice * bobbingAmount; 
            float totalAxes = 2f; 
            totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f); 
            translateChange = totalAxes * translateChange; 

            Vector3 posi = transform.localPosition;
            posi.y = midpoint + translateChange;
            transform.localPosition = posi; 
        } 
        else { 
            Vector3 posi = transform.localPosition;
            posi.y = midpoint;
            transform.localPosition = posi;
        } 
    }
}
