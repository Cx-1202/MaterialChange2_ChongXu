using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int speed = 20;
    int jumpforce = 20;
    int jumpcount = 0;
    float xzlimit = 14.5f;
    float gravitymodifier = 2.5f;
    Rigidbody playerRB;
    MeshRenderer playerRDR;

    public Material[] mtrls; 
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRDR = GetComponent<MeshRenderer>();

        Physics.gravity *= gravitymodifier;
    }

    // Update is called once per frame
    void Update()
    {
        jumping();

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);


        if (transform.position.z > xzlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, xzlimit);
            playerRDR.material.color = mtrls[5].color;
        }
        else if (transform.position.z < -xzlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -xzlimit);
            playerRDR.material.color = mtrls[6].color;
        }
       

        if (transform.position.x > xzlimit)
        {
            transform.position = new Vector3(xzlimit,transform.position.y, transform.position.z);
            playerRDR.material.color = mtrls[3].color;
        }
        else if (transform.position.x < -xzlimit)
        {
            transform.position = new Vector3(-xzlimit, transform.position.y, transform.position.z);
            playerRDR.material.color = mtrls[4].color;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            jumpcount = 0;
            playerRDR.material.color = mtrls[1].color; 
        }
    }

    private void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount < 1)
        {
            playerRB.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            jumpcount++;
            playerRDR.material.color = mtrls[0].color;
        }
    }
}
