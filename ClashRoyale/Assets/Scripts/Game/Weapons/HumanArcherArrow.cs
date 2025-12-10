// -- Human Archer Animations 2.0 | Kevin Iglesias --
// This script is a secondary script that works with HumanArcherController.cs script.
// It animates the bow when entering or exiting an AnimatorController state.
// You can freely edit, expand, and repurpose it as needed. To preserve your custom changes when updating
// to future versions, it is recommended to work from a duplicate of this script.

// Contact Support: support@keviniglesias.com

using UnityEngine;

namespace KevinIglesias
{
    public class HumanArcherArrow : MonoBehaviour
    {
        private float arrowSpeed = 30f;
       

        private Transform targetTransform;
        private float yPosition;
        public float ArrowSpeed
        {
            set
            {
                arrowSpeed = value;
            }
            get
            {
                return arrowSpeed;
            }
        }
        private float arrowLifetime = 2f;
        private Health healthTarget;
        public Health HealthTarget;
        private Vector3 targetPosition;

        private int dmg;
        public int DMG
        {
            set
            {
                dmg = value;
            }
            get
            {
                return dmg;
            }
        }
        
        void OnEnable()
        {   
            Destroy(this.gameObject, arrowLifetime);   
        }

        private void Start()
        {
            targetTransform = HealthTarget.transform;
            yPosition = transform.position.y;
        }
        private void Update()
        {
            targetPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + yPosition, targetTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ArrowSpeed * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                if (HealthTarget != null)
                {
                    HealthTarget.ApplyDamage(DMG);
                }
                Destroy(this.gameObject);
            }
           
    
        }
    }
}
