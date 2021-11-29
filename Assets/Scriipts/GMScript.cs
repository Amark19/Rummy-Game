using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMScript : MonoBehaviour
{
    public static GMScript gm;
    public Sprite[] card;
    public GameObject CardPrefab;
    public static string[] Typeofcard={"S","C","D","H"};
    public static string[] Valueofcard={"01","02","03","04","05","06","07","08","09","10","11","12","13"};
    public List<string> Deck;
    public GameObject cardContainer;
    public GameObject jokerContainer;
    public GameObject DroppedContainer;

    public GameObject Player;
    public GameObject otherPlayer;
    public List<string> playerCardname;

    public int score;
    public Text scoreText;
    
    // Start is called before the first frame update
    private void Awake() {
        gm=this;
    }
    void Start()
    {
        // creatingDeck();
        Deck = creatingDeck();
        shuffle(Deck);
        cardspawn();
        GettingJoker();
        SettingCardsForPlayer();
        SettingCardsForPlayer1();
        setJoker();
    }

    public  List<string> creatingDeck() {
        List<string> newDeck = new List<string>();
        foreach(string toc in Typeofcard){
            foreach(string voc in Valueofcard){
                newDeck.Add(toc+voc);
            }
        // Debug.Log(newDeck);
        }
        return newDeck;
    }


    void shuffle <T>(List<T> list){
        System.Random random = new System.Random();

        int n = list.Count;

        while (n>1){
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public void cardspawn(){
        foreach(string cardname in Deck){
            GameObject cards = Instantiate(CardPrefab,cardContainer.transform);
            cards.name = cardname;
        }
    }

    void GettingJoker(){

        int index = Random.Range(0,cardContainer.transform.childCount);

        cardContainer.transform.GetChild(index).transform.SetParent(jokerContainer.transform); 
        jokerContainer.transform.GetChild(0).gameObject.transform.GetComponent<UpdateCardInfo>().isFace=true; 
        jokerContainer.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        jokerContainer.transform.localEulerAngles = new Vector3(0,0,-30);
    }

    void SettingCardsForPlayer(){

        float z=0.2f;
        for(var i=0;i<13;i++){
            playerCardname.Add(cardContainer.transform.GetChild(i).gameObject.name);
        }
        
        foreach(var cardname in playerCardname){
            cardContainer.transform.Find(cardname).gameObject.transform.SetParent(Player.transform);
            Player.transform.Find(cardname).gameObject.GetComponent<UpdateCardInfo>().isFace=true;
            Player.transform.Find(cardname).gameObject.transform.localPosition = new Vector3(0,0,z);
            z-=0.2f;
        }
    }
    void SettingCardsForPlayer1(){
        playerCardname.Clear();
        float z=0.2f;
        for(var i=0;i<13;i++){
            playerCardname.Add(cardContainer.transform.GetChild(i).gameObject.name);
        }
        playerCardname.Sort();

        foreach(var cardname in playerCardname){
            cardContainer.transform.Find(cardname).gameObject.transform.SetParent(otherPlayer.transform);
            otherPlayer.transform.Find(cardname).gameObject.GetComponent<UpdateCardInfo>().isFace=true;
            otherPlayer.transform.Find(cardname).gameObject.transform.localPosition = new Vector3(0,0,z);
            z-=0.2f;
        }
        Invoke("UpdatePlayerScore",1);
    }
    public void setJoker(){
        for(int i=0;i < Player.transform.childCount;i++){
            if(Player.transform.GetChild(i).name.Substring(1,2)=="11"){
                ConditioningGame.temp=Player.transform.GetChild(i).name;
                break;
            }
        }
    }
    public void UpdatePlayerScore(){
        for(int i=0;i < Player.transform.childCount;i++){
            if(Player.transform.GetChild(i).GetComponent<UpdateCardInfo>()!=null){
                score+=Player.transform.GetChild(i).GetComponent<UpdateCardInfo>().myvalue;
            }
        }
        if(score>80){
            score=80;
        }
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
