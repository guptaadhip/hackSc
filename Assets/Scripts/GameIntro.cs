using UnityEngine;
using System.Collections;

public class GameIntro : MonoBehaviour {

	float timer = 0;
	Vector3 startPos, endPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = new Vector3( transform.position.x,  transform.position.y +7f,  transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if(timer>2f)
		{
			transform.position = Vector3.Lerp(startPos, endPos, timer);
		}

	}
}
