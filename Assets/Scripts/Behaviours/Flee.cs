using UnityEngine;

namespace Behaviours
{
    public class Flee : Seek
    {
        public override void Awake()
        {
            base.Awake();
            AlignAlgorithm.EnableReversing();
        }
    }
}