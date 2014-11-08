using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	float speed =3f;
	public float stopPoint ;
	public int isFacingLeft = 1;

	//public CarController controller;

	public bool moving = true;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
		Move();
	}

	void Move()
	{
		if(transform.position.x *isFacingLeft >stopPoint )
		{
			moving = true;
			transform.position -= new Vector3(Time.deltaTime*3 *isFacingLeft*speed,0, 0);
		}
		else if(transform.position.x *isFacingLeft<= stopPoint)
		{
			//set the controller
			print ("stop car");
			moving = false;
		}
	}
}
