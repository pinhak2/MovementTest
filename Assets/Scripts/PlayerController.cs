using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed, sensitivity, jumpForce;
    Camera cam;
    Rigidbody rb;
    Vector2 val;

    public GameObject floor;
    public Vector3 jumpCheckSize;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
         val = Movement();

        Rotation(val);

        bool isGrounded = Physics.OverlapBox(floor.transform.position, jumpCheckSize).Length > 0;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    Vector2 Movement()
    {
        Vector2 xMov = new Vector2(Input.GetAxisRaw("Horizontal") * transform.right.x, Input.GetAxisRaw("Horizontal") * transform.forward.z);
        Vector2 zMov = new Vector2(Input.GetAxisRaw("Vertical") * transform.forward.x, Input.GetAxisRaw("Vertical") * transform.forward.z);

        Vector2 velocity = (xMov + zMov).normalized * speed * Time.deltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.y);

        return velocity;
    }

    void Rotation(Vector2 val)
    {
       

        float yRot = Input.GetAxisRaw("Mouse X") * sensitivity;

        rb.rotation *= Quaternion.Euler(0, yRot * Time.deltaTime, 0);

        float xRot = Input.GetAxisRaw("Mouse Y") * sensitivity;

        float x_cam_rot = cam.transform.eulerAngles.x;

        x_cam_rot -= xRot;

        float camEulerAnglesX = cam.transform.localEulerAngles.x;

        camEulerAnglesX -= xRot * Time.deltaTime;

        cam.transform.localEulerAngles = new Vector3(camEulerAnglesX, 0, 0);

        rb.velocity = new Vector3(val.x, rb.velocity.y, val.y);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce));
    }
}
