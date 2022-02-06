using System;
using UnityEngine;

namespace Behaviours
{
    public class Runner : Seek
    {
        [SerializeField] private float minDistance;
        private Transform _targetTransform;
        public event Action<int> Approaching;
        public int Index { get; private set; }
        private static int _globalIndex=0;

        public override void Awake()
        {
            base.Awake();
            Index = _globalIndex++;
        }

        public override Steering GetSteering()
        {
            var distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < minDistance)
            {
                OnApproaching(Index);
                return new Steering();
            }
            return base.GetSteering();
        }

        private void OnApproaching(int arg)
        {
            Approaching?.Invoke(arg);
        }
    }
}