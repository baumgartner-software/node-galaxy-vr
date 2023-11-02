using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class NodeData
{
    public string Id { get; set; }
    public float Size { get; set; }
    public List<string> Edges { get; set; }
    public string Label { get; set; }
    public string Text { get; set; }
    public string Color { get; set; }
    public string Additional { get; set; }
    public Vector3 Position { get; set; }
}