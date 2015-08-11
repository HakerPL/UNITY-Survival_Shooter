using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour 
{
	//CZAS MIEDZY KTORYM BEDZIE SIE POJAWIAC ITEM
	public Vector2 RespTime ;
	//POZYCJA SPAWN
	public Transform SpawnPosition ;
	//TABLICA OBIEKTOR
	public GameObject[] BonusItems ;



	//CZAS POTRZEBNY DO RESPAWNA
	float time = 0 ;
	//CZY LOSOWAC CZAS DO SPAWN
	bool RespawnTime ;
	//CZY WYSWIETLIC BONUS
	bool Spawn ;
	//OBIEKT KTORY STWORZYMY
	GameObject objectSpawn ;
	//CZAS JAKI ZOSTAL WYLOSOWANY
	float nextTimeRespawn ;

	//REFERENCJA DO PARTICLE 
	//BEDZIEMY JA WYLACZAC KIEDY BEDZIE ITEM A WLACZAC KIEDY GO NIE BEDZIE 
	//( ALA LADOWANIE )
	ParticleSystem partic ;


	void Awake()
	{
		//POBIERAMY KOMPONENTY
		partic = GetComponent<ParticleSystem>() ;
	
		//SpawnPosition.position = new Vector3 ( SpawnPosition.position.x , SpawnPosition.position.z , SpawnPosition.position.y ) ;
		//ZMIENNE LOSOWANIA I SPAWN USTAWIAMY NA TRUE
		Spawn = true ;
		RespawnTime = true ;
	}

	void FixedUpdate () 
	{
		//JESLI TRZEBA LOSOWAC ITEMA ORAZ CZAS
		if( RespawnTime )
		{
			RespawnTime = false ;
			//LOSUJEMY CZAS
			nextTimeRespawn = Random.Range( RespTime.x + 1 , RespTime.y + 1 ) ;
			//LOSUJEMY OBIEKT
			int index = Random.Range( 0 , BonusItems.Length ) ;
			objectSpawn = BonusItems[index] ;
		}

		//SPRAWDZAMY CZY TRZEBA WYSWIETLIC BONUS
		if ( Spawn )
		{
			//ZLICZAMY CZAS
			time += Time.deltaTime ;
			//SPRAWDZAMY CZY JUZ CZAS NA POJAWIENIE SIE OBIEKTU
			if ( time >= nextTimeRespawn )
			{
				//WYSWIETLAMY OBIEKT
				GameObject Child = Instantiate( objectSpawn , SpawnPosition.position , SpawnPosition.rotation ) as GameObject ;
				//USTAWIAMY INSTANCJE JAKO DZIECKO
				Child.transform.parent = gameObject.transform ;

				//SKORO OBIEKT SIE POJAWIL TO CZEKAMY AZ KTOS GO ZABIERZE
				Spawn = false ;
				//ZERUJEMY CZAS
				time = 0 ;
				//WYLACZAMY PARTICLE
				partic.enableEmission = false ;
			}
		}
	}

	public void StartRespawn()
	{
		//BONUS ZOSTAL ZABRANY TO USTAWIAMY ZMIENNE NA TRUE 
		Spawn = true ;
		RespawnTime = true ;
		//WLACZAMY PARTICLE
		partic.enableEmission = true ;
	}
}
