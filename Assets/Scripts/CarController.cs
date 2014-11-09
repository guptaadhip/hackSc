using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose; 

public class CarController : MonoBehaviour {



	float timer =0f;
	Vector3 startPos;
	Quaternion startRot;
	string moveDirection = "";
	CarMovement carMoveScript;
	bool isCarSet = false, instructed = false;

	public float turnSpeed =3f;
	public GameObject car;
	public GameObject myo = null;


	private Pose _lastPose = Pose.Unknown;

	void Start()
	{
		if(car!=null)
		{
			carMoveScript = car.GetComponent<CarMovement>();
		}
		if(myo == null)
		{
			if(transform.name == "CarRight")
				myo = GameObject.Find("Myo2");
			else if(transform.name == "CarLeft")
				myo = GameObject.Find("Myo1");
		}
	}

	void OnGUI ()
	{
		if(myo == null)
		{

			if(this.name == "CarRight(Clone)")
				myo = GameObject.Find("Myo2");
			else if(this.name == "CarLeft(Clone)")
				myo = GameObject.Find("Myo1");
		}

		GUI.skin.label.fontSize = 12;
		GUI.color = Color.black;

		ThalmicHub hub = ThalmicHub.instance;
		
		// Access the ThalmicMyo script attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		if (!hub.hubInitialized) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "Cannot contact Myo Connect. Is Myo Connect running?\n" +
			          "Press Q to try again."
			          );
		} else if (!thalmicMyo.isPaired) {
			GUI.Label(new Rect (12, 12, Screen.width, Screen.height),
			          "No Myo currently paired."
			          );
		} else if (!thalmicMyo.armRecognized) {
			GUI.Label(new Rect (12, 16, Screen.width, Screen.height),
			          "Perform the Sync Gesture."
			          );
		} else {

			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect ( Screen.width -110, 8, Screen.width, Screen.height),
			           "Score: " + GameController.score
			           );
		}

	}
	
	void Update ()
	{
		if(car!=null)
		{


			if(car.transform.position.x *carMoveScript.isFacingLeft<= carMoveScript.stopPoint)
			{
				if(!isCarSet)
				{ 
					setCars();
					isCarSet = true;

				}

				if(!instructed)
				{	
					DetectInput();
					DetectKeyboard();
				}
					
					MoveCars();
				
			}
		}
	}


	void DetectInput()
	{

		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		if (thalmicMyo.pose == _lastPose) {
			return;
		}
		_lastPose = thalmicMyo.pose;
//		if (thalmicMyo.pose != Pose.Unknown) {
//			instructed = true;
//		}
		if (thalmicMyo.pose == Pose.WaveIn /*|| Input.GetKeyDown(KeyCode.LeftArrow)*/ ) //left
		{
			//carMoveScript.moving = true;
			instructed = true;
			moveDirection = "left";
			if((thalmicMyo.arm.ToString().Equals("Left") && car.name.Equals("CarLeft")) || 
			   (thalmicMyo.arm.ToString().Equals("Right") && car.name.ToString().Equals("CarRight"))) {
				moveDirection = "right";
				/* lets score */
				if (carMoveScript.goingTo == 1) {
					GameController.score++;
				}
			} 
			else 
			{
				/* lets score */
				if (carMoveScript.goingTo == 0) {
					GameController.score++;
				}
			}
		
		}
		if (thalmicMyo.pose == Pose.WaveOut /* || Input.GetKeyDown(KeyCode.RightArrow)*/ ) //right
		{
			//carMoveScript.moving = true;
			instructed = true;
			moveDirection = "right"; 
			if((thalmicMyo.arm.ToString().Equals("Left") && car.name.Equals("CarLeft")) || 
			   (thalmicMyo.arm.ToString().Equals("Right") && car.name.ToString().Equals("CarRight")) ) {
				moveDirection = "left";
				/* lets score */
				if (carMoveScript.goingTo == 0) {
					GameController.score++;
				}
			}
			else 
			{
				/* lets score */
				if (carMoveScript.goingTo == 1) {
					GameController.score++;
				}
			}

		}
		if (thalmicMyo.pose == Pose.FingersSpread) //stop
		{
			//carMoveScript.moving = false;
			moveDirection = "stop";
		}
		if (thalmicMyo.pose == Pose.Fist) //go straight
		{
			//carMoveScript.moving = true;
			instructed = true;
			/* lets score */
			if (carMoveScript.goingTo == 2) {
				GameController.score++;
			}
			moveDirection = "forward";
		}
	}

	void DetectKeyboard()
	{
		if (carMoveScript.isFacingLeft != -1) {
			if ( Input.GetKeyDown (KeyCode.O)) { //left
				
				moveDirection = "left";
				instructed = true;
			}
			if ( Input.GetKeyDown (KeyCode.M)) { //right
				
				moveDirection = "right"; 
				instructed = true;
			}
		}
		else {
			if ( Input.GetKeyDown (KeyCode.W)) { //left
				
				moveDirection = "left";
				instructed = true;
			}
			if ( Input.GetKeyDown (KeyCode.X)) { //right
				
				moveDirection = "right"; 
				instructed = true;
			}
		}
	}

	void MoveCars()
	{
		if(moveDirection == "left" )
		{

			timer+= Time.deltaTime*turnSpeed;
			if(timer<1f)
			{
				if(car.name.Equals("CarLeft"))
					car.transform.position = Vector3.Lerp(startPos, startPos+ new Vector3(-15*carMoveScript.isFacingLeft, 0, 5) , timer);
				else if(car.name.Equals("CarRight"))
					car.transform.position = Vector3.Lerp(startPos, startPos+ new Vector3(-10*carMoveScript.isFacingLeft, 0, 5) , timer);

				car.transform.rotation = Quaternion.Slerp(startRot, startRot * new Quaternion(0,0.7f*carMoveScript.isFacingLeft,0,0.7f) , timer);
			}
			else if(timer>=1f && timer<3f)
			{
				transform.position += new Vector3(0,0, timer*0.5f);
			}
			else if(timer>3f)
			{
				moveDirection = "";
				if(carMoveScript.isFacingLeft == 1)
				{
					GameController.leftCount--;
				}
				else
				{
					GameController.rightCount--;
				}
				Destroy(gameObject);

			}
		}
		else if(moveDirection == "right")
		{
			timer+= Time.deltaTime*turnSpeed;

			if(timer<1f)
			{
				if(car.name.Equals("CarLeft"))
					car.transform.position = Vector3.Lerp(startPos, startPos+ new Vector3(-10*carMoveScript.isFacingLeft, 0, -5) , timer);
				else if(car.name.Equals("CarRight"))
					car.transform.position = Vector3.Lerp(startPos, startPos+ new Vector3(-14*carMoveScript.isFacingLeft, 0, -5) , timer);


				car.transform.rotation = Quaternion.Slerp(startRot, startRot * new Quaternion(0,-0.7f*carMoveScript.isFacingLeft,0,0.7f) , timer);

			}
			else if(timer>=1f && timer<3f)
			{
				transform.position += new Vector3(0,0, -timer*0.5f);
			}
			else if(timer>=3f)
			{
				moveDirection = "";
				if(carMoveScript.isFacingLeft == 1)
				{
					GameController.leftCount--;
				}
				else
				{
					GameController.rightCount--;
				}
				Destroy(gameObject);
			}
		}
		else if(moveDirection == "stop")
		{

			print ("stops");
		}
		else if(moveDirection == "forward")
		{

			timer+= Time.deltaTime*turnSpeed;
			if(timer <3f)
			{
				transform.position += new Vector3(-timer*carMoveScript.isFacingLeft,0, 0);
			} 
			else  if(timer >= 3f)
			{
				moveDirection = "";
				if(carMoveScript.isFacingLeft == 1)
				{
					GameController.leftCount--;
				}
				else
				{
					GameController.rightCount--;
				}
				Destroy(gameObject);
			}

			
		}
	}

	void setCars()
	{
		timer = 0f;
		moveDirection = "";
		startPos = car.transform.position;
		startRot = car.transform.rotation;

	}

	void OnCollisionEnter(Collision collider)
	{

		if(collider.gameObject.name.Contains("Car"))
		{
			//audio.Play();
			GameController.isGameOver = true;
		}
	}
}