using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class HUDweapon : MonoBehaviour 
{
	//TEKST NABOI
	public Text ammoNormal ;
	public Text ammoShootgun ;


	//SKRYPT GRACZA
	Inventory playerInventory ;

	// Use this for initialization
	void Awake () 
	{
		GameObject player = GameObject.FindGameObjectWithTag( "Player" ) ;

		if( player != null )
		{
			playerInventory = player.GetComponent<Inventory>() ;
		}

		if( playerInventory == null )
		{
			Debug.Log( " playerInventory is empty ( HUDweapon ) " ) ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//AKTUALIZUJEMY WARTOSCI 
		ammoNormal.text = playerInventory.AmmoCurrent() + " / " + playerInventory.MaxAmmo() ;
		ammoShootgun.text = playerInventory.AmmoShootgunCurrent() + " / " + playerInventory.MaxAmmoShootgun() ;
	}
}
