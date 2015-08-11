using UnityEngine;
using System.Collections;

public class TurnOnWindowInOptions : MonoBehaviour 
{
	//OBIEKT KTORY MAMY WLACZYC
	public GameObject turnOnWindow ;

	public void TunrON()
	{
		turnOnWindow.SetActive( true ) ;
	}
}
