using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using AI.Fuzzy.Library;

public class FuzzyController : MonoBehaviour {

	MamdaniFuzzySystem fuzSys = null;

	Dictionary<FuzzyVariable, double> inputValues;
	Dictionary<FuzzyVariable, double> outputValues;

	double res = 0.0;
	double inp = 0.0;

	// Use this for initialization
	void Start () {
		fuzSys = new MamdaniFuzzySystem ();

		//Input variables

		FuzzyVariable fvDistance = new FuzzyVariable ("distance", -1.0, 1.0);
		fvDistance.Terms.Add (
			new FuzzyTerm("right", new TriangularMembershipFunction(0.0, 1.0, 1.0)));
		fvDistance.Terms.Add (
			new FuzzyTerm("center", new TriangularMembershipFunction(-0.5, 0.0, 0.5)));
		fvDistance.Terms.Add (
			new FuzzyTerm("left", new TriangularMembershipFunction(-1.0, -1.0, -0.0)));
		fuzSys.Input.Add (fvDistance);

		//FuzzyVariable fvRateOfChange = new FuzzyVariable ("rateOfChange", 0.0, 10.0);

		//Output variables
		FuzzyVariable fvSteering = new FuzzyVariable ("steering", -1.0, 1.0);
		fvSteering.Terms.Add (
			new FuzzyTerm ("soft_right", new TriangularMembershipFunction (-1.0, -1.0, 0.0)));
		fvSteering.Terms.Add (
			new FuzzyTerm ("straight", new TriangularMembershipFunction (-0.3, -0.0, 0.3)));
		fvSteering.Terms.Add (
			new FuzzyTerm ("soft_left", new TriangularMembershipFunction (-0.0, 1.0, 1.0)));
		fuzSys.Output.Add (fvSteering);

		//Create rules
		try{
			MamdaniFuzzyRule rule1 = fuzSys.ParseRule ("if (distance is right) then steering is soft_left");
			MamdaniFuzzyRule rule2 = fuzSys.ParseRule ("if (distance is left) then steering is soft_right");
			MamdaniFuzzyRule rule3 = fuzSys.ParseRule ("if (distance is center) then steering is straight");

			fuzSys.Rules.Add(rule1);
			fuzSys.Rules.Add(rule2);
			fuzSys.Rules.Add (rule3);
		} catch(Exception e){
			Debug.LogException(e);
 		}
	}
	
	// Update is called once per frame
	void Update () {

		FuzzyVariable fvDistance = fuzSys.InputByName ("distance");
		FuzzyVariable fvSteering = fuzSys.OutputByName ("steering");
		inputValues = new Dictionary<FuzzyVariable, double> ();
		outputValues = new Dictionary<FuzzyVariable, double> ();

		RoadLine roadLine = GameObject.Find ("Roadline").GetComponent<RoadLine>();
		inputValues.Add (fvDistance, roadLine.getNormalisedDistance());

		inp = inputValues [fvDistance];

		Dictionary<FuzzyVariable, double> result = fuzSys.Calculate (inputValues);

		res = result [fvSteering];


		if (!double.IsNaN (res)) {
			gameObject.transform.rotation = Quaternion.Euler ( 0.0f, 0.0f, (float) res * 25f );
			gameObject.transform.localPosition = new Vector3(transform.localPosition.x + (-(float)res * Time.deltaTime * 25f),
			                                                 transform.localPosition.y, 
			                                                 transform.localPosition.z);
		}


	}
}
