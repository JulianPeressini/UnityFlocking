using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControls : MonoBehaviour
{
    private CharacterController self;

    private float axisX;
    private float axisY;
    private float axisZ;

    [SerializeField] private float movSpeed;
    [SerializeField] private float verticalSpeed;

    private Vector3 direction = new Vector3();

    void Start()
    {
        self = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInputs();

        Move();
    }

    void GetInputs()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            axisY = -verticalSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            axisY = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            axisY = verticalSpeed;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            axisY = 0;
        }

    }

    void Move()
    {
        direction.x = axisX * movSpeed;
        direction.y = axisY;
        direction.z = axisZ * movSpeed;

        self.Move(direction * Time.deltaTime);
    }
}
