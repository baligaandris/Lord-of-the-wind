using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WindScript : MonoBehaviour {
    public Vector2 windDirection;
    public float windSpeed = 100;
    private float countDownTimer;
    public float countDownMax = 3f;

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
        if (Input.GetAxis("Windrotator") != 0 && rotationCoolDown <= 0) {
            //rotate the wind direction and UI indicator by given degree
            windDirection = Quaternion.AngleAxis(rotateBy * Mathf.Sign(Input.GetAxis("Windrotator")), Vector3.forward)*windDirection;
            Vector3 oldWindIndicatorRotation = windDirIndicator.transform.rotation.eulerAngles;
            windDirIndicator.transform.rotation = Quaternion.Euler(new Vector3(oldWindIndicatorRotation.x, oldWindIndicatorRotation.y, oldWindIndicatorRotation.z+(rotateBy * Mathf.Sign(Input.GetAxis("Windrotator")))));
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
