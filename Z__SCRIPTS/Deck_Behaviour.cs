using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deck_Behaviour : MonoBehaviour{

	public Deck _Deck_1_;
	public GameObject NewObj;
	public GameObject Back_Card;
	public List<Sprite> Player_1_Deck_Back = new List<Sprite>();
	public List<Sprite> Player_2_Deck_Back = new List<Sprite>();
	public Debbug_Logger_Script Deb;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 27; i++) {
			Player_1_Deck_Back.Add(GameObject.Find("Back_Deck_Prefab").GetComponent<UnityEngine.UI.Image>().sprite);
		}

		for (int i = 0; i < 28; i++) {
			Player_2_Deck_Back.Add(GameObject.Find("Back_Deck_Prefab").GetComponent<UnityEngine.UI.Image>().sprite);
		}

		for (int i = 0; i < Player_1_Deck_Back.Count; i++) {
			Back_Card = Instantiate(GameObject.Find("Back_Deck_Prefab"), Vector2.zero , Quaternion.identity);
			Back_Card.transform.position = (Vector2)GameObject.Find("Deck_1").transform.position + new Vector2(Random.Range(0,10),Random.Range(0,10));
			Back_Card.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("FP_Cards_In_Deck").transform,true); //make a child of
		}

		for (int i = 0; i < Player_2_Deck_Back.Count; i++) {
			Back_Card = Instantiate(GameObject.Find("Back_Deck_Prefab"), Vector2.zero , Quaternion.identity);
			Back_Card.transform.position = (Vector2)GameObject.Find("Deck_2").transform.position + new Vector2(Random.Range(0,10),Random.Range(0,10));
			Back_Card.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("SP_Cards_In_Deck").transform,true); //make a child of
		}

	}

}
