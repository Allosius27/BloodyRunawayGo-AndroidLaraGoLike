using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ModuleBehaviour : MonoBehaviour
{
    #region Fields

    private float _meshSize = 0f;

    private Vector3? _anchorPos = null;

    #endregion

    #region UnityInspector

    [Header("Anchors")]
    [SerializeField] private bool _topAnchor = true;
    [SerializeField] private GameObject _anchorSprite = null;

    public ModuleBehaviour[] _neighbors = new ModuleBehaviour[6];

    #endregion


    #region Behavior

    private void Awake()
    {
        _meshSize = this.transform.GetChild(0).localScale.x;

        Vector3 pos = transform.position;

        if (_topAnchor) _anchorPos = (pos + new Vector3(0f, _meshSize, 0f));

        _anchorSprite.SetActive(_topAnchor);
    }

    private void Start()
    {
        //RegisterNeighbors();
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

    public Vector3? GetAnchorPos()
    {
        return _anchorPos;
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        if (!_topAnchor) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _meshSize, transform.position.z), 0.5f);
    }

    #endregion
}
