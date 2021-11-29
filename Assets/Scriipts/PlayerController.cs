using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public  bool isCardSelect=true;
    public Transform Selectedcard;
    public Transform Lastcard;
    public int countclk;
    public List<Transform> cards;
    
    // Start is called before the first frame update
    void Start()
    {
        isCardSelect=true;
    }

    public void SubmitGroup(){
        ConditioningGame.cg.cardsname.Clear();
        for(int i=0;i<cards.Count;i++){
            ConditioningGame.cg.cardsname.Add(cards[i].name);
        }
        ConditioningGame.cg.ValidatingCardByType();
        if(ConditioningGame.setmatch){
        cards.Clear();
    }
        if(ConditioningGame.pureseq){
        cards.Clear();
    }
    }

    public void CardslistUpdate(){
        for(int i=0;i<  cards.Count;i++){
            cards[i].GetComponent<SpriteRenderer>().color= Color.gray;
        }
    }
    public void updateselectedCard(Transform selcard){
        
    bool iscardfound=false;
        if(isCardSelect){
            for(int i=0;i<cards.Count;i++){
                if(selcard.name == cards[i].name){
                    cards[i].GetComponent<SpriteRenderer>().color = Color.white;
                    cards.Remove(cards[i]);
                    iscardfound=true;
                }
            }
        }
        if(!iscardfound){
            cards.Add(selcard);
        }
    }
    // Update is called once per frame
    void Update()
    {
    
       if (Input.GetMouseButtonDown(0)) {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit){
                if(hit.collider.gameObject.transform.tag=="card"){
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color=Color.gray;
                    Selectedcard =  hit.collider.gameObject.transform;
                    if(!isCardSelect){
                    if(Lastcard!=null){
                        CardslistUpdate();
                    Lastcard.GetComponent<SpriteRenderer>().color=Color.white;
                    }
                    Lastcard =  Selectedcard;
                }
                else{
                        // cards.Add(Selectedcard);
                        updateselectedCard(Selectedcard);
                        CardslistUpdate();
                    
                }
                }
            }
            }
    }
}
