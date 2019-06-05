using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointingAndInteracting : MonoBehaviour
{
    [SerializeField] private float pointRange;

    [SerializeField] private TextMesh possibleActionText;

    [SerializeField] private InteractibleObject[] interactibleObjects;

    private InteractibleObject pointingAtInteractible;

    private RaycastHit hit;

    //Debug
    Renderer renderer;
    LineRenderer lnRd;

    public Text dbgHand;
     public Text dbgCam;

    public Transform  HAND;
    public Transform CAM;

    void Start() 
    {
        StartCoroutine(HitCheck());
        renderer = gameObject.GetComponent<Renderer>();
        lnRd= gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {       
       PointObjects();
       //dbgCam.text = "Cam // x : " + CAM.transform.position.x +" y :" + CAM.transform.position.y + " z : " + CAM.transform.position.z;  
       dbgHand.text = "Hand // x : " + HAND.transform.position.x +" y :" + HAND.transform.position.y + " z : " + HAND.transform.position.z;  
       if(hit.collider != null)
       {
        lnRd.enabled = true;
        lnRd.SetPosition(0,transform.position);
        lnRd.SetPosition(1,hit.point);
       }
       else
       {
           lnRd.enabled =  false;
       }
     //  print(pointingAtInteractible.objectName);
    }

    void PointObjects()
    {
        Ray ray = new Ray (transform.position , transform.forward);

        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if(Physics.Raycast(ray , out hit , pointRange))
        {
            
            if(hit.collider.CompareTag("Interactible") )
            {   
                if(pointingAtInteractible != null)
                {
                    ShowActionDescription();
                }

                if(Input.GetKeyDown(KeyCode.Space))
                    {
                        Interact(hit);
                    }
            }
        }

        else 
        {
            float lerp = Mathf.PingPong(Time.time, 0.1f) / 0.1f;
            renderer.material.color =  Color.Lerp(Color.red , Color.blue , lerp);
            possibleActionText.text = "";
                       
        }
    }

    void Interact(RaycastHit objectToInteract)
    {
        print("u  are interacting with: " + objectToInteract.collider.gameObject.name);
        //objectToInteract.collider.gameObject.SendMessage("Action");
    }

   void ShowActionDescription()
    {   
        renderer.material.color = Color.black;        
        possibleActionText.text = pointingAtInteractible.actionDescription;
    }

    IEnumerator HitCheck()
    {
        for(;;)
        {
            print("To no HitCheck");
            if(hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("Interactible"))
                {
                    print("Comparando");
                    for(int i = 0 ; i < interactibleObjects.Length ; i++)
                    {           
                        if(hit.collider.gameObject.name == interactibleObjects[i].objectName)
                        {
                                pointingAtInteractible = interactibleObjects[i];
                                print("Achei");
                        }
                    }
                }

                else
                {
                    pointingAtInteractible = null;
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
    }


}