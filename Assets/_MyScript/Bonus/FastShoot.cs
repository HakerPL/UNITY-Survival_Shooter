using UnityEngine;
using System.Collections;

public class FastShoot : MonoBehaviour 
{
	//ILE ZYCIA DODAJE BONUS
	public float TimeActivate = 10f ;
	
	//SKRYPT GRACZA
	private PlayerShoot player ;
	
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
			Debug.Log( "PlayerComponent is empty ( FastShoot )" ) ;
		}

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{ 
			//POBIERAMY SKRYPT OD GRACZA ( MOGLISMY ZMIENIC BRON )
			player = PlayerComponent.GetComponentInChildren<PlayerShoot>() ;

			//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
			if(player == null)
			{
				Debug.Log( " can't find Player PlayerShoot ( FastShoot )" ) ;
			}

			// player.StartCoroutine( "SpeedUP" , TimeActivate ) ;
			player.shootSpeedUP( TimeActivate ) ;
			
			//ODNAWIAMY SPAWN
			spawnItem.StartRespawn() ;
			//NISZCZYMY OBIEKT
			Destroy( gameObject ) ;
		}
	}
}