using UnityEngine;
using System.Collections;

public class FaceHealthToCamera : MonoBehaviour 
{
	//REFERENCJA DO KAMERY
	Camera MainCamera ;

	void Awake()
	{
		//PRZYPISUJEMY GLOWNA (MAIN) KAMERE
		MainCamera = Camera.main ;
	}

	// Update is called once per frame
	void Update () 
	{
		//LOOKAT POWODUJE ZE OBIEKT PATRZY NA INNY OBIEKT
		//POZYCJA OBIEKTU + POZYCJA KAMERY * (0 , 0 , -1) , ROTACJA KAMERY * (0 , 1 , 0)
		transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.back,
		                 MainCamera.transform.rotation * Vector3.up);
	}
}
