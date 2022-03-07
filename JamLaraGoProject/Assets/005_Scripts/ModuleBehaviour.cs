using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{
    [Header("Anchors")]
    [SerializeField] private bool _topAnchor = true;
    [SerializeField] private bool _forwardAnchor = true;
    [SerializeField] private bool _rightAnchor = true;
    [SerializeField] private GameObject[] _anchorsSprites = new GameObject[3];

    public ModuleBehaviour[] _neighbors = new ModuleBehaviour[6];

    private float _meshSize = 0f;

    private Vector3?[] _anchors = new Vector3?[3];

    private void Awake()
    {
        _meshSize = this.transform.GetChild(0).localScale.x;

        Vector3 pos = transform.position;

        if (_topAnchor) _anchors[0] = (pos + new Vector3(0f, _meshSize, 0f));
        if (_forwardAnchor) _anchors[1] = (pos + new Vector3(-_meshSize * 0.5f, _meshSize * 0.5f, 0f));
        if (_rightAnchor) _anchors[2] = (pos + new Vector3(0f, _meshSize * 0.5f, -_meshSize * 0.5f));

        _anchorsSprites[0].SetActive(_topAnchor);
        _anchorsSprites[1].SetActive(_forwardAnchor);
        _anchorsSprites[2].SetActive(_rightAnchor);
    }

    [ContextMenu("CheckNeigbors")]
    public void RegisterNeighbors()
    {
        Vector3[] dir = new Vector3[6];

        _meshSize = this.transform.GetChild(0).localScale.x;

        dir[0] = new Vector3(0f, 0f, 1);
        dir[1] = new Vector3(0f, 0f, -1);
        dir[2] = new Vector3(1, 0f, 0f);
        dir[3] = new Vector3(-1, 0f, 0f);
        dir[4] = new Vector3(0f, 1, 0f);
        dir[5] = new Vector3(0f, -1, 0f);

        RaycastHit hit = new RaycastHit();

        Vector3 pos = transform.position + new Vector3(0f, _meshSize * 0.5f, 0f);

        for (int i = 0; i < dir.Length; i++)
        {
            Ray ray = new Ray(pos, dir[i]);

            if (Physics.Raycast(ray, out hit, 1f))
            {
                hit.collider.gameObject.TryGetComponent<ModuleBehaviour>(out _neighbors[i]);
            }
            else
            {
                _neighbors[i] = null;
            }
        }
    }

    public ModuleBehaviour[] GetNeighbors()
    {
        return _neighbors;
    }

    public Vector3?[] GetAnchorsPos()
    {
        return _anchors;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3? pos in _anchors)
        {
            if(pos != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(pos.Value, 0.5f);
            }
        }
    }
}
