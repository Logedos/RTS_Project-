using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public sealed class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface _navMeshSurface;

    private void OnValidate()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Awake()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
