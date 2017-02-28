using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

    float onTimer;
    public float onTime;

    public float offTime;
    float offTimer;

    bool on = true;
    string text;

	// Use this for initialization
	void Start () {
        onTimer = onTime;
        offTimer = offTime;

        text = GetComponent<Text>().text;
	}

    // Update is called once per frame
    void Update() {
        if (on) {
            GetComponent<Text>().text = text;

            onTimer -= Time.deltaTime;

            if (onTimer < 0) {
                on = false;
                onTimer = onTime;
            }
        }

        else {
            GetComponent<Text>().text = "";

            offTimer -= Time.deltaTime;

            if (offTimer < 0) {
                on = true;
                offTimer = offTime;
            }
        }
    }
}
