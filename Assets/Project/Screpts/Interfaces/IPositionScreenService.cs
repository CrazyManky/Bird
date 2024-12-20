using UnityEngine;

namespace Project.Screpts.Interfaces
{
    public interface IPositionScreenService : IService
    {
        public Vector3 GetScreenPosition(Vector2 screenAnchor);

        public Quaternion GetRotationTowardsCenter(Vector3 objectPosition);
    }
}