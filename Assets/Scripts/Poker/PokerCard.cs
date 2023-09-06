using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PokerCard : MonoBehaviour
{
    public enum CardType
    {
        MonkeCoin = 0,
        DogeCoin = 1,
        LamastiCoin = 2,
        EteRhum = 3
    }
    public CardType TypeCard;

    public Texture[] textureVisibleFaces = new Texture[4];

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void GenerateCard(bool IsFlipped = false)
    {
        TypeCard = (CardType)Random.Range(0, 4);
        if(IsFlipped) 
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 0, 180));
    }
}
