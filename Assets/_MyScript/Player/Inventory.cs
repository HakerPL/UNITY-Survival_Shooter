using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	//AMUNICJA ZWYKLA
	int CurrentAmmo ;
	//AMUNICJA SHOTGUN 
	int CurrentAmmoShootgun ;


	//MAX AMUNICJI JAKI MOZEMY NOSIC
	int maxAmmo = 300 ;
	int maxAmmoShootgun = 80 ;

	// Use this for initialization
	void Start () 
	{
		//USTAWIAMY OBECNA ILOSC AMUNICJI
		CurrentAmmo = 100 ;
		CurrentAmmoShootgun = 20 ;
	}

	public void AddAmmo ( int plusAmmo )
	{
		CurrentAmmo += plusAmmo ;

		if( CurrentAmmo > maxAmmo )
			CurrentAmmo = maxAmmo ;
	}

	public void AddAmmoShootgun ( int plusAmmo )
	{
		CurrentAmmoShootgun += plusAmmo ;

		if( CurrentAmmoShootgun > maxAmmoShootgun )
			CurrentAmmoShootgun = maxAmmoShootgun ;
	}

	//FUNKCJE ZWRACAJACE AKTUALNA ILOSC AMUNICJI ORAZ JEJ MAX

	public int AmmoCurrent()
	{
		return CurrentAmmo ;
	}

	public int MaxAmmo()
	{
		return maxAmmo ;
	}

	public int AmmoShootgunCurrent()
	{
		return CurrentAmmoShootgun ;
	}

	public int MaxAmmoShootgun()
	{
		return maxAmmoShootgun ;
	}


	public void shootNormal()
	{
		CurrentAmmo-- ;
	}

	public void shootShootgun()
	{
		CurrentAmmoShootgun-- ;
	}


}
