using UnityEngine;

public enum ColliderType
{
    TwoD,
    ThreeD
}

public class ObjCol : MonoBehaviour
{
    [SerializeField] private Collider2D Col2D;
    [SerializeField] private Collider Col3D;
    
    public void TransCollider(ColliderType type)
    {
        if(Col2D == null || Col3D == null)
        {
            Debug.Log("asdf");
            return;
        }
        switch (type)
        {
            case ColliderType.TwoD : 
                Col2D.enabled = true;
                Col3D.enabled = false;
                break;
            case ColliderType.ThreeD :
                Col3D.enabled = true;
                Col2D.enabled = false;
                break;
        }
    }
    private void Awake()
    {
        DimensionManager.Instance.Register(this);
        TransCollider(DimensionManager.Instance.curType);
    }
    private void OnDestroy()
    {
        if(DimensionManager.Instance != null) DimensionManager.Instance.Unregister(this);
    }
}
