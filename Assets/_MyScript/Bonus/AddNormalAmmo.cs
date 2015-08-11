using UnityEngine;
using System.Collections;

public class AddNormalAmmo : MonoBehaviour 
{
	//ILE AMUNICJI DODAJE
	public int HowMuchAmmo = 30 ;

	//SKRYPT GRACZA
	private Inventory playerInventory ;
	
	//SKRYPT SPAWN DO ODNOWIENIA SPAWN
	private SpawnItem spawnItem ;
	
	//OBIEKT GRACZA
	GameObject PlayerComponent ;
	
	
	// Use this for initialization
	void Start () 
	{
		PlayerComponent = GameObject.FindWithTag("Player") ;
		//POBIERAMY OD RODZICA SKRYPT
		spawnItem = GetComponentInParent<SpawnItem>() ;
		//JESLI SZUKANIE SIE POWIODLO TO PRZYPISUJEMY COMPONENT DO OBIEKTU
		if( PlayerComponent != null )
		{
			playerInventory = PlayerComponent.GetComponent<Inventory>() ;
		}
		
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(playerInventory == null)
		{
			Debug.Log( " can't find Player Inventory ( AddNormalAmmo )" ) ;
		}
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{ 
			//DODAJEMY AMUNICJE
			playerInventory.AddAmmo( HowMuchAmmo ) ;
			
			//ODNAWIAMY SPAWN
			spawnItem.StartRespawn() ;
			//NISZCZYMY OBIEKT
			Destroy( gameObject ) ;
		}
	}
}