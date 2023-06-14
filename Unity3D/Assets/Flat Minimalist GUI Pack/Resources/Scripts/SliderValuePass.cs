using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuePass : MonoBehaviour {

	Text progress;


    // Use this for initialization
    void Start () {
		progress = GetComponent<Text>();

	}
	
	public  void UpdateProgress (float content) {
		progress.text = System.Math.Round( content/1000.0f, 3) +" V";
	}


}
