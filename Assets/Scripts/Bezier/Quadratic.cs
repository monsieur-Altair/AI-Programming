using System.IO;
using UnityEditor;
using UnityEngine;

namespace Bezier
{
    public class Quadratic : MonoBehaviour
    {
        [SerializeField] private Transform _point0;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private float _radius;
        [SerializeField] private static int _accuracy = 20;
        private readonly Vector3[] _points = new Vector3[_accuracy + 1];

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            Vector3 point0Position = _point0.position;
            Vector3 point1Position = _point1.position;
            Vector3 point2Position = _point2.position;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point0Position, _radius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(point1Position, _radius);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point2Position, _radius);
            
            Gizmos.color=Color.black;
            Gizmos.DrawLine(point0Position, point1Position);
            Gizmos.DrawLine(point1Position, point2Position);
            
            for (int i = 0; i <= _accuracy; i++)
            {
                float t =  i / (float) _accuracy;
                Vector3 firstPoint = Vector3.Lerp(point0Position, point1Position, t); 
                Vector3 secondPoint = Vector3.Lerp(point1Position, point2Position, t);
                Vector3 point = Vector3.Lerp(firstPoint, secondPoint, t);
                _points[i] = point;
            }
            
            Handles.DrawAAPolyLine(_points);
            
        }

#endif
    }
}