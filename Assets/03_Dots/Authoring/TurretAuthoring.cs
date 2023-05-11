using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

class TurretAuthoring : UnityEngine.MonoBehaviour
{
}

// Bakers convert authoring MonoBehaviours into entities and components.
class TurretBaker : Baker<TurretAuthoring>
{
    public override void Bake(TurretAuthoring authoring)
    {
        AddComponent<Turret>();
    }
}