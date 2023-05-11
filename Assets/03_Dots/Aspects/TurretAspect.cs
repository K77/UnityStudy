using Unity.Entities;

readonly partial struct TurretAspect : IAspect {
    readonly RefRO<Turret> m_Turret;

    public Entity CannonBallSpawn => m_Turret.ValueRO.CannonBallSpawn;
    public Entity CannonBallPrefab => m_Turret.ValueRO.CannonBallPrefab;
}