using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WindScript : MonoBehaviour {
    public Vector2 windDirection;
    public float windSpeed = 100;
    private float countDownTimer;
    public float countDownMax = 3f;

    public Text inputText;

    public float coverTolerance = 2f;

    public Text countDownTimerText;
    public Image windDirIndicator;

    public GameObject[] allGameObjects;

    private float rotationCoolDown;
    public float rotationCoolDownMax = 0.5f;
    public float rotateBy = 5f;

    // Use this for initialization
    void Start () {
        windDirection = Vector2.left;
        countDownTimer = countDownMax;
        rotationCoolDown = rotationCoolDownMax;
	}
	
	// Update is called once per frame
	void Update () {
        countDownTimer -= Time.deltaTime;
        rotationCoolDown -= Time.deltaTime;
        //if the player presses the wind rotator buttons and the cooldown for rotating is under 0
        if (Input.GetAxis("Windrotator X") > 0.01 || Input.GetAxis("Windrotator Y") > 0.01 || Input.GetAxis("Windrotator X") < - 0.01 || Input.GetAxis("Windrotator Y") < -0.01 /*&& rotationCoolDown <= 0*/) {
            //rotate the wind direction and UI indicator by given degree

            inputText.GetComponent<Text>().text = ("X: "+Input.GetAxis("Windrotator X").ToString() + " Y: " +Input.GetAxis("Windrotator Y").ToString());

            windDirection = new Vector2(Input.GetAxis("Windrotator X"), Input.GetAxis("Windrotator Y")) /*Quaternion.AngleAxis(rotateBy * Mathf.Sign(Input.GetAxis("Windrotator X")), Vector3.forward)*windDirection*/;
            windDirection = windDirection.normalized;

            

            windDirIndicator.transform.eulerAngles = new Vector3(windDirIndicator.transform.eulerAngles.x, windDirIndicator.transform.eulerAngles.y, Mathf.Atan2(-windDirection.x, windDirection.y) * Mathf.Rad2Deg);

            rotationCoolDown = rotationCoolDownMax;
        }
        

        if(countDownTimer <= 0)
        {
            //apply force to everything
            countDownTimer = countDownMax;
            allGameObjects = GameObject.FindGameObjectsWithTag("Physics");
            for (int i = 0; i<allGameObjects.Length;i++) {
                if (allGameObjects[i].GetComponent<Rigidbody2D>() != null) {
                    //check if it is in cover
                    if (!Physics2D.Linecast(allGameObjects[i].transform.position, new Vector2 (allGameObjects[i].transform.position.x+(windDirection.x * coverTolerance*-1), allGameObjects[i].transform.position.y+(windDirection.y * coverTolerance*-1)), 1 << LayerMask.NameToLayer("Ground")))
                    {
                        allGameObjects[i].GetComponent<ReactionToWindScript>().GetBlownAway(windDirection * windSpeed);
                    }


                }
                }

        }
        countDownTimerText.GetComponent<Text>().text = countDownTimer.ToString();
	}
}
