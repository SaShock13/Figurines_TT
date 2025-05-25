using System;
using UnityEngine;

public class Figurine : MonoBehaviour
{
    //[SerializeField] private ShapeType shapeType;
    //private AnimalType animalType; 
    private Collider2D[] colliders;
    public FigurineData data;
    public bool isInActionBar = false;
    //private Color color;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
    }

    internal void MakeInactive()
    {
        
        var rigidBody = GetComponent<Rigidbody2D>();            
        //rigidBody.bodyType = RigidbodyType2D.Kinematic;   // не подчиняется физике, но может двигаться вручную
        rigidBody.simulated = false;
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public bool IsEqual(Figurine other)
    {
        if (data.shape == other.data.shape && data.color == other.data.color && data.animal == other.data.animal)
        {
            return true;
        }
        else return false;
    }
}
