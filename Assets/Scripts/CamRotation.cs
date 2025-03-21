using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CamRotation : MonoBehaviour
{
    public Vector2 MousePos;
    public Vector3 DesiredRot;
    [SerializeField]private Vector3 FullDesiredRot;
    public Vector3 DesiredRotVelocity;
    [SerializeField]Vector3 PlayerRotation;
    public float LookSpeed;
    [SerializeField] private float xVel;
    [SerializeField] private float yVel;
    [SerializeField] public Transform Head;
    public float speed;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!transform.parent.GetComponent<PhotonView>().IsMine)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        DesiredRot = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"),0);
        FullDesiredRot += DesiredRot*Time.deltaTime*LookSpeed;
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, DesiredRot,0.2f);


        transform.eulerAngles += DesiredRot;
        //transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles,DesiredRot,ref DesiredRotVelocity,LookSpeed);

        //transform.position = Vector3.Lerp(transform.position, Head.position, 0.9f);
        transform.position = Vector3.MoveTowards(transform.position, Head.position, (transform.position-Head.position).sqrMagnitude*speed*Time.deltaTime);

        //PlayerRotation = new Vector3(Mathf.SmoothDampAngle(transform.eulerAngles.x,FullDesiredRot.x,ref xVel,0.1f), Mathf.SmoothDampAngle(transform.eulerAngles.y, FullDesiredRot.y, ref yVel, 0.1f), 0);
        //transform.eulerAngles = PlayerRotation;
    }
    
}
