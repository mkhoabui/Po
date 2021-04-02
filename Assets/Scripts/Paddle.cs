using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{
    //configuration parameters

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float minX = 1f;
    [SerializeField] float movementSpeed = 1f;
    int prisonerHP = 0;
    

    float prisonerBallRecieve = 0f;
    float prisonerPos = 0f;

    //cached references
    GameStatus theGameStatus;
    Ball theBall;
    int tempHits;
    TimeFlowManager theTimeFlowManager;

    // Start is called before the first frame update
    void Start()
    {
        theGameStatus = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
        theTimeFlowManager = FindObjectOfType<TimeFlowManager>();
        int random = Random.Range(1, 4);
        if (random == 1)
        {
            prisonerHP = 40;
        }
        else if (random == 2)
        {
            prisonerHP = 60;
        }
        else
            prisonerHP = 80;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Prisoner")
        {
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            prisonerPos = Mathf.Lerp(prisonerPos, prisonerBallRecieve, 0.05f);
            paddlePos.x = Mathf.Clamp(theBall.transform.position.x + prisonerPos, minX, maxX);
            transform.position = paddlePos;
        }
        else
        {
            if (theTimeFlowManager.IsTimeSlowed() == true)
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
                transform.position = paddlePos;

                float translation = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.unscaledDeltaTime;
                transform.Translate(translation, 0, 0);
            }
            else
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
                transform.position = paddlePos;

                float translation = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
                transform.Translate(translation, 0, 0);
            }
        }
        
    }

    private float GetXPos()
    {
        if (theGameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return this.transform.position.x;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Prisoner")
        {
            prisonerHP--;
            if (prisonerHP <= 0)
            {
                int random = Random.Range(1, 3);
                if (random % 2 == 0)
                    prisonerBallRecieve = -1.5f;
                else
                    prisonerBallRecieve = 1.5f;
            }
            else
            {
                int random = Random.Range(1, 5);
                if (random == 1 || random == 4)
                    prisonerBallRecieve = 0f;
                else if (random == 2)
                    prisonerBallRecieve = 1.2f;
                else if (random == 3)
                    prisonerBallRecieve = -1.2f;
            }
        }
    }
}
