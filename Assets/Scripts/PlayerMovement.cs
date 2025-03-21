using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float Speed;
    public Vector3 DesiredMoveDir;
    public GameObject Cam;
    public GameObject Feet;
    public bool Moving;
    private bool Sprinting;
    public float SprintMultiplier;
    public float MaxVelocity;

    [Header("Jumping")]
    private bool Jumping;
    public bool CanGround;
    public bool IsGrounded;
    public float JumpForce;
    public float JumpForceMultiplier;
    public LayerMask GroundLayerMask;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cam = Camera.main.gameObject;
        CanGround = true;

        if (!transform.parent.GetComponent<PhotonView>().IsMine)
        {
            Destroy(this);
        }
    }
    void Update()
    {
        DesiredMoveDir = (Cam.transform.right * Input.GetAxisRaw("Horizontal")) + (Cam.transform.forward * Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetAxisRaw("Horizontal")!=0|| Input.GetAxisRaw("Vertical") != 0)
        {
            Moving = true;
        }
        else
        {
            Moving=false;
        }
        if (IsGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                SprintMultiplier = 1.5f;
                rb.maxLinearVelocity = MaxVelocity * 1.5f;
            }
            else
            {
                SprintMultiplier = 1;
                rb.maxLinearVelocity = MaxVelocity;
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(Feet.transform.position, Vector3.down, out hit, 0.1f, GroundLayerMask))
        {
            if (CanGround)
            {
                IsGrounded = true;
                Speed = 160;
            }
        }
        else
        {
            IsGrounded = false;
            rb.maxLinearVelocity = MaxVelocity * 100f;
            Speed = 80;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded)
                StartCoroutine("Jump");
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(DesiredMoveDir.x, 0, DesiredMoveDir.z) * Time.deltaTime * Speed*SprintMultiplier, ForceMode.VelocityChange);
    }
    public IEnumerator Jump()
    {
        IsGrounded=false;
        CanGround = false;
        rb.maxLinearVelocity = MaxVelocity * 100f;
        rb.AddForce(new Vector3(DesiredMoveDir.x, JumpForceMultiplier, DesiredMoveDir.z) * JumpForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.04f);
        CanGround = true;
    }

}
