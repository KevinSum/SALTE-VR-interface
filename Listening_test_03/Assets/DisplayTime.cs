using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using OscJack;

// Previously used to display pasted time. Now used to display instructions
public class DisplayTime : MonoBehaviour {
    public Text text; // get text component 
    private float timer = 0.0f; // initialise timer to 0 
    public float timeLimitInMin = 10.0f; // set time limit (in minute)
    [HideInInspector]
    public bool gameStarted = false; // initialise game start state
    [HideInInspector]
    public bool gameOver = false; // initialise gmae over state
    [HideInInspector]
    GameObject CenterEyeAnchor; // call Print script
    [HideInInspector]
    Print tracker; // call Print script
    [HideInInspector]
    OSCData OSCData; // call Print script

    public string IPAddress = "127.0.0.1"; // IP address for OSC 
	public int oscPortOut = 9001; // Port for OSC out
	OscClient client;

    // Use this for initialization
    void Start () {
		// Setup the osc clients 
		client = new OscClient(IPAddress, oscPortOut);

        // locate OSCData script
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
        OSCData = CenterEyeAnchor.GetComponent<OSCData>();
        // locate Print script
        tracker = CenterEyeAnchor.GetComponent<Print>();

        /*
        text.text = timeLimitInMin + ":00"; // initialise text
        timer = timeLimitInMin * 60; // change time limit to seconds
        */
    }
	
	// Update is called once per frame
	void Update () {
        // start game 
        if (OVRInput.GetDown(OVRInput.RawButton.X) && Mathf.Abs(tracker.ele) < OSCData.triggerRange && OSCData.save_state == false)
        {
            if (gameStarted == false)
            {
                gameStarted = true;
                text.enabled = false;
                text.text = "Look at horizon and press X when ready for next trial";
                client.Send("/lochead/test", "start");
            }
            else if (gameStarted == true) {
             
                text.enabled = false;
            }


        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            text.enabled = true;
        }

        // count down
        /*
        if (gameStarted == true)
        {
            timer = timer - Time.deltaTime; // count time
            int minutes = Mathf.FloorToInt(timer / 60F); // convert to minute
            int seconds = Mathf.FloorToInt(timer - minutes * 60); // convert to seconds
            string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds); // set string
            text.text = niceTime; // update text with previous string
            //print(timer);


        }
        // when time is up
        if (Mathf.FloorToInt(timer) == 0)
        {
            gameStarted = false; // gmae stop
            gameOver = true; // game over
			client.Send ("/lochead/test", "end");
        }
        */
    }
}
