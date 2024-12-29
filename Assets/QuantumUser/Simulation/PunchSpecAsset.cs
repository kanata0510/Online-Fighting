using Photon.Deterministic;

namespace Quantum
{
    public class PunchSpecAsset : AssetObject
    {
        public Shape3DConfig AttackShape;
        public LayerMask AttackLayers;
        public FP Damage;
        public FP KnockbackForce;
    }
}