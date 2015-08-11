using UnityEngine;
using System.Collections;

public class RandomRotateItem : MonoBehaviour 
{
	//ZMIENNA DO ROTACJI
	public float rotate ;
	
	// Use this for initialization
	void Update () 
	{
		transform.Rotate(Vector3.up , rotate * Time.deltaTime) ;
		transform.Rotate(Vector3.left , rotate * Time.deltaTime) ;
	}
	
}
