using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using Hostile.SimplePool.Components;
using Hostile.SimplePool;

using Pathfinding;

[RequireComponent(typeof(Seeker))]
[AddComponentMenu("Scripts/AI/Base")]
public class AI_Base : BasePoolComponent
{

    Seeker _seeker;

    #region unity message functions

    void Start()
    {
        _seeker = GetComponent<Seeker>();
    }

    void OnGUI()
    {
    }

    public void FixedUpdate()
    {
    }

    #endregion

    #region implemented abstract members of BasePoolComponent

    public override void OnSpawn()
    {
        //throw new NotImplementedException();
    }

    public override void OnDespawn()
    {
        //throw new NotImplementedException();
    }

    #endregion

    #region implemented abstract members of BasePoolComponent

    public virtual void Hit(Vector3 direction, int damage)
    {
    }

    public virtual void Kill(Vector3 direction)
    {
    }

    #endregion
}
