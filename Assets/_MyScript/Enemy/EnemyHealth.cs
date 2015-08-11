using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
	//ILE ZYCIA BEDZIE MIAL ENEMY
	public int enemyHealth = 100 ;
	//SZYBKOSC ZNIKNIECIA POD PODLOGE
	public float fastGoDown = 2f ;
	//ILOSC PUNKTOW ZA STWORA
	public int scoreValue = 10 ;
	//SKALA ZYCIA SLIDER
	public Slider healthBar ;
	//DZWIEK KIEDY ENEMY UMIERA
	public AudioClip enemyDeath ;
	
	

	//ANIMACJA POSTACI
	Animator animationEnemy ;
	//DZWIEK JAKI "WYDOBYWA" SIE Z ENEMY (OBECNIE)
	AudioSource enemyAudio ;
	//REFERENCJA DO POKAZANIA GDZIE TRAFIL GRACZ
	ParticleSystem ParticleHit ;
	//POTRZEBNA ZEBY WYLACZYC FIZYKE (KAZDY BEDZIE PRZECHODZIC PRZEZ OBIEKT)
	CapsuleCollider capsuleCollider ;

	
	
	//OBECNE ZYCIE GRACZA
	int currentHealthEnemy ;
	
	
	//ZMIENNE BOOL DO SMIERCI LUB DAMAGE GRACZA
	bool EnemyDead ;
	bool isGoDown ;
	
	void Awake()
	{
		//PRZYPISUJEMY KOMPONENTY ZMIENNYM
		animationEnemy = GetComponent<Animator>() ;
		enemyAudio = GetComponent<AudioSource>() ;
		ParticleHit = GetComponentInChildren<ParticleSystem>() ;
		capsuleCollider = GetComponent<CapsuleCollider>() ;
		
		//USTAWIAMY POCZATKOWE ZYCIE GRACZA
		currentHealthEnemy = enemyHealth ;
		
		//USTAWIAMY MAXYMALNA WIELKOSC HealthBar
		healthBar.maxValue = enemyHealth ;
		healthBar.value = enemyHealth ;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		//JESLI ENEMY NIE ZYJE I MA ZNIKNAC POD PODLOGA
		if( isGoDown )
		{
			//PRZESUWAMY PRZECIWNIKIEM POD PODLOGE
			transform.Translate ( -Vector3.up * fastGoDown * Time.deltaTime ) ;
		}

	}
	
	
	//FUNKCJA UZYWANA PRZEZ INNE OBIEKTY ZEBY ZADAC OBRARZENIA GRACZOWI
	public void TakeDamage( int HowMuch , Vector3 Where ) 
	{
		//JESLI PRZECIWNIK NIE ZYJE TO NIE MA CO SPRAWDZAC CO Z NIM
		if( EnemyDead )
			return ;
		
		//Debug.Log( "Get Damage" ) ;
		
		//ODTWARZAMY DZWIEK OBRAZEN
		enemyAudio.Play () ;

		//USTAWIAMY POZYCJE GDZIE TRAFIL GRACZ
		ParticleHit.transform.position = Where ;
		//ODPALAMY PARTICLE
		ParticleHit.Play() ;
		
		//ODEJMUJEMY OD ZYCIA OBRAZENIA
		currentHealthEnemy -= HowMuch ;
		
		//USTAWIAMY OBECNY STAN NA HealthBar
		healthBar.value = currentHealthEnemy ;
		
		//SPRAWDZAMY CZY ENEMY ZYJE 
		if( currentHealthEnemy <= 0 )
		{
			HeIsDeath() ;
		}
	}
	
	
	public int CurrentEnemyHealth()
	{
		//Debug.Log( "Curent Health" ) ;
		return currentHealthEnemy ;
	}
	

	//FUNKCJA UZYWANA PRZEZ EVENT ZARAZ PO ANIMACJI "Dead" (TRZEBA TAM USTAWIC ZEBY SIE WLACZYLA)
	public void StartGoDown() 
	{
		//ENEMY IDZIE W DOL
		isGoDown = true ;

		//WYLACZAMY NavMesh ZEBY MARTWY PRZECIWNIK NIE STARAL SIE PORUSZAC
		GetComponent<NavMeshAgent>().enabled = false ;
		//USTAWIAMY isKinematic W Rigidbody PO TO ZEBY PRZECIWNIK PRZELECIAL PRZEZ PODLOGE
		GetComponent <Rigidbody> ().isKinematic = true;

		//NISZCZYMY OBIEKT PRZECIWNIKA PO OKRESLONYM CZASIE
		Destroy( gameObject , 3f ) ;
	}


	void HeIsDeath()
	{
		//ENEMY NIE ZYJE
		EnemyDead = true ;

		//USTAWIAMY TRIGGER NA TRUE ZEBY KAZDY MOGL PRZEJSC PRZEZ ENEMY
		capsuleCollider.isTrigger = true ;

		//USTAWIAMY ZMIENNA ANIMACJI NA TRUE
		animationEnemy.SetTrigger ("Dead") ;
		
		//USTAWIAMY NOWY DZWIEK (SMIERC ENEMY) PO CZYM ODPALAMY GO
		enemyAudio.clip = enemyDeath ;
		enemyAudio.Play() ;

		//DODAJEMY PUNKTY 
		ScoreManager.score += scoreValue ;
	}
}
