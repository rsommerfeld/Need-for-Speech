using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonListener : MonoBehaviour {

    public GameObject car, menu, mainCamera;

    public bool inMenu = true;
    private CameraController cameraController;

    public void Start()
    {
        cameraController = mainCamera.GetComponent<CameraController>();
        updateCamera();

    }

    private void updateCamera() {
        if (inMenu)
        {
            cameraController.objectToFollow = menu;
        }
        else
        {
            cameraController.objectToFollow = car;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
            updateCamera();
        }
    }
}
