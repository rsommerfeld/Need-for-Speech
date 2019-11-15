using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarButtonListener : MonoBehaviour {

    public Transform car;
    public Component carmodel;
    public Vector3 position = new Vector3(0,0,0);
    public Vector3 rotation = new Vector3(0,0,0);
    public Vector3 scale = new Vector3(1,1,1);

    void OnMouseUp()
    {
        foreach (Transform child in car.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Component model = Instantiate(carmodel, position, Quaternion.identity);
        model.transform.parent = car;
        model.transform.localPosition = this.position;
        model.transform.localScale = this.scale;
        model.transform.localEulerAngles = this.rotation;
    }
}
