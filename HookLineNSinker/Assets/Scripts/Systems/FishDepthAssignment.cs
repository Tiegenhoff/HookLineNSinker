// Allows this script to use Unity functionality, including MonoBehaviour
using UnityEngine;

// Allows this script to use the existing Fish Enum
using HookLineNSinker;

// Defines a component that sotres a fish's type and assigned depth
public class  FishDepthAssignment : MonoBehaviour
{
    // Lets us select the fish type in the Unity Inspector
    [SerializeField] private Fish fishType;

    // Lets us assign the fish's depth in the Unity Inspector
    [SerializeField] private int assignedDepth;

    // Allows other scripts to read the assigned fish type
    public Fish FishType => fishType;

    // Allows other scripts to read the assigned depth
    public int AssignedDepth => assignedDepth;
}
