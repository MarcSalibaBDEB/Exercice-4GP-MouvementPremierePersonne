using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementJoueur : MonoBehaviour
{
    [SerializeField] private GameObject objectif;
    [SerializeField] private float vitesse;
    [SerializeField] private float saut;
    private CharacterController cc;
    private Vector3 bouger;
    private float velociteY;

    private Vector3 posInitiale;
    private Quaternion rotationInitiale;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        velociteY = Physics.gravity.y * Time.deltaTime;

        posInitiale = transform.position;
        rotationInitiale = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        bool surLeSol = cc.isGrounded;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            bouger = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * vitesse * 2, 0, Input.GetAxis("Vertical") * Time.deltaTime * vitesse * 2));
            cc.Move(bouger);
        }
        else
        {
            bouger = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * vitesse, 0, Input.GetAxis("Vertical") * Time.deltaTime * vitesse));
            cc.Move(bouger);
        }


        // Gestion des sauts et de la gravité
        if (surLeSol && Input.GetKey(KeyCode.Space))
        {
            // Sauter = appliquer une vitesse instantannée vers le haut
            velociteY = saut;
        }
        else if (surLeSol)
        {
            velociteY = 0;
        }

        // On applique toujours la formule de gravité
        velociteY += Physics.gravity.y * Time.deltaTime;

        cc.Move(new Vector3(0,velociteY * Time.deltaTime,0));
    }

    internal void remettreASaPositionInitiale()
    {
        cc.enabled = false;
        transform.position = posInitiale;
        transform.rotation = rotationInitiale;
        cc.enabled = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject == objectif)
        {
            remettreASaPositionInitiale();
        }
    }
}
