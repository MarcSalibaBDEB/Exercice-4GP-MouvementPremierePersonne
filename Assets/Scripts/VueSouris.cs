using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VueSouris : MonoBehaviour
{
    [SerializeField] private GameObject joueur;
    [SerializeField] private float rotationXMinimale;
    [SerializeField] private float rotationXMaximale;
    [SerializeField] private float vitesseRotation;
    float rotationY;
    float rotationX;
    // Start is called before the first frame update
    void Start()
    {
        rotationX = joueur.transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * vitesseRotation;
        rotationX -= Input.GetAxis("Mouse Y") * Time.deltaTime * vitesseRotation;

        rotationX = Mathf.Clamp(rotationX, rotationXMinimale, rotationXMaximale);

        joueur.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
