using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	float timer_L = 0.0f;
	float timer_R = 0.0f;
	float gen_timer_L = 3.0f;
	float gen_timer_R = 3.0f;
	public static bool isGameOver = false;

	public TextMesh gameStats;
	public static int leftCount =1;
	public static int rightCount =1;
	public static int score = 0;


	public GameObject[] carsTypes; 

	// Use this for initialization
	void Start () {
		gen_timer_L =  Random.Range(5,20)/10f;
		gen_timer_R =  Random.Range(5,20)/10f;
	}
	
	// Update is called once per frame
	void Update () {

		timer_L += Time.deltaTime;
		timer_R += Time.deltaTime;

		if (timer_L > gen_timer_L) {
			//gen car



			timer_L = 0f;
			gen_timer_L = 1.0f + Random.Range(0,45)/10f;

			CreatePrefab(0);


			leftCount++;



				}
		if (timer_R > gen_timer_R) {
			//gen car
			

			
			timer_R = 0f;
			gen_timer_R = 1.0f + Random.Range(0,45)/10f;
			
			CreatePrefab(1);
			
			rightCount++;
			
			
			
		}

		if(leftCount > 5 || rightCount > 5)
		{
			isGameOver = true;
		}

		if(isGameOver)
		{
			gameStats.text = "Game Over\n Score: " + score;
			gameStats.gameObject.SetActive(true);
		}

	}

	void CreatePrefab(int type)
	{
		if(!isGameOver)
		{
			GameObject clone = Instantiate(carsTypes[type]) as GameObject;
			clone.name = carsTypes[type].name;
		}

	}
}
