using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class TestMovement : MonoBehaviour {
	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;
	void OnGUI ()
	{
		GUI.skin.label.fontSize = 18;
		
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
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		if (thalmicMyo.pose == _lastPose) {
			return;
		}
		_lastPose = thalmicMyo.pose;
		if (thalmicMyo.pose == Pose.WaveIn)
		{
			Vector3 position = this.transform.position;
			position.x--;
			this.transform.position = position;
		}
		if (thalmicMyo.pose == Pose.WaveOut)
		{
			Vector3 position = this.transform.position;
			position.x++;
			this.transform.position = position;
		}
		if (thalmicMyo.pose == Pose.FingersSpread)
		{
			Vector3 position = this.transform.position;
			position.y++;
			this.transform.position = position;
		}
		if (thalmicMyo.pose == Pose.Fist)
		{
			Vector3 position = this.transform.position;
			position.y--;
			this.transform.position = position;
		}
	}
}