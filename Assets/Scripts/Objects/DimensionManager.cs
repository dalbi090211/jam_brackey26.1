using UnityEngine;
using System.Collections.Generic;

public class DimensionManager : Singleton<DimensionManager>
{
    public ColliderType curType = ColliderType.TwoD;
    private List<ObjCol> objInfos = new List<ObjCol>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void Register(ObjCol obj)
    {
        objInfos.Add(obj);
    }

    public void Unregister(ObjCol obj)
    {
        objInfos.Remove(obj);
    }

    public void TransDim()
    {
        if(curType == ColliderType.TwoD) curType = ColliderType.ThreeD;
        else curType = ColliderType.TwoD;

        for(int i = 0; i < objInfos.Count; i++)
        {
            objInfos[i].TransCollider(curType);
        }
    }
}
