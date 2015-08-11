using UnityEngine;
using System.Collections;

public class SpeedUP : MonoBehaviour 
{
	//ILE ZYCIA DODAJE BONUS
	public float TimeActivate = 10f ;
	
	//SKRYPT GRACZA
	private PlayerMove player ;
	
	//SKRYPT SPAWN DO ODNOWIENIA SPAWN
	private SpawnItem spawnItem ;
	
	// Use this for initialization
	void Start () 
	{
		GameObject PlayerComponent = GameObject.FindWithTag("Player") ;
		//POBIERAMY OD RODZICA SKRYPT
		spawnItem = GetComponentInParent<SpawnItem>() ;
		//JESLI SZUKANIE SIE POWIODLO TO PRZYPISUJEMY COMPONENT DO OBIEKTU
		if( PlayerComponent != null )
		{
			player = PlayerComponent.GetComponent<PlayerMove>() ;
		}
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(player == null)
		{
			Debug.Log( " can't find Player PlayerMove " ) ;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{ 
			//URUCHAMIAMY CORUPTINE OD PLAYER
			// player.StartCoroutine( "SpeedUP" , TimeActivate ) ;
			player.SpeedUP( TimeActivate ) ;
			
			//ODNAWIAMY SPAWN
			spawnItem.StartRespawn() ;
			//NISZCZYMY OBIEKT
			Destroy( gameObject ) ;
		}
	}
}
