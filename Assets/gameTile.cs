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
    SpriteRenderer gem;
    SpriteRenderer selected;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        //index = indexCounter++;

        var gemIndex = rand.Next(0, gems.Count);
        holding = gems[gemIndex];

        gameGrid = GetComponentInParent<GameGrid>();

        bg = GetComponent<SpriteRenderer>();

        gem = transform.Find("Gem").GetComponent<SpriteRenderer>();
        gem.sprite = holding.getSprite();

        selected = transform.Find("selected").GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        gameGrid.setCurrent(this);
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
        return holding;
    }

    public void setGem(Gem newGem)
    {
        if (newGem)
        {
            holding = newGem;
            gem.sprite = holding.getSprite();
        } else
        {
            holding = null;
            gem.sprite = null;
        }
    }

    public void setBg(Sprite rep)
    {
        bg.sprite = rep;
    }

    public void randomGem()
    {
        holding = gems[rand.Next(0, gems.Count)];
        gem.sprite = holding.getSprite();
    }

    public void setSelected(bool isSelected)
    {
        selected.enabled = isSelected;
    }


}
