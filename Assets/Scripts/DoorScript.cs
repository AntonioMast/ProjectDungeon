﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************************************************
//Place this script on every child object for every possible connecting path on a floor.
//Two 2D Colliders (one is a extending trigger, the other a stricter collision) is necessary per child
//and a Rigidbody 2D is neccessary on the parent for this strategy to work.
//Ultimately, this method should allow for dynamic pathing.
//****************************************************************************************************

public class DoorScript : MonoBehaviour
{
    int counterSinceSpawn;
    int counter = 5;
    bool hasMoved;


    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        counterSinceSpawn = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counterSinceSpawn > 0)
        {
            counterSinceSpawn -= 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counterSinceSpawn > 0 && collision.tag == "Floor") //ensures that the oldest object does not move
        {
            Vector3 tmp;
            Vector2 tmp2D;
            transform.parent.gameObject.GetComponent<StageScript>().staySpawned = true;
            switch (this.gameObject.name) //moves object if incorrectly placed
            {
                case "NorthCollider":
                    tmp = collision.gameObject.transform.position - new Vector3(0, 22.4f, 0);
                    tmp2D = new Vector2(tmp.x, tmp.y);
                    transform.parent.gameObject.active = false;
                    if (Physics2D.OverlapCircle(tmp2D, 5f) == false)
                    {
                        transform.parent.position = tmp;
                        transform.parent.gameObject.active = true;
                    }
                    break;
                case "EastCollider":
                    tmp = collision.gameObject.transform.position - new Vector3(22.4f, 0, 0);
                    tmp2D = new Vector2(tmp.x, tmp.y);
                    transform.parent.gameObject.active = false;
                    if (Physics2D.OverlapCircle(tmp2D, 1f) == false)
                    {
                        transform.parent.position = tmp;
                        transform.parent.gameObject.active = true;
                    }
                    break;
                case "SouthCollider":
                    tmp = collision.gameObject.transform.position + new Vector3(0, 22.4f, 0);
                    tmp2D = new Vector2(tmp.x, tmp.y);
                    transform.parent.gameObject.active = false;
                    if (Physics2D.OverlapCircle(tmp2D, 5f) == false)
                    {
                        transform.parent.position = tmp;
                        transform.parent.gameObject.active = true;
                    }
                    break;
                case "WestCollider":
                    tmp = collision.gameObject.transform.position + new Vector3(22.4f, 0, 0);
                    tmp2D = new Vector2(tmp.x, tmp.y);
                    transform.parent.gameObject.active = false;
                    if (Physics2D.OverlapCircle(tmp2D, 5f) == false)
                    {
                        transform.parent.position = tmp;
                        transform.parent.gameObject.active = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Floor") //honestly i forgot why i did the counter for this, something to do with how many objects can correctly spawn
        {
            transform.parent.gameObject.GetComponent<StageScript>().staySpawned = true;
            if (counter <= 0) { Destroy(gameObject); }
        }
        counter--; 
    }
}