using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	//CZAS KIEDY MA SIE ZACZAC POJAWIANIE MOBKOW ORAZ CZAS MIEDZY POJAWIENIAMI
	public float StartSpawnTime = 2f ;
	public float SpawnTimeBewteen = 3f ;

	//GDZIE I CO MA SIE POJAWIC
	public Transform SpawnPoint ;
	public GameObject Type_of_Opponent ;


	//REFERENCJA NA ZYCIE GRACZA
	PlayerHealth playerHealth ;


	void Awake()
	{
		//SZUKAMY GRACZA
		playerHealth = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<PlayerHealth>() ;

		//SPRAWDZAMY CZY GRACZ ZOSTAL ZNALEZIONY I POBRANY OD NIEGO SKRYPT
		if( playerHealth == null )
		{
			Debug.Log( "SpawnManager - Player was not found." ) ;
		}
	}

	
	void Start () 
	{
		//MUSI BYC W START BO INACZEJ ZA KAZDYM RAZEM WLACZA FUNKCJE ( NA MAPIE JEST MILION MOBKOW :p )
		InvokeRepeating( "Spawn" , StartSpawnTime , SpawnTimeBewteen ) ;
	}

	void Spawn()
	{
		//SPRAWDZAMY CZY GRACZ ZYJE
		if( playerHealth.CurrentPlayerHealth() <= 0 ) 
		{
			return ;
		}
		else
		{
			Instantiate ( Type_of_Opponent , SpawnPoint.position , SpawnPoint.rotation ) ;
		}
	}
}
