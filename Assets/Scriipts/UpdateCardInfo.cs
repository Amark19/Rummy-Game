using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCardInfo : MonoBehaviour
{

    GMScript gm;
    public Sprite cardFace;
    public Sprite cardBack;
    public SpriteRenderer sprender;
    public bool isFace = false;
    public int myvalue;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GMScript>();


        sprender = GetComponent<SpriteRenderer>();
        SettingCardFace();

    }

public void updateValue(){

    myvalue =int.Parse(gameObject.name.Remove(0,1));
    if(myvalue > 10 || myvalue==1){
        myvalue=10;
    }

}


void SettingCardFace(){
        
        List<string> cardname = gm.creatingDeck();
        int i =0;
        foreach(string card in cardname){
            if(this.name == card){
                cardFace = gm.card[i];
                sprender.sprite = cardFace;
                updateValue();
                break;
            }
            i++;
        }
}
    // Update is called once per frame
    void Update()
    {
        if(isFace){
            sprender.sprite = cardFace; 
        }
        else{
            sprender.sprite = cardBack; 

        }
    }
}
