using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gameTile : MonoBehaviour, IPointerClickHandler
{
    private static System.Random rand = new System.Random();

    [SerializeField]
    GameGrid gameGrid;

    [SerializeField]
    List<Gem> gems;

    Gem holding;

    SpriteRenderer gem;
    SpriteRenderer selected;

    // Start is called before the first frame update
    void Start()
    {
        var gemIndex = rand.Next(0, gems.Count);
        holding = gems[gemIndex];

        gameGrid = GetComponentInParent<GameGrid>();

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
        print("here");
        selected.enabled = true;
        gameGrid.setCurrent(this);
    }

    public void deselect()
    {
        selected.enabled = false;
    }
}
