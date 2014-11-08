using UnityEngine;
using System.Collections;

public class GenerateSign : MonoBehaviour {

	//public Texture2D textures;
	bool isDirectionSet = false;
	Material material;

	// Use this for initialization
	void Start () {
	
		if(!isDirectionSet)
		{
			int i = Random.Range(1,1000)%3;
			switch(i)
			{
			case 1:
				renderer.material.mainTextureScale = new Vector2(-1, -1);
				break;
			case 2:
				renderer.material.mainTextureScale = new Vector2(1, 1);
				break;
			case 3:
				if(transform.parent.name == "CarLeft")
					renderer.material.mainTextureScale = new Vector2(-1, 1);
				else
					renderer.material.mainTextureScale = new Vector2(1, -1);
				break;
			}
			isDirectionSet = true;
		}
	}
	
	// Update is called once per frame
	void Update () {



	}
}
