using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
     
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject tnt;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Material grassMat;
    [SerializeField] private Material slimeMat;
    [SerializeField] private Material tntMat;
    
    private float speed = 5.0f;
    private float horizontalInput;

    private float nextY = 0;
    private float nextX = 2;

    private bool gameOver = false;
    private bool lastTNT = false;


    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            int randint = Random.Range(0, 4);
            if (randint == 0 || randint == 1)
            {
                var _grass = Instantiate(grass, new Vector3(nextX, nextY, 0), transform.rotation);
                _grass.GetComponent<MeshRenderer>().material = grassMat;
                nextX = nextX + 4;
                nextY = nextY + Random.Range(0, 2) * 2 - 1;
            }
            else if (randint == 2)
            {
                var _slime = Instantiate(slime, new Vector3(nextX, nextY, 0), transform.rotation);
                _slime.GetComponent<MeshRenderer>().material = slimeMat;
                _slime.gameObject.name = "slime";
                nextX = nextX + 4;
                nextY = nextY + Random.Range(0, 2) * 2 - 1;
            }
            else if(i > 5 && randint == 3)
            {
                if (lastTNT == false)
                {
                    var _tnt = Instantiate(tnt, new Vector3(nextX, nextY, 0), transform.rotation);
                    _tnt.GetComponent<MeshRenderer>().material = tntMat;
                    _tnt.gameObject.name = "tnt";
                    lastTNT = true;
                    nextX = nextX + 2;
                    nextY = nextY + Random.Range(0, 2) * 2 - 1;
                }
                else
                {
                    var _grass = Instantiate(grass, new Vector3(nextX, nextY, 0), transform.rotation);
                    _grass.GetComponent<MeshRenderer>().material = grassMat;
                    nextX = nextX + 4;
                    nextY = nextY + Random.Range(0, 2) * 2 - 1;
                }
            }
        }

        portal.transform.position = new Vector3(nextX, nextY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver==false)
        {
            transform.rotation = Quaternion.identity;
        
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                print(Math.Round(myRigidbody.velocity.y));
                if (Math.Round(myRigidbody.velocity.y) == 0)
                {
                    myRigidbody.velocity = Vector3.up * 5;
                }
            }
            horizontalInput = Input.GetAxis("Horizontal");
        
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "slime")
        {
            myRigidbody.velocity = Vector3.up * 8;
        }
        else if (other.gameObject.name == "tnt")
        {
            gameOver = true;
            print("You lose");
            Instantiate(gameOverText, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        }
        else if (other.gameObject.name == "Portal")
        {
            print("You win");
            Instantiate(winText, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        }
    }
}
