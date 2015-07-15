using UnityEngine;
using System.Collections;

public class ZeppelinCrafter : MonoBehaviour {

	// Fileds shown in the Inspector pane
	public int numZeppelin = 40;			// Number of clouds to generate

	public GameObject[] ZeppelinPrefabs;	// Holds the prefabs for the clouds

	public Vector3 ZeppelinPosMin;			// Minimum position for each cloud
	public Vector3 ZeppelinPosMax;			// Maximum position for each cloud

	public float ZeppelinScaleMin = 1.0f;	// Minimum scale for each cloud
	public float ZeppelinScaleMax = 7.0f;	// Maximum scale for each cloud

	public float ZeppelinSpeedMult = 0.5f; // Speed modifier for clouds

	// Fields set dynamically
	private GameObject[] ZeppelinInstances;

	void Awake() {
		// Make an array to hold all cloud instances
		ZeppelinInstances = new GameObject[numZeppelin];

		// Find the anchor parent
		GameObject anchor = GameObject.Find("ZeppelinAnchor");

		// Iterate through each array element and create a cloud
		GameObject Zeppelin;

		for(int i=0; i<ZeppelinInstances.Length; i++) {
			// Choose a prefab between 0 and cloudPrefabs.Length-1
			int prefabNum = Random.Range(0,ZeppelinPrefabs.Length);
			// Make an Instance
			Zeppelin = Instantiate(ZeppelinPrefabs[prefabNum]); // as GameObject;
			// Now position the cloud
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(ZeppelinPosMin.x, ZeppelinPosMax.x);
			cPos.y = Random.Range(ZeppelinPosMin.y, ZeppelinPosMax.y);
			Debug.Log ("positioned");
			// Randomly scale the cloud
			float scaleFactor = Random.value;
			float scaleValue = Mathf.Lerp(ZeppelinScaleMin, ZeppelinScaleMax, scaleFactor);
			Debug.Log ("scaled");
			// Smaller clouds with smaller scaleFactor should be nearer to the ground
			cPos.y = Mathf.Lerp(ZeppelinPosMin.y, cPos.y, scaleFactor);
			// Smaller clouds should be farther away
			cPos.z = 100 - 90 * scaleFactor;

			// Apply the transforms to the cloud
			Zeppelin.transform.position = cPos;
			Zeppelin.transform.localScale = Vector3.one * scaleValue;
			// Make the cloud a child of the anchor
			Zeppelin.transform.parent = anchor.transform;
			// Add the cloud to the cloudInstances
			ZeppelinInstances[i] = Zeppelin;
		}
	}

	void Update() {
		// Iterate over all generated clouds
		foreach(GameObject Zeppelin in ZeppelinInstances) {
			// Get cloud scale and position
			Vector3 cPos = Zeppelin.transform.position;
			float scaleValue = Zeppelin.transform.localScale.x;

			// Move lager clouds faster
			cPos.x -= scaleValue * Time.deltaTime * ZeppelinSpeedMult;

			// If cloud moved too far left, set it back to max
			if(cPos.x < ZeppelinPosMin.x){
				cPos.x = ZeppelinPosMax.x;
			}
			// Apply the new position to the cloud
			Zeppelin.transform.position = cPos;
		}
	
	}

}
