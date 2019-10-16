using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class OrthogtaphicCameraBoundsConstraint
    {
        private OrthogtaphicCameraBounds _bounds = null;
        private Camera _camera = null;

        public OrthogtaphicCameraBoundsConstraint(OrthogtaphicCameraBounds bounds, Camera camera)
        {
            _bounds = bounds;
            _camera = camera;
        }
    
        public void ForceBoundaries()
        {
            float x = _camera.orthographicSize * _camera.aspect;
            float y = _camera.orthographicSize;

            Vector3 position = _camera.gameObject.transform.position;

            position.x = MinCheck(position.x, x, _bounds.MinWidth);
            position.x = MaxCheck(position.x, x, _bounds.MaxWidth);
            position.y = MinCheck(position.y, y, _bounds.MinHeight);
            position.y = MaxCheck(position.y, y, _bounds.MaxHeight);

            _camera.gameObject.transform.position = position;
        }

        private float MinCheck(float x, float x1, float xMin)
        {
            if (x - x1 < xMin)
                return xMin + x1;
            else
                return x;
        }

        private float MaxCheck(float x, float x1, float xMax)
        {
            if (x + x1 > xMax)
                return xMax - x1;
            else
                return x;
        }
    }
}
