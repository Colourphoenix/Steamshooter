using UnityEngine;
using System.Collections;

public class cloudcrafter : MonoBehaviour {


	//inspector fields
	public int numClouds = 40;

	public Vector3 cloudPosMin;
	public Vector3 cloudPosMax;

	public float cloudScaleMin = 1.0f;
	public float cloudScaleMax = 5.0f;

	public float cloudSpeedMult = 0.5f;
	public GameObject[] cloudPrefabs;

	//internal fields
	private GameObject[] cloudInstances;



	void Awake(){
		//create an array large enough to store all cloud intances
		cloudInstances = new GameObject [numClouds];
		//find the cloud anker in the hierarchy(gameobject.find)
		GameObject anchor = GameObject.Find ("ZeppelinAnchor");
		//iterate through array and create a cloud for each slot
		GameObject cloud;

		for (int i = 0; i<cloudInstances.Length; i ++) {
			//randomly pick one of the cloud prefabs
			int prefabNum = Random.Range(0, cloudPrefabs.Length);
			//create that instance
			cloud = Instantiate (cloudPrefabs[prefabNum]);
			//print("created");
			//position and scale the cloud (randomly)
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
			cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
			//print ("positioned");

			float scaleU = Random.value; 
			float scaleValue = Mathf.Lerp (cloudScaleMin, cloudScaleMax, scaleU);
			//print ("scaled");

			cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.x, scaleU);  //kleine wolken tiefer
			cPos.z = 100-90*scaleU; //entfernung von der Kamera

			//apply the changes to our instance
			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one * scaleValue;

			//make the cloud a child of the cloudanchor
			cloud.transform.parent = anchor.transform;
			//Put the cloud into our instances array
			cloudInstances [i] = cloud;
		}

	}

	void Update (){
		//Iterate over all cloud objects in the background

		foreach (GameObject cloud in cloudInstances){
			//Get Position and scale 
			float scaleVal= cloud.transform.localScale.x;
			Vector3 cPos= cloud.transform.position;
			//Move larger clouds faster
			cPos.x -= Time.deltaTime *cloudSpeedMult *scaleVal;

			if (cPos.x < cloudPosMin.x){

				cPos.x = cloudPosMax.x;
			}

			cloud.transform.position = cPos;

		}

	}
}

//paralax scrolling für background: Alienspiel bei Unity examples checken!! 

















//homework: update function, die die wolken bewegt
