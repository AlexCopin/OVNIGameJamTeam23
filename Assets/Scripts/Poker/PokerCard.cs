using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PokerCard : MonoBehaviour
{
    public enum CardType
    {
        ShitCoin = 0,
        DogeCoin = 1,
        LamastiCoin = 2,
        EteRhum = 3
    }
    public CardType TypeCard;

    [SerializeField]
    private List<Material> _materials;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void GenerateCard(bool IsFlipped = false)
    {
        int RandomInd = Random.Range(0, 4);
        TypeCard = (CardType)RandomInd;
        List<Material> tempList = new();
        tempList.Add(_materials[RandomInd]);
        meshRenderer.SetMaterials(tempList);
        if (IsFlipped) 
            Flip();
        
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 0, 180));
    }
}
