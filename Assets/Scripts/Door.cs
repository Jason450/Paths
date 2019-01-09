using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public bool cannotClose;

    public GameObject leftPoint;
    public GameObject rightPoint;

    SpriteRenderer spriteRenderer;

    void Start ()
    {
        open = false;
        cannotClose = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Close();
	}
	
	void Update ()
    {
		
    }

    void OnMouseDown()
    {
        if(cannotClose)
        {
            return;
        }
        else
        {
            open = !open;
            
            if(open)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    void Open()
    {
        leftPoint.SetActive(true);
        rightPoint.SetActive(true);
        spriteRenderer.color = new Color(255, 255, 255);
        Debug.Log("open");
    }

    void Close()
    {
        leftPoint.SetActive(false);
        rightPoint.SetActive(false);
        spriteRenderer.color = new Color(0, 0, 0);
        Debug.Log("close");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            cannotClose = true;
            Debug.Log("cannot close");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            cannotClose = false;
            Debug.Log("can close");
        }
    }
}
