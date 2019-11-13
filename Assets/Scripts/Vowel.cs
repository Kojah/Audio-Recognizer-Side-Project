using UnityEngine;

[CreateAssetMenu(fileName = "New Vowel", menuName = "Vowel")]
public class Vowel : ScriptableObject
{
    public char name;
    public int[] bin = new int[7];
    public float[] intensity = new float[7];
}
