using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CusorScript : MonoBehaviour
{


    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = Input.mousePosition;
        
        Vector3 cursorWorldPos = mainCamera.ScreenToWorldPoint(cursorPos);
        cursorPos.z = 0f;
        transform.position = cursorWorldPos;

    }
}
