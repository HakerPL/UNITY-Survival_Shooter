using UnityEngine;
using System.Collections;

public class CameraMoveOnPlayer : MonoBehaviour 
{
	//POBIERAMY POZYCJE OBIEKTU ZA KTORYM MA PORUSZAC SIE KAMERA
	public Transform TargetPosition ;
	//SZYBKOSC PORUSZANIA SIE KAMERY
	public float speedMoveCamera = 5f ;

	//DYSTANS MIEDZY KAMERA A CELEM
	Vector3 offset ;


	// Use this for initialization
	void Start () 
	{
		//POBIERAMY ROZNICE MIEDZY CELEM A KAMERA 
		//(ZEBY ZAWSZE MIEC TAKA SAMA ODLEGLOSC)
		offset = transform.position - TargetPosition.position ;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//USTALAMY NOWA POZYCJE DLA KAMERY
		Vector3 newPositionCammera = TargetPosition.position + offset ;
	
		//USTAWIAMY NOWA POZYCJE KAMERY (LERP PLYNNE PRZSEJSCIE Z
		//JEDNEGO PUNKTU DO DRUGIEGO)
		transform.position = Vector3.Lerp( transform.position , 
		                                   newPositionCammera , 
		                                   speedMoveCamera ) ;
	}	
}
