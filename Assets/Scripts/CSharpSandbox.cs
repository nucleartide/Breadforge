using UnityEngine;

public class CSharpSandbox : MonoBehaviour
{
    // This declares a delegate type.
    public delegate int Comparison<in T>(T left, T right);

    // This declares an instance of the type above.
    public Comparison<int> comparator;
}
