using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.EventSystems;
using System.Collections;

//DLA LISTY
using System.Collections.Generic ;

public class ChangeAudioVolumeEffect : MonoBehaviour 
{
	//TABLICA OBIEKTOW Z AUDIO
	public GameObject[] GameObjectAudioSource ;

	//TABLICA COMPONENTOW AUDIO
	List<AudioSource> audioSource ;

	//REFERENCJA DO SUWAKA
	Slider slider ;


	void Awake () 
	{
		//POBIERAMY KOMPONENT SUWAKA
		slider = GetComponent<Slider>() ;
	}


	//void Start()
	//{
	//	StartCoroutine( "Change_Volume" ) ;
	//}

	void OnEnable()
	{
		//Debug.Log( "ENABLE" ) ;

		//TWORZYMY LISTE
		audioSource = new List<AudioSource>() ;

		//SPRAWDZAMY ILE JEST OBIEKTOW
		if( GameObjectAudioSource.Length > 1 )
		{
			//PRZELATUJEMY PO TABLICY OBIEKTOW I POBIERAMY KOMPONENT AUDIO
			for( int i = 0 ; i < GameObjectAudioSource.Length ; i++ )
			{
				audioSource.Add( GameObjectAudioSource[i].GetComponent<AudioSource>() ) ;
			}
		}
		else 
			audioSource[0] = GameObjectAudioSource[0].GetComponent<AudioSource>() ;
		

		//SZUKAMY OBIEKTOW Z TAGIEM ENEMY I GUN
		GameObject[] objectWithTagEnemy = GameObject.FindGameObjectsWithTag( "Enemy" ) ;
		GameObject objectWithTagGun = GameObject.FindGameObjectWithTag( "Gun" ) ;

		//Debug.Log ( " objectWithTag.Length : " + objectWithTag.Length ) ;


		//SPRAWDZAMY CZY COS ZNALAZL
		if( objectWithTagEnemy.Length > 0 )
		{
			//PRZELATUJEMY PO OBIEKTACH TABLICY
			foreach( GameObject objectTagEnemy in objectWithTagEnemy )
			{
				//POBIERAMY AudioSource
				AudioSource SourceAudioEnemy = objectTagEnemy.GetComponent<AudioSource>() ;

				//SPRAWDZAMY CZY OBIEKT POSIADA AudioSource
				if ( SourceAudioEnemy != null )
				{
					//DODAJEMY KOMPONENT DO LISTY
					audioSource.Add ( SourceAudioEnemy ) ;
				}
			}
		}

		if( objectWithTagGun != null )
		{
			//POBIERAMY AudioSource
			AudioSource SourceAudioGun = objectWithTagGun.GetComponent<AudioSource>() ;
			
			//SPRAWDZAMY CZY OBIEKT POSIADA AudioSource
			if ( SourceAudioGun != null )
			{
				//DODAJEMY KOMPONENT DO LISTY
				audioSource.Add ( SourceAudioGun ) ;
			}
		}

		//UPEWNIAMY SIE ZE WSZYSSTKIE OBIEKTY MAJA TAKA SAMA GLOSNOSC
		for( int i = 0 ; i < audioSource.Count ; i++ )
		{
			audioSource[i].volume = audioSource[0].volume ;
		}


		//SUWAK USTAWIAMY NA WARTOSC GLOSNOSCI AUDIO
		slider.value = audioSource[0].volume ;


		StartCoroutine( "Change_Volume" ) ;
	}


	void OnDisable() 
	{
		//Debug.Log( "DISABLE" ) ;
		StopCoroutine( Change_Volume() ) ;
	}


	public void StartCorutineVolume()
	{
		StartCoroutine( "Change_Volume" ) ;
	}

	//PO ZATRZYMANIU CZASU UPDATE I FIXEDUPDATE NIE DZIALA
	IEnumerator Change_Volume()
	{
		while( true )
		{
			//Debug.Log( "bbbbbbbbbbbbbb" ) ;
			//PRZELATUEJMY PO OBIEKTACH I USTAWIAMY GLOSNOSC ZA POMOCA SUWAKA
			for( int i = 0 ; i < audioSource.Count ; i++ )
			{
				audioSource[i].volume = slider.value ;
			}

			yield return null;
		}
	}
}
