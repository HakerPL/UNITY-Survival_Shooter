using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	//REFERENCJA DO CANVAS OD MENU
	public GameObject canvasMenu ;

	//SKRYPT DO ZMIANY GLOSNOSCI EFFEKTOW
	public ChangeAudioVolumeEffect changeAudioVolumeEffect ;
	//SKRYPT DO ZMIANY GLOSNOSCI MUZYKI
	public ChangeAudioVolumeSound changeAudioVolumeSound ;

	//OKNO W MENU AUDIO
	public GameObject WindowInMenuTurnOFF ;

	//REFERENCJA DO HUD
	CanvasGroup canvasGroup;

	//POMOCNA ZMIENNA DO OBSLUGI WLACZANIA I WYLACZANIA MENU
	bool menu = false ;

	//REFERENCJA DO SKRYPTU GRACZA
	PlayerShoot playerShoot ;

	//REFERENCJA NA SKRYPT GRACZA
	GameObject PlayerComponent ;



	void Awake()
	{
		//SZUKAMY GRACZA I POBIERAMY SKRYPT
		PlayerComponent = GameObject.FindGameObjectWithTag("Player") ;
		//JESLI SZUKANIE SIE POWIODLO TO PRZYPISUJEMY COMPONENT DO OBIEKTU
		if( PlayerComponent != null )
		{
			playerShoot = PlayerComponent.GetComponentInChildren<PlayerShoot>() ;
		}
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(playerShoot == null)
		{
			Debug.Log( " can't find Player PlayerShoot (MenuManager) " ) ;
		}

		canvasGroup = GetComponent<CanvasGroup>();
	}


	void Update()
	{
		//Debug.Log( "MenuManager Update" ) ;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//ZMIENIAMY STAN MENU
			menu = !menu ;
			if( menu )
			{
				//WLACZAMY INTERAKCJE ORAZ BLOKUJEMY RAYCAST
				canvasGroup.interactable = true ;
				canvasGroup.blocksRaycasts = true ;
				//WLACZAMY SCREEN MENU
				canvasMenu.SetActive( true ) ;

				//PONOWNIE POBIERAMY KOMPONENT PlayerShoot ( MOGLISMY ZMIENIC BRON )
				playerShoot = PlayerComponent.GetComponentInChildren<PlayerShoot>() ;

				//WYLACZAMY STRZELANIE
				playerShoot.enabled = false ;

				//WLACZAMY SKRYPTY ZMIANY GLOSNOSCI DZWIEKU
				changeAudioVolumeEffect.enabled = true ;
				changeAudioVolumeSound.enabled = true ;
			}
			else
			{
				//WYLACZAMY INTERAKCJE ORAZ ODBLOKOWUJEMY RAYCAST
				canvasGroup.interactable = false ;
				canvasGroup.blocksRaycasts = false ;
				//WYLACZAMY SCREEN MENU
				canvasMenu.SetActive( false ) ;

				//WLACZAMY STRZELANIE
				playerShoot.enabled = true ;

				//WYLACZAMY SKRYPTY ZMIANY GLOSNOSCI DZWIEKU
				changeAudioVolumeEffect.enabled = false ;
				changeAudioVolumeSound.enabled = false ;

				//WYLACZAMY OKNO AUDIO
				WindowInMenuTurnOFF.SetActive( false ) ;
			}

			//WLACZAMY / WYLACZAMY PAUZE
			Pause();
		}
	}

	public void Pause()
	{
		//Time.timeScale 1 CZAS IDZIE NORMALNIE 0 FUNKCJE ( OPRUCZ realtimeSinceStartup ) ZATRZYMUJA SIE (CZAS NIE PLYNIE)
		// 0.5 SLOW MOTION ;]
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}
}
