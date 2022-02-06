using System;
using UnityEngine;

namespace Behaviours
{
    public class Face : Align
    {
        //protected GameObject targetAux;

        public override void Awake()
        {
            base.Awake();
        //    targetAux = target;
            /*target = new GameObject();
            target.AddComponent<Agent>();*/
        }

        /*private void OnDestroy()
        {
            Destroy(target);
        }*/

        public override Steering GetSteering()
        {
            /*var direction = targetAux.transform.position - transform.position;
            if (direction.magnitude > 0.0f)
            {
                var targetOrientation = Mathf.Atan2(direction.x, direction.z);
                targetOrientation *= Mathf.Rad2Deg;
                target.GetComponent<Agent>().orientationAngle = targetOrientation;
            }*/
            return base.GetSteering();
        }
    }
    
}