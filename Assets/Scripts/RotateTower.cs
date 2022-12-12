using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTower : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 150 ;
    
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float move = Input.GetAxisRaw("Mouse X");
            transform.Rotate(0, -move * rotationSpeed * Time.deltaTime, 0);

        }
    }
}
