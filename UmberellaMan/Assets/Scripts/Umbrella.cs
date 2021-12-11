using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UmbrellaState { open, closed };
public class Umbrella : MonoBehaviour
{
    public Mesh umbrellaModelClosed;
    public Mesh umbrellaModelOpen;
    MeshFilter meshFilter;
    public UmbrellaState State { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = umbrellaModelClosed;
        State = UmbrellaState.closed;
    }

    public void Open()
    {
        if(State == UmbrellaState.closed)
        {
            State = UmbrellaState.open;
            meshFilter.mesh = umbrellaModelOpen;
        }
    }
    public void Close()
    {
        if(State == UmbrellaState.open)
        {
            State = UmbrellaState.closed;
            meshFilter.mesh = umbrellaModelClosed;
        }
    }
}
