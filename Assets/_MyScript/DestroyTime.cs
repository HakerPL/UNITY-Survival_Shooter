using UnityEngine;
using System.Collections;

public class DestroyTime : MonoBehaviour 
{
	//CZAS PO JAKIM OBIEKT ZOSTANIE ZNISZCZONY
	public float timeDestroy ;

	// Use this for initialization
	void Start () 
	{
		//NISZCZYMY OBIEKT PO OKRESLONYM CZASIE
		Destroy( gameObject , timeDestroy ) ;
	}

}
