using UnityEngine;
using System.Collections;

public class BotCar : MonoBehaviour {

	float speed ;
	public float stopPoint ;

	// Use this for initialization
	void Start () {
		speed = Random.Range(35, 38);
	}
	
	// Update is called once per frame
	void Update () {
		if (stopPoint < 0) {
						if (transform.position.z > stopPoint) {
			
								transform.position -= new Vector3 (0, 0, Time.deltaTime * speed);
						} else {
								Destroy (gameObject);
						}
				}
		else
		{
			{
				if (transform.position.z < stopPoint) {
					
					transform.position += new Vector3 (0, 0, Time.deltaTime * speed);
				} else {
					Destroy (gameObject);
				}
			}
				}
	}
}
