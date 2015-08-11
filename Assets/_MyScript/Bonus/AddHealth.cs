using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour 
{
	//ILE ZYCIA DODAJE BONUS
	public int NumberHealth = 20 ;

	//SKRYPT GRACZA
	private PlayerHealth player ;

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
			player = PlayerComponent.GetComponent<PlayerHealth>() ;
		}
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(player == null)
		{
			Debug.Log( " can't find Player PlayerHealth " ) ;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//SPRAWDZAMY CZY GRACZ MA MAX ZYCIA ( JESLI TAK TO NIE MA CO BRAC ZYCIA )
			if( player.CurrentPlayerHealth() < player.playerHealth )
			{
				//DODAJEMY ZYCIE
				player.AddHealth( NumberHealth ) ;

				//ODNAWIAMY SPAWN
				spawnItem.StartRespawn() ;
				//NISZCZYMY OBIEKT
				Destroy( gameObject ) ;
			}
		}
	}
}
