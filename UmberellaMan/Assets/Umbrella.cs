using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UmbrellaState { open, closed };
public class Umbrella : MonoBehaviour
{
    public Mesh umbrellaModelClosed;
    public Mesh umbrellaModelOpen;
    MeshFilter meshFilter;
    UmbrellaState state;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = umbrellaModelClosed;
        state = UmbrellaState.closed;
    }

    public void Open()
    {
        if(state == UmbrellaState.closed)
        {
            state = UmbrellaState.open;
            meshFilter.mesh = umbrellaModelOpen;
        }
    }
    public void Close()
    {
        if(state == UmbrellaState.open)
        {
            state = UmbrellaState.closed;
            meshFilter.mesh = umbrellaModelClosed;
        }
    }
}
