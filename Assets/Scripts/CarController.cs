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
	}

	void OnGUI ()
	{
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
			if(carMoveScript.moving == false)
			{
				if(!isCarSet)
				{
					isCarSet = true;
					setCars();
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
		if (thalmicMyo.pose == Pose.WaveIn) //left
		{
			moveDirection = "left";
		}
		if (thalmicMyo.pose == Pose.WaveOut) //right
		{
			moveDirection = "right"; 
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
		if(moveDirection == "left")
		{
			timer+= Time.deltaTime*turnSpeed;
			if(timer<1f)
			{
				car.transform.localPosition = Vector3.Lerp(startPos, startPos+ new Vector3(-5, 0, -3) , timer);
				car.transform.localRotation = Quaternion.Slerp(startRot, startRot * new Quaternion(0,0.7f,0,0.7f) , timer);
			}
			else if(timer>=1f && timer<2f)
			{
				transform.localPosition -= new Vector3(0,0, timer);
			}
			else if(timer>2f)
			{
				moveDirection = "";
			}
		}
		else if(moveDirection == "right")
		{
			timer+= Time.deltaTime*turnSpeed;
			if(timer<1f)
			{
				car.transform.localPosition = Vector3.Lerp(startPos, startPos+ new Vector3(5, 0, -3) , timer);
				car.transform.localRotation = Quaternion.Slerp(startRot, startRot * new Quaternion(0,-0.7f,0,0.7f) , timer);
			}
			else if(timer>=1f && timer<2f)
			{
				transform.localPosition -= new Vector3(0,0, timer);
			}
			else if(timer>2f)
			{
				moveDirection = "";
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
		startPos = car.transform.localPosition;
		startRot = car.transform.localRotation;

	}
}