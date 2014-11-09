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
	public GameObject PlayerName;

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

		if(PlayerName == null)
		{
			PlayerName = GameObject.Find("Player");
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
			PlayerName.SetActive(true);
			PlayerName.GetComponent<TextMesh>().text = "PLAYER 1";
		}
		else if (step == 2)
		{
			rules[0].transform.position += new Vector3 (Time.deltaTime*15f, 0,0);
			PlayerName.GetComponent<TextMesh>().text = "PLAYER 2";
		}
		else if (step == 3)
		{
			rules[1].transform.position -= new Vector3 (Time.deltaTime*15f, 0,0);
			PlayerName.GetComponent<TextMesh>().text = "BOTH";
		}

		if(step == 5)
		{
			rules[2].transform.position += new Vector3 (0, 0,Time.deltaTime*25f);
			PlayerName.SetActive(false);
			GetComponent<TextMesh>().text = "Game Start!!";
			transform.position = startPos;
			timer += Time.deltaTime;

			if(timer>1f)
			{
				step =3;
				transform.position = endPos;
				rules[0].SetActive(false);
				rules[1].SetActive(false);
				rules[2].SetActive(false);
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
		if (( thalmicMyo1.pose == Pose.WaveIn) && GameIntro.step == 2 ) //left
		{
			GameIntro.step++;

			rules[2].SetActive(true);
			
		}
		if ((thalmicMyo2.pose == Pose.WaveOut  ) && GameIntro.step == 1) //right
		{

			GameIntro.step++;

			rules[1].SetActive(true);
		}

		if ((thalmicMyo1.pose == Pose.Fist || thalmicMyo2.pose == Pose.Fist )&& GameIntro.step == 3) //go straight
		{





			GameIntro.step=5;

		}
	}

}
