using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	float timer_L = 0.0f;
	float timer_R = 0.0f;
	float gen_timer_L = 3.0f;
	float gen_timer_R = 3.0f;

	public GameObject[] carsTypes; 

	// Use this for initialization
	void Start () {
		gen_timer_L = 1.0f + Random.Range(0,45)/10f;
		gen_timer_R = 1.0f + Random.Range(0,45)/10f;
	}
	
	// Update is called once per frame
	void Update () {

		timer_L += Time.deltaTime;
		timer_R += Time.deltaTime;

		if (timer_L > gen_timer_L) {
			//gen car

			print(gen_timer_L);

			timer_L = 0f;
			gen_timer_L = 1.0f + Random.Range(0,45)/10f;

			CreatePrefab(0);





				}
		if (timer_R > gen_timer_R) {
			//gen car
			
			print(gen_timer_R);
			
			timer_R = 0f;
			gen_timer_R = 1.0f + Random.Range(0,45)/10f;
			
			CreatePrefab(1);
			
			
			
			
			
		}

	}

	void CreatePrefab(int type)
	{
		GameObject clone = Instantiate(carsTypes[type]) as GameObject;
	}
}
