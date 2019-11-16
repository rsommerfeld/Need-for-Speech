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
        updateThings();

    }

    private void updateThings() {
        if (inMenu)
        {
            car.GetComponent<CarController>().freeze = true;
            cameraController.objectToFollow = menu;
        }
        else
        {
            car.GetComponent<CarController>().freeze = false;
            cameraController.objectToFollow = car;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
            updateThings();
        }
    }
}
