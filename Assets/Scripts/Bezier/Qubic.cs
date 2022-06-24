using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Bezier
{
    public class Qubic : MonoBehaviour
    {
        [SerializeField] private List<Transform> _pointTf;
        [SerializeField] private float _radius;
        [SerializeField] private static int _accuracy = 20;
        private readonly Vector3[] _points = new Vector3[_accuracy + 1];
        private readonly Vector3[] _subPositions = new Vector3[3];
        
#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            Vector3 point0Position = _pointTf[0].position;
            Vector3 point1Position = _pointTf[1].position;
            Vector3 point2Position = _pointTf[2].position;
            Vector3 point3Position = _pointTf[3].position;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point0Position, _radius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(point1Position, _radius);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point2Position, _radius);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(point3Position, _radius);
            
            Gizmos.color=Color.black;
            Gizmos.DrawLine(point0Position, point1Position);
            Gizmos.DrawLine(point2Position, point3Position);
            
            for (int i = 0; i <= _accuracy; i++)
            {
                float t =  i / (float) _accuracy;

                for (int j = 0; j < 3; j++)
                    _subPositions[j] = Vector3.Lerp(_pointTf[j].position, _pointTf[j + 1].position, t);

                Vector3 firstPoint = Vector3.Lerp(_subPositions[0], _subPositions[1], t); 
                Vector3 secondPoint = Vector3.Lerp(_subPositions[1], _subPositions[2], t);
                Vector3 point = Vector3.Lerp(firstPoint, secondPoint, t);
                _points[i] = point;
            }
            
            Handles.DrawAAPolyLine(_points);
            
        }

#endif
    }
}