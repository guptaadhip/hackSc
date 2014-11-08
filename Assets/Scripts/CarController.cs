using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose; 

public class CarController : MonoBehaviour {



	float timer =0f;
	Vector3 startPos;
	Quaternion startRot;
	string moveDirection = "";
	CarMovement carMoveScript;
	bool isCarSet = false;

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
		
		ThalmicHub hub = ThalmicHub.instance;
		
		// Access the ThalmicMyo script attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		if (!hub.hubInitialized) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "Cannot contact Myo Connect. Is Myo Connect running?\n" +
			          "Press Q to try again."
			          );
		} else if (!thalmicMyo.isPaired) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "No Myo currently paired."
			          );
		} else if (!thalmicMyo.armRecognized) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "Perform the Sync Gesture."
			          );
		} else {
			GUI.Label (new Rect (12, 8, Screen.width, Screen.height),
			           "Score: 0"
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
				DetectInput();
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
		if (thalmicMyo.pose == Pose.WaveIn /*|| Input.GetKeyDown(KeyCode.LeftArrow)*/ ) //left
		{

			moveDirection = "left";
			if((thalmicMyo.arm.ToString().Equals("Left") && car.name.Equals("CarLeft")) || 
			   (thalmicMyo.arm.ToString().Equals("Right") && car.name.ToString().Equals("CarRight"))) {
				print("1");
				moveDirection = "right";
			}

		}
		if (thalmicMyo.pose == Pose.WaveOut /* || Input.GetKeyDown(KeyCode.RightArrow)*/ ) //right
		{

			moveDirection = "right"; 
			if((thalmicMyo.arm.ToString().Equals("Left") && car.name.Equals("CarLeft")) || 
			   (thalmicMyo.arm.ToString().Equals("Right") && car.name.ToString().Equals("CarRight")) ) {
				print("2");
				moveDirection = "left";
			}
		}
		if (thalmicMyo.pose == Pose.FingersSpread) //stop
		{
			moveDirection = "stop";
		}
		if (thalmicMyo.pose == Pose.Fist) //go straight
		{
			moveDirection = "forward";
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
				transform.position += new Vector3(0,0, timer);
			}
			else if(timer>3f)
			{
				moveDirection = "";
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
				transform.position += new Vector3(0,0, -timer);
			}
			else if(timer>=3f)
			{
				moveDirection = "";
				Destroy(gameObject);
			}
		}
		else if(moveDirection == "stop")
		{

		}
		else if(moveDirection == "forward")
		{
			
		}
	}

	void setCars()
	{
		timer = 0f;
		moveDirection = "";
		startPos = car.transform.position;
		startRot = car.transform.rotation;

	}
}