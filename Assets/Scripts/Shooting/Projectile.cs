﻿using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Shooting
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 _firePos;
        private Vector3 _direction;
        private float _speed;
        private float _timeElapsed;
        private float _minHeight;
        public event Action<Projectile> Fallen;
        public bool isBlue;

        private void Update()
        {
            var t = Time.deltaTime;
            _timeElapsed += t;
            var pos= _firePos + _direction * _speed * _timeElapsed;
            
            pos += Physics.gravity * _timeElapsed * _timeElapsed / 2.0f;
            transform.position = pos;
            if (transform.position.y <= _minHeight)
            {
                OnFallen(this);
            }
            
        }

        public void Launch(Vector3 pos, Vector3 direction, float speed, float height=0.0f)
        {
            _direction = direction.normalized;
            _firePos = pos;
            _speed = speed;
            transform.position = _firePos;
            _timeElapsed = 0.0f;
            
            var fallsPos = GetLandPos(height);
            var color = isBlue ? Color.blue : Color.red;
            Debug.DrawRay(fallsPos, Vector3.up * 5, color, 3f);
        }

        protected virtual void OnFallen(Projectile obj)
        {
            Fallen?.Invoke(obj);
        }

        private float GetLandingTime()
        {
            var pos = transform.position;
            var valueInt = (_direction.y * _direction.y) * _speed * _speed;
            valueInt -= Physics.gravity.y * 2 * (pos.y - _minHeight);
            valueInt = Mathf.Sqrt(valueInt);
            var valueAdd = (-_direction.y) * _speed;
            var valueSub = valueAdd;
            valueAdd = (valueAdd + valueInt) / Physics.gravity.y;
            valueSub = (valueSub - valueInt) / Physics.gravity.y;

            if (float.IsNaN(valueAdd) && !float.IsNaN(valueSub))
                return valueSub;
            if (!float.IsNaN(valueAdd) && float.IsNaN(valueSub))
                return valueAdd;
            if (float.IsNaN(valueAdd) && float.IsNaN(valueSub))
                return -1.0f;
            return Mathf.Max(valueAdd, valueSub);
        }

        public static Vector3 GetFireDirection(Vector3 startPos, Vector3 endPos, float speed)
        {
            var res = Vector3.zero;
            var direction = endPos - startPos;
            var a = Vector3.Dot(Physics.gravity, Physics.gravity);
            var b = -4 * (Vector3.Dot(Physics.gravity, direction) + speed * speed);
            var c = 4 * Vector3.Dot(direction, direction);
            if (b * b - 4 * a * c < 0)
                return res;

            var time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));
            var time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));

            var time = 0.0f;

            if (time0 < 0.0f)
            {
                if(time1<0.0f)
                    return res;
                time = time1;
            }
            else
            {
                time = time1 < 0 ? time0 : Mathf.Max(time0, time1);
            }

            res = 2 * direction - Physics.gravity * time * time;
            res /= (2 * speed * speed);
            return res;
        }

        private Vector3 GetLandPos(float height = 0.0f)
        {
            _minHeight = height;
            var landingPos = Vector3.zero;
            var time = GetLandingTime();
            if (time<0.0f)
            {
                return Vector3.zero;
            }
            landingPos.y = _minHeight;
            landingPos.x = _firePos.x + _direction.x * _speed * time;
            landingPos.z = _firePos.z + _direction.z * _speed * time;
            return landingPos;
        }
    }
}