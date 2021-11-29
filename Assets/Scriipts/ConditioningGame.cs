using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditioningGame : MonoBehaviour
{

    public static ConditioningGame cg;
    public GameObject setmatchObj;
    public GameObject PureSequenceobj;
    public  string joker;
    public static string temp;
    public List<string> cardsname;
    public bool isCardTypeUniq = false;
    public bool isjokerPresent=false;
    public Text alert;
    public GameObject bg;
    public GameObject playercontainer;
    //success bools
    public static bool setmatch=false;
    public static bool pureseq=false;
    int cnt,cont=0;
    private void Awake() {
        cg = this;
    }

    public void ValidatingCardByType(){
        int cardmathCount=0;
        string commoncard="";
        for(int i=0;i<cardsname.Count;i++){
            if(cardsname[i].Remove(0,1)!= joker.Remove(0,1)){
                commoncard=cardsname[i];
                break;
            }
        }
        for(int i=0;i<cardsname.Count;i++){
            if(i + 1 < cardsname.Count){
            string card = cardsname[i];
            string card2 = cardsname[i+1];
            if(card.Substring(0,1)== card2.Substring(0,1)){
                if(int.Parse(card.Remove(0,1))!= int.Parse(joker.Remove(0,1)) && int.Parse(card2.Remove(0,1))!= int.Parse(joker.Remove(0,1))){
                cardmathCount++;
            }
            else{
                cardmathCount++;
                isjokerPresent=true;
            }
            }
            else if(int.Parse(card.Remove(0,1))!= int.Parse(joker.Remove(0,1))){
                if(commoncard.Substring(0,1)==card2.Substring(0,1)){
                    isjokerPresent=true;
                cardmathCount++;
                print("Jocker Found in Card1");
            }
            }
            else if(int.Parse(card2.Remove(0,1))!= int.Parse(joker.Remove(0,1))){
                print("Jocker Found in Card2");
                isjokerPresent=true;
                cardmathCount++;
            }
            
            else{
                print("Not found");
            }
            // else if(cardsname.count>1 && i==cardsname.count-1){
            //     if(card.Substring(0,1)==tempstr || card2.Remove(0,1)==joker.Remove(0,1)  ){
            //         cardmathCount++;
            //     }
            //     else if()
            // }
            }


        }
        if(cardmathCount == cardsname.Count-1 ){
            isCardTypeUniq = true;
            checkForPureSeq();
        }
        else{
            isCardTypeUniq = false;
            bool issetmath=SetCheck();
            if(issetmath){
                setmatch=true;
                print("Set matched");
                alert.text="";
                alert.text="Set  Matched";
                for(int i=0;i<cardsname.Count;i++){
                    playercontainer.transform.Find(cardsname[i]).transform.SetParent(setmatchObj.transform); 
                    if(i==0 && cnt!=0){
                        setmatchObj.transform.GetChild(cnt-1).transform.localPosition =  new Vector3(setmatchObj.transform.GetChild(cnt-1).transform.position.x + 50,setmatchObj.transform.GetChild(cnt-1).transform.position.y + 50);
                    }
                    cnt+=1;
                }
            }
            else{
                setmatch=false;
                print("Set not matched");
                alert.text="";
                alert.text="Set not Matched";

            }

        }
    }

    bool validatePureSeq(int cvalue){
        bool ismatched=false;
        for(int i=0;i<cardsname.Count;i++){
            if(cvalue == int.Parse(cardsname[i].Remove(0,1))){
                ismatched=true;
            }

        }
        return ismatched;
    }
    public void checkForPureSeq(){

        int pureMatchCount=0,impureMatchCount=0,totaljoker=0,cardnum=0,jocker=int.Parse(joker.Remove(0,1));
        for(int i=0;i<cardsname.Count;i++){
            if(cardsname[i].Remove(0,1) == joker.Remove(0,1)){
                totaljoker+=1;
            }

        }
            for(int i=0;i<cardsname.Count;i++){
        if(cardsname[i].Remove(0,1)!= joker.Remove(0,1)){
                cardnum=int.Parse(cardsname[i].Remove(0,1));
                break;
            }
        }
         for(int i=0;i<cardsname.Count;i++){
            if(i + 1 < cardsname.Count){
            int fcard=int.Parse(cardsname[i].Remove(0,1));
            print(fcard);
            bool isSequencee = validatePureSeq(fcard+1);
            if(isSequencee){
                pureMatchCount+=1;
            }
            else{
                int ncard=2;
                if(totaljoker > 0){
                    impureMatchCount+=1;
                    totaljoker-=1;
                
                for(int j=0;j<cardsname.Count;j++){
                    int card2 = int.Parse(cardsname[i].Remove(0,1));
                    card2+=ncard;
                    isSequencee = validatePureSeq(fcard+1);
            if(isSequencee){
                impureMatchCount+=1;
                break;
            }
            else{
                if(totaljoker > 0){
                    impureMatchCount+=1;
                    totaljoker-=1;
                }
                else{
                    print("sequence not matched");
                    break;
                }
            }
                }
                }
            else{

                    print("sequence not matched");
            }

            }
            }
        }
        if(pureMatchCount == cardsname.Count - 1 &&!isjokerPresent){
                alert.text="";
                alert.text="Pure Sequence";
                pureseq=true;
                    print("pure sequence");
                     for(int i=0;i<cardsname.Count;i++){
                    playercontainer.transform.Find(cardsname[i]).transform.SetParent(PureSequenceobj.transform); 
                    if(i==0 && cont!=0){
                        setmatchObj.transform.GetChild(cont-1).transform.localPosition =  new Vector3(PureSequenceobj.transform.GetChild(cont-1).transform.position.x + 50,PureSequenceobj.transform.GetChild(cont-1).transform.position.y + 50);
                    }
                    cont+=1;
                }

        }
        else if(pureMatchCount + impureMatchCount == cardsname.Count - 1 && isjokerPresent){
            alert.text="";
            pureseq=false;
                alert.text="Impure Sequence";
                    print("Impure sequence");

        }
        

    }
    public bool SetCheck(){
        int matchcount=0,cardnum=0,jocker=int.Parse(joker.Remove(0,1));
            for(int i=0;i<cardsname.Count;i++){
        if(cardsname[i].Remove(0,1)!= joker.Remove(0,1)){
                cardnum=int.Parse(cardsname[i].Remove(0,1));
                break;
            }
        }
        for(int i=0;i<cardsname.Count;i++){
            if(i + 1 < cardsname.Count){
            int fcard=int.Parse(cardsname[i].Remove(0,1));
            int scard=int.Parse(cardsname[i+1].Remove(0,1));
            print(fcard + "," + scard);
            if(fcard==scard){
                matchcount++;
            }
            else if(fcard==jocker){
                if(scard==cardnum){
                    matchcount++;
                }
            }
            else if(scard==jocker){
                matchcount++;
            }
            else{
                print("unidenfied card name");
            }
        }
        }
         if(matchcount == cardsname.Count-1 ){
            return true;
            
        }
        else{
            return false;

        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        joker=temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
