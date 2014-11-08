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
		moving = true;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.forward , out hit, 5f))
		{
			if(hit.transform.name.Contains(gameObject.transform.name))
			{
				if((hit.transform.position.x - transform.position.x)*isFacingLeft <1 )
				{

					moving = false;
				}

			}

		}
		
		if(transform.position.x *isFacingLeft >stopPoint && moving)
		{

			transform.position -= new Vector3(Time.deltaTime*3 *isFacingLeft*speed,0, 0);
		}
//		else if(transform.position.x *isFacingLeft<= stopPoint)
//		{
//			//set the controller
//
//			moving = false;
//		}
	}
}
