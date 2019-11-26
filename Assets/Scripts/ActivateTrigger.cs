using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityStandardAssets.Utility
{
    public class ActivateTrigger : MonoBehaviour
    {
        // A multi-purpose script which causes an action to occur when
        // a trigger collider is entered.
        public enum Mode
        {
            Trigger = 0,    // Just broadcast the action on to the target
            Replace = 1,    // replace target with source
            Activate = 2,   // Activate the target GameObject
            Enable = 3,     // Enable a component
            Animate = 4,    // Start animation on target
            Deactivate = 5  // Decativate target GameObject
        }

        public Mode action = Mode.Activate;         // The action to accomplish
        public Object target;                       // The game object to affect. If none, the trigger work on this game object
        public GameObject source;                   //  The source game object that we will be replacing
        public int triggerCount = 1;
        public bool repeatTrigger = false;

        private Object currentSource;
        private Object currentTarget;

        private void DoActivateTrigger()
        {
            triggerCount--;

            if (triggerCount == 0 || repeatTrigger)
            {
                currentTarget = target ?? gameObject;
                Behaviour targetBehaviour = currentTarget as Behaviour;
                GameObject targetGameObject = currentTarget as GameObject;
                if (targetBehaviour != null)
                {
                    targetGameObject = targetBehaviour.gameObject;
                }

                switch (action)
                {
                    case Mode.Trigger:
                        if (targetGameObject != null)
                        {
                            targetGameObject.BroadcastMessage("DoActivateTrigger");
                        }
                        break;
                    case Mode.Replace:
                        if (source != null)
                        {
                            if (targetGameObject != null)
                            {
                                currentSource = Instantiate(source, targetGameObject.transform.position,
                                            targetGameObject.transform.rotation);
                                DestroyObject(targetGameObject);
                            }
                        }
                        break;
                    case Mode.Activate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(true);
                        }
                        break;
                    case Mode.Enable:
                        if (targetBehaviour != null)
                        {
                            targetBehaviour.enabled = true;
                        }
                        break;
                    case Mode.Animate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.GetComponent<Animation>().Play();
                        }
                        break;
                    case Mode.Deactivate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(false);
                        }
                        break;
                }
            }
            triggerCount++;
        }

        /// <summary>
        /// Personal code, that implements a way to deactivate the trigger effects.
        /// </summary>
        void DoDeactivateTrigger()
        {
            triggerCount--;

            //So that we dont activate the trigger more than once
            if (triggerCount == 0 || repeatTrigger)
            {
                Object currentTarget = target ?? gameObject;
                Behaviour targetBehaviour = currentTarget as Behaviour;
                GameObject targetGameObject = currentTarget as GameObject;
                GameObject sourceGameObject = currentSource as GameObject;
                if (targetBehaviour != null)
                {
                    targetGameObject = targetBehaviour.gameObject;
                }

                switch (action)
                {
                    
                    case Mode.Replace:
                        if (currentSource != null)
                        {
                            if (targetGameObject != null)
                            {
                                Instantiate(target, targetGameObject.transform.position,
                                            targetGameObject.transform.rotation);
                                DestroyObject(sourceGameObject);
                            }
                        }
                        break;
                    case Mode.Activate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(false);
                        }
                        break;
                    case Mode.Enable:
                        if (targetBehaviour != null)
                        {
                            targetBehaviour.enabled = false;
                        }
                        break;
                    case Mode.Animate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.GetComponent<Animation>().Stop();
                        }
                        break;
                    case Mode.Deactivate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(true);
                        }
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DoActivateTrigger();
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            DoDeactivateTrigger();
        }
    }
}
