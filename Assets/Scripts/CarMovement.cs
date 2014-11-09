using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	float speed =9f;
	public float stopPoint ;
	public int isFacingLeft = 1;
	/* Going To: -1 -> Not Initialized, 0 -> Left, 1-> Right, 2-> Straight */
	public int goingTo = -1;

	//public CarController controller;

	public bool moving = true;

	// Use this for initialization
	void Start () {
		speed = Random.Range(10, 20);

	}

	// Update is called once per frame
	void Update () {
	

		Move();
	}

	void Move()
	{
		//moving = true;
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
			else
			{
				moving = true;
			}

		}
		
//		if(transform.position.x *isFacingLeft >stopPoint && moving)
//		{
		if(moving)
			transform.position -= new Vector3(Time.deltaTime *isFacingLeft*speed,0, 0);
//		}
//		else if(transform.position.x *isFacingLeft<= stopPoint)
//		{
//			//set the controller
//
//			moving = false;
//		}
	}
}
