using UnityEngine;
using System.Collections;

public enum TypeWeapon { Normal , Shootgun } ;


public class PlayerShoot : MonoBehaviour 
{
	//OBRAZENIA
	public int DamageShoot = 20 ;
	//CZAS MIEDZY STRZALAMI
	public float TimeBetweenShoot = 0.15f ;
	//ODLEGLOSC NA JAKA STRZELA BRON
	public float range = 100f ;

	//RODZAJ BRONI
	public TypeWeapon weaponType ;


	//LINIE DLA STRZALU ( OBOJETNE JAKIEGO )
	public LineRenderer[] LineShot ;


	//CZAS POTRZEBNY DO ODMIEZENIA STRZALU
	float TimerShoot ;
	//PROMIEN OD KONCA LUFY DO PRZODU
	Ray shootRay ;
	//DANE W CO TRAFILISMY
	RaycastHit shootHit ;
	//MASKA W CO MOZEMY TRAFIAC (RESZTA NIE BEDZIE BRANA POD UWAGE)
	int shootMask ;

	//KOMPONENTY ODPOWIEDZIALNE ZA SWIATLO , DZWIEK , "OBRAZEK" STRZALU , RYSOWANIE LINI
	Light gunLight ;
	AudioSource gunAudio ;
	ParticleSystem gunPaticle ;

	//PRZEZ JAKI CZAS EFEKTY(TE WYZEJ) MAJA BYC WYSWIELTANE
	float DiplayTimeEfect = 0.3f ;


	//CZAS POTRZEBNY DO SZYBSZEGO STRZELANIA
	float timeActivate ;
	float time ;


	//INVENTORY GRACZA
	Inventory inventory ;


	void Awake()
	{
		//PRZYPISUJEMY REFERENCJE
		gunLight = GetComponent<Light>() ;
		gunAudio = GetComponent<AudioSource>() ;
		gunPaticle = GetComponent<ParticleSystem>() ;

		//USTAWIAMY MASKE
		shootMask = LayerMask.GetMask("Shootable") ;

		//SZUKAMY OBIEKTU GRACZA
		GameObject player = GameObject.FindGameObjectWithTag( "Player" ) ;

		if( player != null )
		{
			//POBIERAMY KOMPONENT INVENTORY
			inventory = player.GetComponent<Inventory>() ;
		}

		if( inventory == null )
		{
			Debug.Log( " inventory is empty ( PlayerShoot ) " ) ;
		}
	}


	// Update is called once per frame
	void Update () 
	{
		//USTAWIAMY CZAS
		TimerShoot += Time.deltaTime ;

		//SPRAWDZAMY CZYM STRZELAMY
		switch( weaponType )
		{
			case TypeWeapon.Normal:

				//SPRAWDZAMY CZY CZAS MIEDZY STRZALAMI JEST MNIEJSZY NIZ CZAS ORAZ CZY WCISNIETO PRZYCISK STRZALU
				if( Input.GetButton("Fire1") && TimerShoot >= TimeBetweenShoot && inventory.AmmoCurrent() > 0 )
				{
					inventory.shootNormal() ;
					//STRZELAMY
					Shoot() ;
				}
				break ;

			case TypeWeapon.Shootgun:

				//SPRAWDZAMY CZY CZAS MIEDZY STRZALAMI JEST MNIEJSZY NIZ CZAS ORAZ CZY WCISNIETO PRZYCISK STRZALU
				if( Input.GetButton("Fire1") && TimerShoot >= TimeBetweenShoot && inventory.AmmoShootgunCurrent() > 0 )
				{
					inventory.shootShootgun() ;
					//STRZELAMY
					Shoot() ;
				}
				break ;
		}
		//SPRAWDZAMY JAK DLUGO SA WYSWIETLANE EFEKTY I JESLI SA DLUZEJ WYSWIETLANE TO JE WYLACZAMY
		if( TimerShoot >= TimeBetweenShoot * DiplayTimeEfect )
		{
			//WYLACZAMY EFEKTY
			DisableEfect() ;
		}
	}


	public void DisableEfect()
	{	
		//WYLACZAMY KOMPONENTY
		for ( int i = 0 ; i < LineShot.Length ; i++ )
		{
			LineShot[i].enabled = false ;
		}
		gunLight.enabled = false ;
	}


	public void shootSpeedUP( float activeTime )
	{
		StartCoroutine( shootSpeedUP_Coroutine( activeTime ) ) ;
	}
	
	IEnumerator shootSpeedUP_Coroutine( float activeTime )
	{
		//PRZYPISUJEMY CZAS AKTYWACJI
		timeActivate = activeTime ;
		//ZERUJEMY CZAS
		time = 0 ;
		
		//POBIERAMY STARA SZYBKOSC ORAZ ZWIEKSZAMY OBECNA O 2x
		float oldFastShoot = TimeBetweenShoot ;
		TimeBetweenShoot /= 2 ;
		
		//Debug.Log ( "oldFastShoot = " + oldFastShoot + "  TimeBetweenShoot = " + TimeBetweenShoot ) ;
		
		//SPRAWDZAMY CZY CZAS AKTYWNOSCI JEST WIEKSZY OD OBECNEGO
		while( timeActivate > time )
		{
			//ZWIEKSZAMY OBECNY I WYCHODZIMY Z PETLI
			time += Time.deltaTime ;
			//Debug.Log ( "time = " + time + "  timeActivate = " + timeActivate ) ;
			yield return new WaitForFixedUpdate(); 
		}
		
		//PRZYPISUJEMY STARA SZYBKOSC
		TimeBetweenShoot = oldFastShoot ;
		//Debug.Log ( "oldFastShoot = " + oldFastShoot + "  TimeBetweenShoot = " + TimeBetweenShoot ) ;
		
		yield return new WaitForEndOfFrame();  
	}


