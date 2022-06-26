using System;
using UnityEngine;

namespace DefaultNamespace
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class RayMarcher : SceneViewFilter
    {
        [SerializeField] 
        private Shader _shader;

        [SerializeField] 
        private Camera _camera;

        [SerializeField] 
        private float _sensitivity = 100.0f;
        
        private Material _material;

        private float _xRotation;
        private float _yRotation;

        public Material Material
        {
            get
            {
                if (!_material && _shader)
                {
                    _material = new Material(_shader);
                    _material.hideFlags = HideFlags.HideAndDontSave;
                }

                return _material;
            }
        }

    
         private void Start()
         {
             Cursor.lockState = CursorLockMode.None;
         }
//
//         private void Update()
//         {
//             //float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
//             //float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * 2 * Time.deltaTime;
// //
//             //_xRotation -= mouseY;
//             //_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
// //
//             //_yRotation += mouseX;
//             //transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
//         }


        [ImageEffectOpaque]
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!Material)
            {
                Graphics.Blit(src, dest);
                return;
            }

            Material.SetMatrix("_FrustumCornersES", GetFrustumCorners());
            Material.SetMatrix("_CameraToWorldMatrix", _camera.cameraToWorldMatrix);
            Material.SetVector("_CameraWorldPos", _camera.transform.position);

            CustomGraphicsBlit(src, dest, Material, 0);
        }

        private Matrix4x4 GetFrustumCorners()
        {
            Matrix4x4 corners = Matrix4x4.identity;
            float tanFov = Mathf.Tan(_camera.fieldOfView / 2 * Mathf.Deg2Rad);

            Vector3 toRight = Vector3.right * tanFov * _camera.aspect;
            Vector3 toTop = Vector3.up * tanFov;
            
            Vector3 topLeft = -Vector3.forward - toRight + toTop;
            Vector3 topRight = -Vector3.forward + toRight + toTop;
            Vector3 bottomRight = -Vector3.forward + toRight - toTop;
            Vector3 bottomLeft = -Vector3.forward - toRight - toTop;

            corners.SetRow(0, topLeft);
            corners.SetRow(1, topRight);
            corners.SetRow(2, bottomRight);
            corners.SetRow(3, bottomLeft);

            return corners;
        }

        static void CustomGraphicsBlit(RenderTexture src, RenderTexture dest, Material mat, int passNr)
        {
            RenderTexture.active = dest;
            
            mat.SetTexture("_MainTex",  src);
            
            GL.PushMatrix();
            GL.LoadOrtho();

            mat.SetPass(passNr);
            
            GL.Begin(GL.QUADS);
            
            GL.MultiTexCoord2(0, 0.0f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 3.0f); // BL

            GL.MultiTexCoord2(0, 1.0f, 0.0f);
            GL.Vertex3(1.0f, 0.0f, 2.0f); // BR

            GL.MultiTexCoord2(0, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f); // TR

            GL.MultiTexCoord2(0, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 1.0f, 0.0f); // TL
    
            GL.End();
            GL.PopMatrix();
        }
    }
}