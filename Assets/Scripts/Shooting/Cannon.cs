using UnityEngine;



namespace Shooting
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Projectile projectileRed;
        [SerializeField] private Projectile projectileBlue;

        private void Awake()
        {
            projectileRed.Fall += ReLaunchRed;
            ReLaunchRed(projectileRed);

            projectileBlue.Fall += ReLaunchBlue;
            ReLaunchBlue(projectileBlue);
        }

        private void ReLaunchRed(Projectile projectile1)
        {
            var direction = new Vector3(Random.Range(-100,100), Random.Range(0,100), Random.Range(-100,100));
            projectileRed.Launch(transform.position,direction,Random.Range(6,10));
        }

        private void ReLaunchBlue(Projectile projectile1)
        {
            var endPos = new Vector3(Random.Range(-7,7), -1, Random.Range(-7,7));
            var speed = Random.Range(6, 14);
            var direction = 
                Projectile.GetFireDirection(transform.position, transform.TransformPoint(endPos), speed);
            projectileBlue.Launch(transform.position,direction,Random.Range(6,10));
        }
    }
}