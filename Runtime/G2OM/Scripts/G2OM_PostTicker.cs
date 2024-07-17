// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

namespace Tobii.G2OM
{
    using System.Collections.Generic;
    using UnityEngine;


    public class G2OM_PostTicker : IG2OM_PostTicker
    {
        private const int ExpectedNumerOfGazeFocusableComponentsPerObject = 6;

        private GameObject _previousGazeFocusedObject;
        private readonly List<IGazeFocusable> _gazeFocusableComponents = new List<IGazeFocusable>(ExpectedNumerOfGazeFocusableComponentsPerObject);

        //private UIGazeButtonGraphics btnGraphics;

        public bool focusedBoolean;
        //private Tobii.XR.UIGazeButtonGraphics _uiGazeButtonGraphics;
        //private float timeElapsed = 0f;

        //private float gazeThreshold = 1f;
        public bool btnPressed = false;

        public void TickComplete(List<FocusedCandidate> focusedObjects)
        {
            GameObject focusedObject = focusedObjects.Count == 0 ? null : focusedObjects[0].GameObject;


            //Debug.Log(focusedObjects.Count);
            UpdateFocusableComponents(focusedObject, ref _previousGazeFocusedObject, _gazeFocusableComponents);
        }

        private void UpdateFocusableComponents(GameObject focusedObject, ref GameObject previousFocusedObject, List<IGazeFocusable> gazeFocusableComponents)
        {
            //Debug.Log("g2Om tick3 prin");
            if (focusedObject == previousFocusedObject) return;
         
           
            

            //Debug.Log("g2Om tick3 meta.............................................................................................................");
            if (previousFocusedObject != null)
            {
                foreach (var focusableComponent in gazeFocusableComponents)
                {

                    //Debug.Log("mpika false");
                    focusableComponent.GazeFocusChanged(false);
                }
                gazeFocusableComponents.Clear();
            }

            if (focusedObject != null)
            {
                focusedObject.GetComponents(gazeFocusableComponents);

                foreach (var focusableComponent in gazeFocusableComponents)
                {
                    //Debug.Log("mpika true");
                    focusableComponent.GazeFocusChanged(true);
                }
            }
            
            previousFocusedObject = focusedObject;
        }
    }
}