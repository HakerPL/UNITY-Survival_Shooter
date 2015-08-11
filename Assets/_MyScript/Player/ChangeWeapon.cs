using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ChangeWeapon : MonoBehaviour 
{
	//KONIEC LUFY
	public GameObject EndOfGun ;

	//ZAZNACZENIE
	public GameObject useNormalGun ;
	public GameObject useShootgun ;

	//ZAZNACZENIE AKTUALNEJ BRONI
	public GameObject normalWeaponImage ;
	public GameObject shootgunWeaponImage ;
	

	//OBECNA BRON KTORA TRZEBA SKASOWAC
	GameObject oldWeapon ;

	//KTORA BRON JEST AKTUALNIE UZYTA
	bool normalWeapon = true ;
	bool Shootgun = false ;

	void Awake()
	{
		//TWORZYMY INSTANCJE BRONI
		oldWeapon = Instantiate( useNormalGun , EndOfGun.transform.position , EndOfGun.transform.rotation ) as GameObject ;
		//USTAWIAMY INSTANCJE JAKO DZIECKO
		oldWeapon.transform.parent = EndOfGun.transform ;
	}


	// Update is called once per frame
	void Update () 
	{
		//WYBOR BRONI

		if( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			//JESLI AKTUALNIE NIE UZYWAMY TEJ BRONI
			if( !normalWeapon )
			{
				//USTAWIAMY ZAZNACZENIE
				normalWeaponImage.SetActive( true ) ;
				shootgunWeaponImage.SetActive( false ) ;

				//ZMIENIAMY NA TRUE BO AKTUALNIE UZYWAMY TEJ BRONI
				normalWeapon = true ;

				//ZMIENIAMY NA FALSE BO JEJ NIE UZYWAMY
				Shootgun = false ;

				//NISZCZYMY STARY OBIEKT ( BRON )
				Destroy( oldWeapon ) ;

				//TWORZYMY INSTANCJE BRONI
				oldWeapon = Instantiate( useNormalGun , EndOfGun.transform.position , EndOfGun.transform.rotation ) as GameObject ;
				//USTAWIAMY INSTANCJE JAKO DZIECKO
				oldWeapon.transform.parent = EndOfGun.transform ;
			}
		}

		if( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			//JESLI AKTUALNIE NIE UZYWAMY TEJ BRONI
			if( !Shootgun )
			{
				//USTAWIAMY ZAZNACZENIE
				normalWeaponImage.SetActive( false ) ;
				shootgunWeaponImage.SetActive( true ) ;

				//ZMIENIAMY NA TRUE BO AKTUALNIE UZYWAMY TEJ BRONI
				Shootgun = true ;
				
				//ZMIENIAMY NA FALSE BO JEJ NIE UZYWAMY
				normalWeapon = false ;
				
				//NISZCZYMY STARY OBIEKT ( BRON )
				Destroy( oldWeapon ) ;
				
				//TWORZYMY INSTANCJE BRONI
				oldWeapon = Instantiate( useShootgun , EndOfGun.transform.position , EndOfGun.transform.rotation ) as GameObject ;
				//USTAWIAMY INSTANCJE JAKO DZIECKO
				oldWeapon.transform.parent = EndOfGun.transform ;
			}
		}
	}
}
