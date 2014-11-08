using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public float speed =3f ;

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
		if(transform.localPosition.z>0 )
		{
			moving = true;
			transform.localPosition -= new Vector3(0,0, Time.deltaTime*3);
		}
		else if(transform.localPosition.z<=0)
		{
			//set the controller
			moving = false;
		}
	}
}
