using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
public class TrackableList : MonoBehaviour {

	public bool isTracking;
	void Update () {
		// Get the StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();
		
		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();
			
		// Iterate through the list of active trackables
	
		int numFrameMarkers = 0;
		int numImageTargets = 0;
		int numMultiTargets = 0;
		foreach (TrackableBehaviour tb in activeTrackables) {

			
			if (tb is MarkerBehaviour)
				numFrameMarkers++;
			else if (tb is ImageTargetBehaviour)
				numImageTargets++;
			else if (tb is MultiTargetBehaviour)
				numMultiTargets++;
		}
		
		if (numImageTargets > 0) {
			isTracking = true;
		} else {
			isTracking = false;
		}
	}
}
