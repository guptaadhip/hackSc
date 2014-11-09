using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	float timer_L = 0.0f;
	float timer_R = 0.0f;
	float timer_F = 0.0f;
	float timer_B = 0.0f;
	float gen_timer_L = 3.0f;
	float gen_timer_R = 3.0f;
	float gen_timer_F = 3.0f;
	float gen_timer_B = 3.0f;

	float randtimermax = 20.0f;


	int nextlevel = 10;
	int nextlevelmin = 8;
	int nextlevelmax = 10;

	int invnum = 0;

	public static bool isGameOver = false;

	public TextMesh gameStats;
	public static int leftCount =1;
	public static int rightCount =1;
	public static int score = 0;

	float testtimer = 0.0f;
	public int swap = 0;

	public GameObject[] carsTypes; 

	// Use this for initialization
	void Start () {
		gen_timer_L =  Random.Range(5,20)/10f;
		gen_timer_R =  Random.Range(5,20)/10f;

		gen_timer_F =  Random.Range(10,randtimermax)/5f;
		gen_timer_B =  Random.Range(10,randtimermax)/5f;
	}
	
	// Update is called once per frame
	void Update () {

		timer_L += Time.deltaTime;
		timer_R += Time.deltaTime;
		timer_F += Time.deltaTime;
		timer_B += Time.deltaTime;

		testtimer += Time.deltaTime;


		if (score == nextlevel) 
		{
			if( invnum == 0)
			{
				gameStats.text = "SWAP" ;
				gameStats.color = Color.magenta;
				gameStats.gameObject.SetActive(true);
				testtimer = 0;
				swap += 1;
				invnum = 1;

				nextlevelmin -= Random.Range(0,2);
				if(nextlevelmin<3) nextlevelmin = 3;
				nextlevelmax -= Random.Range(0,2);
				if(nextlevelmax<5) nextlevelmax = 5;
				if(nextlevelmin>nextlevelmax) nextlevelmax = nextlevelmin + 1;

				nextlevel += Random.Range(nextlevelmin,nextlevelmax);
			}
		}
		if(testtimer>2.0f)
		{
			gameStats.gameObject.SetActive(false);
			invnum = 0;
		}


			if (timer_L > gen_timer_L) {
				//gen car
				timer_L = 0f;
				gen_timer_L = Random.Range(15,45)/10f;

				CreatePrefab(0);
				leftCount++;

			}
			if (timer_R > gen_timer_R) {
				//gen car
				timer_R = 0f;
				gen_timer_R = Random.Range(15,45)/10f;
				
				CreatePrefab(1);
				
				rightCount++;

			}
			if (timer_F > gen_timer_F) {
				//gen car	
				timer_F = 0f;
				gen_timer_F = Random.Range(10,randtimermax)/5f;
				randtimermax -= Random.Range(0,5)/7f;
				if(randtimermax < 12.5f) randtimermax = 12.5f;
				CreatePrefab(2);

			}
			if (timer_B > gen_timer_B) {
				//gen car	
				timer_B = 0f;
				gen_timer_B = Random.Range(10,randtimermax)/5f;
				randtimermax -= Random.Range(0,5)/7f;
				if(randtimermax < 12.5f) randtimermax = 12.5f;
				CreatePrefab(3);
				
			}
		



		if(leftCount > 5 || rightCount > 5)
		{
			isGameOver = true;
		}

		if(isGameOver)
		{

		
			gameStats.text = "Game Over";
			gameStats.color = new Color(0.3f,0,0);
			gameStats.gameObject.SetActive(true);
		}

	}

	public void CreatePrefab(int type)
	{
		if(!isGameOver)
		{

			GameObject clone = Instantiate(carsTypes[type]) as GameObject;
			clone.name = carsTypes[type].name;

		}

	}
}
