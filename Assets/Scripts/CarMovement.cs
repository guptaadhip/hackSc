using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public float speed =3f ;
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
		if(transform.localPosition.x * isFacingLeft >0 )
		{
			moving = true;
			transform.localPosition -= new Vector3(Time.deltaTime*3 *isFacingLeft,0, 0);
		}
		else if(transform.localPosition.x * isFacingLeft<=0)
		{
			//set the controller
			moving = false;
		}
	}
}
