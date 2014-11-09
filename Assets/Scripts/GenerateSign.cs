using UnityEngine;
using System.Collections;

public class GenerateSign : MonoBehaviour {

	public Texture2D[] textures;
	bool isDirectionSet = false;
	Material material;
	CarMovement carMove;


	// Use this for initialization
	void Start () {

		carMove = transform.parent.GetComponent<CarMovement>();
	
		if(!isDirectionSet )
		{
			int i;
//			if(GameIntro.step==0)
//			{
//				i = 1;
//
//			}
//			else if(GameIntro.step==1)
//			{
//				i = 0;
//			}
//			else if(GameIntro.step==2)
//			{
//				i = 2;
//			}
//			else 
//			{
				i = Random.Range(1,1000)%3;
			//}
			SetDirection(i);
			isDirectionSet = true;
		}

	}

	public void SetDirection(int i)
	{
		renderer.material.mainTexture = textures[0];
		
		switch(i)
		{
		case 0: //left
			
			if(transform.parent.name.Contains("CarRight"))
			{
				
				renderer.material.mainTextureScale = new Vector2(-1, -1);
				
			}
			else
				renderer.material.mainTexture = textures[0];
			carMove.goingTo = 0;
			break;
		case 1: //right
			
			if(transform.parent.name.Contains("CarRight"))
			{
				renderer.material.mainTextureScale = new Vector2(-1, 1);
				renderer.material.mainTexture = textures[0];
			}
			else
				renderer.material.mainTexture = textures[1]; 
			carMove.goingTo = 1;
			
			break;
		case 2:
			if(transform.parent.name.Contains("CarRight"))
			{
				renderer.material.mainTextureScale = new Vector2(-1, -1);
				
			}
			renderer.material.mainTexture = textures[2];
			carMove.goingTo = 2;
			
			break;
		}
	}

	// Update is called once per frame
	void Update () {



	}
}
