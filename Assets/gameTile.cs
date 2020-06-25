using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gameTile : MonoBehaviour, IPointerClickHandler
{
    private static System.Random rand = new System.Random();
    private static int indexCounter = 1;

    [SerializeField]
    GameGrid gameGrid;

    [SerializeField]
    List<Gem> gems;

    Gem holding;

    SpriteRenderer bg;
    //SpriteRenderer gem;
    SpriteRenderer selected;

    int index;

    [SerializeField]
    Gem gem;

    [SerializeField]
    Transform gemTransform;

    // Start is called before the first frame update
    void Start()
    {
        //index = indexCounter++;

        var gemIndex = rand.Next(0, gems.Count);
        holding = gems[gemIndex];
        gem = Instantiate(holding, transform.localPosition, Quaternion.identity);
        gem.transform.parent = this.transform;
        gemTransform = gem.transform;

        gameGrid = GetComponentInParent<GameGrid>();

        bg = GetComponent<SpriteRenderer>();

        selected = transform.Find("selected").GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData data)
    {
        gameGrid.setCurrent(this);
        //print(holding.getVal());
    }

    public int getIndex()
    {
        return index;
    }

    public void setIndex(int newIndex)
    {
        index = newIndex;
    }

    public Gem getGem()
    {
        return gem;
    }

    public void setGem(Gem newGem)
    {
        if (newGem)
        {
            gem = newGem;
            gemTransform = gem.transform;
        } else
        {
            gem  = null;
            gemTransform = null;
        }
    }

    public void setBg(Sprite rep)
    {
        bg.sprite = rep;
    }

    public void randomGem()
    {
        holding = gems[rand.Next(0, gems.Count)];
        gem = Instantiate(holding, transform.localPosition, Quaternion.identity);
        gem.transform.parent = this.transform;
    }

    public void setSelected(bool isSelected)
    {
        selected.enabled = isSelected;
    }

    public int getGemVal()
    {
        return gem.getVal();
    }

    public Transform getGemTransform()
    {
        return gemTransform;
    }
}