	void Shoot()
	{
		//ZERUJEMY CZAS
		TimerShoot = 0F ;

		//GRAMY DZWIEK
		gunAudio.Play() ;

		//URUCHAMIAMY KOMPONENTY LINIE
		for ( int i = 0 ; i < LineShot.Length ; i++ )
		{
			LineShot[i].enabled = true ;
		}

		//USTAWIAMY shootRay TAK ZEBY ZACZYNAL SIE Z LUFY I LECIAL DO PRZODU
		shootRay.origin = transform.position ;

		//WYBUR SKRYPTU DO STRZELANIA
		switch( weaponType )
		{
			case TypeWeapon.Normal:

				//USTALAMY KIERUNEK
				shootRay.direction = transform.forward ;
				
				//ZATRZYMUJEMY EFEKT I ZARAZ PONOWNIE GO URUCHAMIAMY
				gunPaticle.Stop() ;
				gunPaticle.Play () ;
				
				//USTAWIAMY POZYCJE STARTOWA LINI (0 POZYCJA STARTOWA)
				LineShot[0].SetPosition ( 0 , transform.position ) ;


				//SPRAWDZAMY CZY TRAFILISMY W OBIEKT Z MASKA (shootable) ORAZ CZY MIESCIMY SIE W ZASIEGU
				if( Physics.Raycast( shootRay , out shootHit , range , shootMask ) )
				{
					//JESLI TRAFILISMY W COS TO POBIERAMY SKRYPT EnemyHealty
					EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>() ;

					//SPRAWDZAMY CZY OBIEKT POSIADA TAKI SKRYPT (INACZEJ WYWALI BLEDY I PROGRAM SIE ZAWIESI)
					if( enemyHealth != null )
					{
						//ZADAJEMY DAMAGE  (shootHit.point ZAWIECA MIEJSCE W JAKIE TRAFIL PROMIEN Ray
						enemyHealth.TakeDamage( DamageShoot , shootHit.point ) ;
					}

					//USTAWIAMY KONIEC LINI (1 KONIEC)
					LineShot[0].SetPosition( 1 , shootHit.point ) ;
				}
				else
				{
					//JESLI NIE TRAFILISMY W CHCIANA PRZEZ NAS MASKE TO KONCZYMY LINIE
					//Z PUNKTU STARTU AZ DO ODLEGLOSCI range (POCZATEK + KIERUNEK * ZASIEG)
					LineShot[0].SetPosition( 1 , shootRay.origin + shootRay.direction * range ) ;
				}

				break ;

			case TypeWeapon.Shootgun:

				//PETLA STRZALU
				for( int i = 0 ; i < LineShot.Length ; i++ )
				{

					//LOSUJEMY ZMIENNE DLA ROZRZUTU
					float x =  Random.Range( -0.3f , 0.3f ) ;
					float y =  Random.Range( -0.1f , 0.1f ) ;


					//ZATRZYMUJEMY EFEKT I ZARAZ PONOWNIE GO URUCHAMIAMY
					
					gunPaticle.Play () ;
					
					
					//USTAWIAMY POZYCJE STARTOWA LINI ( 0 POZYCJA STARTOWA )
					for ( int a = 0 ; a < LineShot.Length ; a++ )
					{
						LineShot[a].SetPosition ( 0 , transform.position ) ;
					}


					//ZMIENIAMY KIERUNEK ( DO PRZODU + LOSOWY ROZRZUT )
					shootRay.direction = transform.forward + new Vector3( x , y , 0f ) ;
					
					Debug.Log( "shootRay.direction = " + shootRay.direction.ToString() ) ;

					//SPRAWDZAMY CZY TRAFILISMY W OBIEKT Z MASKA (shootable) ORAZ CZY MIESCIMY SIE W ZASIEGU
					if( Physics.Raycast( shootRay , out shootHit , range , shootMask ) )
					{
						//JESLI TRAFILISMY W COS TO POBIERAMY SKRYPT EnemyHealty
						EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>() ;
						
						//SPRAWDZAMY CZY OBIEKT POSIADA TAKI SKRYPT (INACZEJ WYWALI BLEDY I PROGRAM SIE ZAWIESI)
						if( enemyHealth != null )
						{
							//ZADAJEMY DAMAGE  (shootHit.point ZAWIECA MIEJSCE W JAKIE TRAFIL PROMIEN Ray
							enemyHealth.TakeDamage( DamageShoot , shootHit.point ) ;
						}
						
						//USTAWIAMY KONIEC LINI (1 KONIEC)
						for ( int a = 0 ; a < LineShot.Length ; a++ )
						{
							LineShot[i].SetPosition( 1 , shootHit.point ) ;
						}
					}
					else
					{
						//JESLI NIE TRAFILISMY W CHCIANA PRZEZ NAS MASKE TO KONCZYMY LINIE
						//Z PUNKTU STARTU AZ DO ODLEGLOSCI range (POCZATEK + KIERUNEK * ZASIEG)
						for ( int a = 0 ; a < LineShot.Length ; a++ )
						{
							LineShot[i].SetPosition( 1 , shootRay.origin + shootRay.direction * range ) ;
						}
					}
				}

				gunPaticle.Stop() ;
				
				break ;
		}
	}
}










