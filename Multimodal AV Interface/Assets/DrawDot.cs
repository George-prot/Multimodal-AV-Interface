using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Tobii.XR;

namespace Tobii.XR {

    public class DrawDot : MonoBehaviour
    {
        public GameObject eyeTrackingPoint;
        //public G2OM ena;
        public GameObject UI;
        public GameObject dotPrefab;
        public Ray ray;

        // Start is called before the first frame update
        void Start()
        {
            eyeTrackingPoint = Instantiate(dotPrefab, UI.transform, worldPositionStays: true);
        }

        // Update is called once per frame
        void Update()
        {
            //eyeTrackingPoint = Instantiate(recentCallPrefab, callPosition1.transform, worldPositionStays: false);
            Vector3 dir = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).GazeRay.Direction + UI.transform.position;
            dir.z -= 0.3f;
            if (dir.x > 0.25) dir.x = (dir.x * 0.6f);
            //dir.x -= 0.1f;
            //dir.y -= 0.03f;

            //TobiiXR.GetEyeTrackingData(TobiiXR_EyeTrackingData).

            ray.origin = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).GazeRay.Origin;
            ray.direction = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).GazeRay.Direction;

            //OnDrawGizmos();


            //eyeTrackingPoint.transform.SetLocalPositionAndRotation(dir, eyeTrackingPoint.transform.rotation);
            eyeTrackingPoint.transform.SetPositionAndRotation(dir, UI.transform.rotation);

            //Debug.Log("tobiiii: "+dir);

            /* OVRGazePointer gazePointer = ena.GetDeviceData().gaze_ray_world_space
             TobiiXR_TrackingSpace trackingSpace;
             TobiiXR_EyeTrackingData ray = Tobii.XR.TobiiXR.GetEyeTrackingData(trackingSpace);*/
        }

        /*public void OnDrawGizmos()
        {
            Gizmos.DrawRay(ray);
        }*/
    }

}
