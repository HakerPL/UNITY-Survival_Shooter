using UnityEngine;
using System.Collections;

public class AtackEnemy : MonoBehaviour 
{
	//CZAS MIEDZY ATAKAMI
	public float timeBetweenAtack = 1f ;
	//OBRAZENIA JAKIE ZADAJE GRACZOWI
	public int Damage = 10 ;


	//ANIMACJA ENEMY
	Animator animationEnemy ;
	//REFERENCJA NA OBIEKT GRACZA
	GameObject player ;
	//REFERENCJA NA SKRYPT GRACZA
	PlayerHealth playerHealth ;
	//REFERENCJA NA SKRYPT PRZECIWNIKA
	EnemyHealth enemyHealth ;
	
	//REFERENCJA DO RIGIDBODY
	Rigidbody rigidbodyEnemy ;

	//SPRAWDZAMY CZY GRACZ JEST W ZASIEGU ATAKU
	bool isInRange ;
	//OBECNY CZAS (POTRZEBNY DO SYNCHRONIZACJI ATAKU)
	float timerAtack ;


	void Awake()
	{
		//PRZYPISUJEMY KOMPONENTY I OBIEKTY DO ZMIENNYCH
		animationEnemy = GetComponent<Animator>() ;
		rigidbodyEnemy = GetComponent<Rigidbody>() ;
		enemyHealth = GetComponent<EnemyHealth>() ;

		player = GameObject.FindGameObjectWithTag("Player") ;
		playerHealth = player.GetComponent<PlayerHealth>() ;
	}


	void OnTriggerEnter( Collider other )
	{
		//SPRAWDZAMY CZY TO GRACZ JEST W ZASIEGU
		//other.gameObject PRZECHOWUJE OBIEKT I POZWALA POROWNAC Z INNYM
		if( other.gameObject == player )
		{
			//Debug.Log( "He is in range" ) ;
			isInRange = true ;
		}
	}

	void OnTriggerExit( Collider other )
	{
		//JESLI GRACZ OPUSCIL ZASIEG
		if( other.gameObject == player )
		{
			//Debug.Log( "He is not in range" ) ;
			isInRange = false ;
		}
	}

	void OnTriggerStay( Collider other )
	{
		//JESLI GRACZ JEST W POLU ATAKU I PRZECIWNIK ZYJE TO OBRACAMY SIE PRZODEM DO NIEGO
		if( other.gameObject == player && enemyHealth.CurrentEnemyHealth() > 0 )
		{
			//TWORZYMY VECTOR3 OD ENEMY DO POZYCJI GRACZA NA EKRANIE
			Vector3 EnemyToPlayer = player.transform.position - transform.position ;
			
			//VEKTOR MA BYC NA PODLODZE NIE W POWIETRZU
			EnemyToPlayer.y = 0f ;
			
			//USTAWIAMY ROTACJE NA PLAYER (OBRACA NA Z AXIS??)
			Quaternion newEnemyRotation = Quaternion.LookRotation ( EnemyToPlayer ) ;
			
			//USTAWIAMY ROTACJE ENEMY
			rigidbodyEnemy.MoveRotation ( newEnemyRotation ) ;
		}
	}


	// Update is called once per frame
	void Update () 
	{
		//USTAWIAMY CZAS (DO ATAKU)
		timerAtack += Time.deltaTime ;


		//SPRAWDZAMY CZY GRACZ JEST W ZASIEGU , CZY CZAS POZWALA NA ATAK I CZY ENEMY ZYJE
		if( timerAtack >= timeBetweenAtack && isInRange && enemyHealth.CurrentEnemyHealth() > 0 )
		{
			//Debug.Log( "Damage player" ) ;
			AtackPlayer() ;
		}


		//JESLI GRACZ JEST MARTWY (0 LUB MNIEJ ZYCIA) TO PRZECHODZIMY W STAN IDLE
		if( playerHealth.CurrentPlayerHealth() <= 0 )
		{
			animationEnemy.SetTrigger ("PlayerDead") ;
		}
	}


	void AtackPlayer()
	{
		//ZERUJEMY CZAS (POTRZEBNY DO ATAKU)
		timerAtack = 0f ;

		//SPRAWDZAMY CZY GRACZ ZYJE (JESLI NIE TO NIE ATAKUJEMY)
		if( playerHealth.CurrentPlayerHealth() > 0 )
		{
			//Debug.Log( "Send Damage to player" ) ;
			playerHealth.TakeDamage( Damage ) ;
		}
	}
}
