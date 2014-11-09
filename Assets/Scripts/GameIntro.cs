using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose; 

public class GameIntro : MonoBehaviour {

	float timer = 0;
	Vector3 startPos, endPos;
	public GameObject[] rules;
	public GameObject myo1 = null;
	public GameObject myo2 = null;
	public static int step =0;

	bool timerOn = true;
	//public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = new Vector3( transform.position.x,  transform.position.y +25f,  transform.position.z);

		if(myo2 == null)
		{
				myo2 = GameObject.Find("Myo2");
		}
		if(myo1 == null)
		{
			myo1 = GameObject.Find("Myo1");
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(timerOn)
			timer += Time.deltaTime;
		if(timer>2f && timer<3f)
		{
			transform.position = Vector3.Lerp(startPos, endPos, timer*0.5f);
		}

		if(timer>3f && timerOn)
		{
			timer =0;
			step =1;
			timerOn =false;
		}

		if(step == 1)
		{
			rules[0].SetActive(true);

		}

		if(step == 5)
		{
			GetComponent<TextMesh>().text = "Game Start!!";
			transform.position = startPos;
			timer += Time.deltaTime;

			if(timer>1f)
			{
				step =3;
				transform.position = endPos;
			}
		}

		DetectInput();
	}

	

	
	
	void DetectInput()
	{

		ThalmicMyo thalmicMyo1 = myo1.GetComponent<ThalmicMyo> ();
		ThalmicMyo thalmicMyo2 = myo2.GetComponent<ThalmicMyo> ();


		//		if (thalmicMyo.pose != Pose.Unknown) {
		//			instructed = true;
		//		}
		if ((thalmicMyo1.pose == Pose.WaveIn || thalmicMyo2.pose == Pose.WaveIn) && GameIntro.step == 2 ) //left
		{
			GameIntro.step++;
			rules[1].SetActive(false);
			rules[2].SetActive(true);
			
		}
		if ((thalmicMyo1.pose == Pose.WaveOut ||thalmicMyo2.pose == Pose.WaveOut ) && GameIntro.step == 1) //right
		{

			GameIntro.step++;
			rules[0].SetActive(false);
			rules[1].SetActive(true);
		}

		if ((thalmicMyo1.pose == Pose.Fist || thalmicMyo2.pose == Pose.Fist )&& GameIntro.step == 3) //go straight
		{

			rules[2].SetActive(false);



			GameIntro.step=5;

		}
	}

}
