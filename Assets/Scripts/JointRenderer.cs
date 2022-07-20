using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JointRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Material _material;

    [SerializeField] private Vector3 startPos;

    void Start()
    {
        _renderer.positionCount = _points.Count;

        _renderer.material = _material;

        startPos = transform.GetChild(0).localPosition;
        // 0.3702438

    }



    // Update is called once per frame
    void Update()
    {
       // transform.GetChild(0).localPosition = startPos;

        DrawLine();
    }

    private void OnDrawGizmos()
    {
        _renderer.positionCount = _points.Count;
        _renderer.material = _material;
        DrawLine();
    }

    private void DrawLine()
    {
        _renderer.SetPositions(_points.Select(p => p.position).ToArray());
    }
}
