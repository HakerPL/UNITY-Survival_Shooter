using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	//ILE ZYCIA BEDZIE MIAL GRACZ
	public int playerHealth = 100 ;
	//SKALA ZYCIA SLIDER
	public Slider healthBar ;
	//DZWIEK KIEDY GRACZ UMIERA
	public AudioClip playerDeath ;

	//ZDJECIA DAMAGE (MIGAJACY CZERWONY)
	public Image DamageImage ;
	//SZYBKOSC ZNIKNIECIA DamageImage
	public float fadeDamageImage = 5f ;
	//KOLOR MIGOTANIA					RED , GREEN , BLUE , ALPHA
	public Color DamageColor = new Color( 1f , 0f , 0f , 0.1f ) ;



	//ANIMACJA POSTACI
	Animator animationPlayer ;
	//DZWIEK JAKI "WYDOBYWA" SIE Z GRACZA (OBECNIE)
	AudioSource playerAudio ;
	//REFERENCJA DO SKRYPTU RUCH GRACZA
	PlayerMove playerMove ;
	//REFERENCJA DO SKRYPTU ZE STRZELANIEM ( TRZEBA GO WYLACZYC JAK GRACZ NIE ZYJE )
	PlayerShoot playerShoot ;


	//OBECNE ZYCIE GRACZA
	int currentHealthPlayer ;


	//ZMIENNE BOOL DO SMIERCI LUB DAMAGE GRACZA
	bool PlayerDead ;
	bool damage ;

	void Awake()
	{
		//PRZYPISUJEMY KOMPONENTY ZMIENNYM
		animationPlayer = GetComponent<Animator>() ;
		playerAudio = GetComponent<AudioSource>() ;
		playerMove = GetComponent<PlayerMove>() ;
		playerShoot = GetComponentInChildren<PlayerShoot>() ;

		//USTAWIAMY POCZATKOWE ZYCIE GRACZA
		currentHealthPlayer = playerHealth ;

		//USTAWIAMY MAXYMALNA WIELKOSC HealthBar
		healthBar.maxValue = playerHealth ;
		healthBar.value = playerHealth ;
	}
	

	// Update is called once per frame
	void Update () 
	{
		//JESLI GRACZ OTRZYMAL OBRAZENIA
		if( damage )
		{
			//USTAWIAMY KOLOR MIGNIECIA NA EKRANIE
			DamageImage.color = DamageColor ;
		}
		else
		{
			//DZIEKI LERP ZMIENIAMY KOLOR Z OBECNEGO NA CZYSTY (NIEWIDOCZNY) 
			DamageImage.color = Color.Lerp( DamageImage.color , Color.clear , fadeDamageImage * Time.deltaTime ) ;
		}

		damage = false ;
	}


	//FUNKCJA UZYWANA PRZEZ INNE OBIEKTY ZEBY ZADAC OBRARZENIA GRACZOWI
	public void TakeDamage( int HowMuch ) 
	{
		//OTRZYMUJEMY OBRAZENIA
		damage = true ;

		//Debug.Log( "Get Damage" ) ;

		//ODTWARZAMY DZWIEK OBRAZEN
		playerAudio.Play () ;

		//ODEJMUJEMY OD ZYCIA OBRAZENIA
		currentHealthPlayer -= HowMuch ;

		//USTAWIAMY OBECNY STAN NA HealthBar
		if ( currentHealthPlayer > 0 )
			healthBar.value = currentHealthPlayer ;
		else
			healthBar.value = 0 ;

		//SPRAWDZAMY CZY GRACZ ZYJE ORAZ NIE JEST JESZCZE MARTWY
		if( currentHealthPlayer <= 0 && !PlayerDead )
		{
			HeIsDeath() ;
		}
	}


	public void AddHealth( int addHealth )
	{
		//DODAJEMY ZYCIE
		currentHealthPlayer += addHealth ;
		//SPRAWDZAMY CZY NIE PRZEKROCZYLISMY MAX ILOSC ZYCIA JESLI TAK TO ZMNIEJSZAMY DO MAX
		if( currentHealthPlayer > playerHealth )
			currentHealthPlayer = playerHealth ;

		//USTAWIAMY OBECNY STAN NA HealthBar
		healthBar.value = currentHealthPlayer ;
	}



	public int CurrentPlayerHealth()
	{
		//Debug.Log( "Curent Health" ) ;
		return currentHealthPlayer ;
	}


	void HeIsDeath()
	{
		//GRACZ NIE ZYJE
		PlayerDead = true ;

		//USTAWIAMY ZMIENNA ANIMACJI NA TRUE
		animationPlayer.SetTrigger ("Die") ;

		//USTAWIAMY NOWY DZWIEK (SMIERC GRACZA) PO CZYM ODPALAMY GO
		playerAudio.clip = playerDeath ;
		playerAudio.Play() ;

		//POBIERAMY SKRYPT BRONI ( BO MOGLA SIE ZMIENIC )
		playerShoot = GetComponentInChildren<PlayerShoot>() ;

		//WYLACZAMY SKRYPT PORUSZANIA SIE GRACZA ORAZ STRZELANIA ( SKORO NIE ZYJE TO LEPIEJ ZEBY SIE NIE RUSZAL I NIE STRZELAL )
		if ( playerMove.enabled )
			playerMove.enabled = false ;
		if ( playerShoot.enabled )
			playerShoot.enabled = false ;
	}
}
