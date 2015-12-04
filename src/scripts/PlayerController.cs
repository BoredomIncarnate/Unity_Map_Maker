using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float Speed = 6;
    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        RotateCam();
        Move();
    }

    void RotateCam()
    {
        float rotHor = Input.GetAxis("Mouse X");
        float rotVer = -Input.GetAxis("Mouse Y");
        transform.Rotate(0, rotHor, 0);
        //float x = Camera.main.transform.eulerAngles.x;
        Mathf.Clamp(rotVer, -40, 40);
        Camera.main.transform.Rotate(rotVer, 0, 0);
    }

    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            gameObject.transform.position += new Vector3(0, 1, 0);
        

        Vector3 movment = new Vector3(hor, 0, ver);
        movment = transform.rotation * movment;

        cc.SimpleMove(movment * Speed);
    }

}
