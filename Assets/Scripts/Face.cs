using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private int value;

    public int Value { get => value; set => this.value = value; }
}
