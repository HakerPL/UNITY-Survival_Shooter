using UnityEngine;
using System.Collections;

public class MoveUP : MonoBehaviour 
{
	public float speedMove = 5f ;


	Vector3 positionUP ;
	Vector3 positionDOWN ;

	Vector3 positionStart ;
	Vector3 positionStop ;

	// Use this for initialization
	void Start () 
	{
		positionUP = transform.position + new Vector3 ( 0f , 0.25f , 0f ) ;

		positionDOWN = transform.position + new Vector3 ( 0f , -0.25f , 0f ) ;

		positionStop = positionDOWN ;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Debug.Log ( "positionStop = " + positionStop.ToString() ) ;
		//Debug.Log ( " transform.position = " + transform.position.ToString() ) ;

		transform.position = Vector3.Lerp ( transform.position , positionStop , speedMove * Time.deltaTime ) ;

		if( transform.position == positionStop )
		{
			//Debug.Log("1") ;
			if( positionStop == positionUP )
			{
				//Debug.Log("2") ;
				positionStop = positionDOWN ;
			}
			else
			{
				//Debug.Log("3") ;
				positionStop = positionUP ;
			}
		}
	}
}
