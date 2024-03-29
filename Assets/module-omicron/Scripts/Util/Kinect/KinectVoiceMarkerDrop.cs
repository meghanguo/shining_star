﻿/**************************************************************************************************
* THE OMICRON PROJECT
 *-------------------------------------------------------------------------------------------------
 * Copyright 2010-2018		Electronic Visualization Laboratory, University of Illinois at Chicago
 * Authors:										
 *  Arthur Nishimoto		anishimoto42@gmail.com
 *-------------------------------------------------------------------------------------------------
 * Copyright (c) 2010-2018, Electronic Visualization Laboratory, University of Illinois at Chicago
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted 
 * provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of conditions 
 * and the following disclaimer. Redistributions in binary form must reproduce the above copyright 
 * notice, this list of conditions and the following disclaimer in the documentation and/or other 
 * materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE  GOODS OR SERVICES; LOSS OF 
 * USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
 * ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *************************************************************************************************/
 
using UnityEngine;
using System.Collections;

public class KinectVoiceMarkerDrop : MonoBehaviour {

	public GameObject flagPrefab;
	public GameObject flagPrefab2;

	public Transform head;

	public Vector3[] previousMarkersType1;
	public Vector3[] previousMarkersType2;

	// Use this for initialization
	void Start () {
		previousMarkersType2 = new Vector3[11];
		previousMarkersType2[0] = new Vector3 (-1.0791f, 0, 0.6950537f);
		previousMarkersType2[1] = new Vector3(1.325743f, 0, 0.3330491f);
		previousMarkersType2[2] = new Vector3(0.8699049f, 0, 0.8649473f);
		previousMarkersType2[3] = new Vector3(0.2740416f, 0, 1.263374f);
		previousMarkersType2[4] = new Vector3(-0.5579219f, 0, 1.326921f);
		previousMarkersType2[5] = new Vector3(-0.3503017f, 0, 1.323008f);
		previousMarkersType2[6] = new Vector3(-0.7328109f, 0, 1.173285f);
		previousMarkersType2[7] = new Vector3(-1.467721f, 0, 0.02693012f);
		previousMarkersType2[8] = new Vector3(-1.758688f, 0, -0.8584192f);
		previousMarkersType2[9] = new Vector3(1.814916f, 0, -0.5128982f);
		previousMarkersType2[10] = new Vector3(0.02821394f, 0, -0.7697589f);

		previousMarkersType1 = new Vector3[9];
		previousMarkersType1[0] = new Vector3 (1.762259f,0, -0.1401564f);
		previousMarkersType1[1] = new Vector3(1.544016f,0,  0.4700503f);
		previousMarkersType1[2] = new Vector3(1.328239f,0,  0.5562797f);
		previousMarkersType1[3] = new Vector3(0.4825714f,0,  1.78759f);
		previousMarkersType1[4] = new Vector3(-0.2173307f,0,  1.836473f);
		previousMarkersType1[5] = new Vector3(-0.6020163f,0,  1.758563f);
		previousMarkersType1[6] = new Vector3(-1.030896f,0,  0.7435958f);
		previousMarkersType1[7] = new Vector3(-1.196984f,0,  -0.2257504f);
		previousMarkersType1[8] = new Vector3(0.147489f,0,  -0.2503585f);

		foreach( Vector3 pos in previousMarkersType1 )
		{
			Instantiate( flagPrefab, pos, Quaternion.identity );
		}
		foreach( Vector3 pos in previousMarkersType2 )
		{
			Instantiate( flagPrefab2, pos, Quaternion.identity );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnVoiceCommand( string speech )
	{

		if( speech.Equals("SET MARKER") )
		{
			Vector3 pos = new Vector3( head.position.x, 0, head.position.z );
			Instantiate( flagPrefab, pos, Quaternion.identity );
			Debug.Log (pos);
		}
	}
}
