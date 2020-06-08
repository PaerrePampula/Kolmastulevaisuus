using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField]
    GameObject pointerPrefab;
    Renderer prefabMaterial;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PointAndClickMovement.OnMoveStart += MovePointerTo;
        PointAndClickMovement.OnMoveStopped += DisablePointer;
    }
    private void OnDisable()
    {
        PointAndClickMovement.OnMoveStart -= MovePointerTo;
        PointAndClickMovement.OnMoveStopped += DisablePointer;
    }
    void MovePointerTo(Vector3 vector3)
    {
        pointerPrefab.SetActive(true);
        pointerPrefab.transform.position = vector3;
    }
    void DisablePointer()
    {
        pointerPrefab.transform.position = Vector3.zero;
    }
    void Start()
    {
        pointerPrefab = Instantiate(pointerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
