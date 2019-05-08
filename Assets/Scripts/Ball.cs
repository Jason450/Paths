using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallState { Inactive, NextDisplay, GetStart, Moving }
    public BallState currentBallState;

    SpriteRenderer spriteRenderer;
    Vector2 position;

    public bool vertical;
    public bool horizontal;
    public bool leftToRight;
    public bool rightToLeft;

    public float speed;
    public Transform transf;
    public Transform targetTransf;
    public int randomColor;

    public List<Transform> starts;
    public List<Sprite> colors;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transf = GetComponent<Transform>();
        currentBallState = BallState.Inactive;
	}
	
	void Update ()
    {
        switch (currentBallState)
        {
            case BallState.Inactive:
                Inactive();
                break;
            case BallState.NextDisplay:
                NextDisplay();
                break;
            case BallState.GetStart:
                GetStart();
                break;
            case BallState.Moving:
                Moving();
                break;
            default:
                break;
        }
    }

    #region Updates

    void Inactive()
    {
        randomColor = Random.Range(0, 4);

        if (randomColor == 0) spriteRenderer.color = new Color(255, 0, 0);
        else if (randomColor == 1) spriteRenderer.color = new Color(255, 255, 0);
        else if (randomColor == 2) spriteRenderer.color = new Color(0, 255, 0);
        else if (randomColor == 3) spriteRenderer.color = new Color(0, 255, 255);
        else if (randomColor == 4) spriteRenderer.color = new Color(0, 0, 255);

        //spriteRenderer.sprite = colors[Random.Range(0, 4)];

        currentBallState = BallState.NextDisplay;
    }

    void NextDisplay()
    {
        currentBallState = BallState.GetStart;
    }

    void GetStart()
    {
        transf.localPosition = starts[Random.Range(0, 4)].localPosition;
        position = transf.localPosition;

        currentBallState = BallState.Moving;

        vertical = true;
    }

    void Moving()
    {
        if(vertical)
        {
            position.y -= speed * Time.deltaTime;
            transf.localPosition = position;
        }

        if(horizontal)
        {
            transf.localPosition = Vector2.MoveTowards(transform.position, targetTransf.position, Time.deltaTime * speed);

            if(transf.localPosition == targetTransf.position)
            {
                horizontal = false;
                vertical = true;
                leftToRight = false;
                rightToLeft = false;
            }
        }
    }

    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Door")
        {
            if(leftToRight)
            {
                targetTransf = collision.GetComponent<Door>().rightPoint.transform;
            }

            if(rightToLeft)
            {
                targetTransf = collision.GetComponent<Door>().leftPoint.transform;
            }
        }

        if (collision.tag == "LeftPoint")
        {
            if (position.y <= collision.transform.localPosition.y && vertical && targetTransf == null)
            {
                leftToRight = true;
                rightToLeft = false;
                vertical = false;
                horizontal = true;
            }
        }

        if (collision.tag == "RightPoint")
        {
            if (position.y <= collision.transform.localPosition.y && vertical && targetTransf == null)
            {
                rightToLeft = true;
                leftToRight = false;
                vertical = false;
                horizontal = true;
            }
        }
        Debug.Log("colliding " + collision);
    }
}
