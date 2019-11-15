using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonListener : MonoBehaviour {

    public Camera menuCamera, mainCamera;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.menuCamera.enabled)
            {
                this.menuCamera.enabled = false;
                this.mainCamera.enabled = true;
            }
            else
            {
                this.menuCamera.enabled = true;
                this.mainCamera.enabled = false;
            }
        }
    }
}
