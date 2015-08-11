using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ChangeAudioVolumeSound : MonoBehaviour 
{
	//TABLICA OBIEKTOW Z AUDIO
	public GameObject[] GameObjectAudioSource ;

	//REFERENCJA DO SUWAKA
	Slider slider ;

	AudioSource[] audioSource ;
	
	void Awake () 
	{
		//POBIERAMY KOMPONENT SUWAKA
		slider = GetComponent<Slider>() ;

		//TWORZYMY TABLICE WIELKOSCI ILOSCI PONADYCH ELEMENTOW
		audioSource = new AudioSource[GameObjectAudioSource.Length] ;


		//SPRAWDZAMY ILE JEST OBIEKTOW
		if( GameObjectAudioSource.Length > 1 )
		{
			//PRZELATUJEMY PO TABLICY OBIEKTOW I POBIERAMY KOMPONENT AUDIO
			for( int i = 0 ; i < GameObjectAudioSource.Length ; i++ )
			{
				audioSource[i] = GameObjectAudioSource[i].GetComponent<AudioSource>()  ;
			}
		}
		else 
			audioSource[0] = GameObjectAudioSource[0].GetComponent<AudioSource>() ;
		
		

		//UPEWNIAMY SIE ZE WSZYSSTKIE OBIEKTY MAJA TAKA SAMA GLOSNOSC
		for( int i = 0 ; i < audioSource.Length ; i++ )
		{
			audioSource[i].volume = audioSource[0].volume ;
		}
		
		
		//SUWAK USTAWIAMY NA WARTOSC GLOSNOSCI AUDIO
		slider.value = audioSource[0].volume ;
	}
	
	void OnEnable()
	{
		//Debug.Log( "ENABLE" ) ;
	
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
			for( int i = 0 ; i < audioSource.Length ; i++ )
			{
				audioSource[i].volume = slider.value ;
			}
			
			yield return null;
		}
	}
}
