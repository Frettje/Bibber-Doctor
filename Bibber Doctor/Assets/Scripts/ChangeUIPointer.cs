﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeUIPointer : MonoBehaviour
{

	public GameObject rightController;
	public GameObject leftController;
	private VRTK_Pointer rightPointer;
	private VRTK_Pointer leftPointer;
	private VRTK_BezierPointerRenderer rightBezier;
	private VRTK_StraightPointerRenderer rightStraight;
	private VRTK_BezierPointerRenderer leftBezier;
	private VRTK_StraightPointerRenderer leftStraight;

	private void Start()
	{
		rightPointer = rightController.GetComponent<VRTK_Pointer>();
		leftPointer = leftController.GetComponent<VRTK_Pointer>();
		rightBezier = rightController.GetComponent<VRTK_BezierPointerRenderer>();
		rightStraight = rightController.GetComponent<VRTK_StraightPointerRenderer>();
		leftBezier = leftController.GetComponent<VRTK_BezierPointerRenderer>();
		leftStraight = leftController.GetComponent<VRTK_StraightPointerRenderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		// If bodycollider collides with trigger collider
		if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]")
		{
			SetPointerRenderer(false);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		// If bodycollider collides with trigger collider
		if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]")
		{
			SetPointerRenderer(true);
		}
	}

	// Set Bezier or Straight Point Renderer
	private void SetPointerRenderer(bool setBezier)
	{
		rightPointer.enabled = false;
		leftPointer.enabled = false;
		rightPointer.pointerRenderer.enabled = false;
		leftPointer.pointerRenderer.enabled = false;

		if (setBezier)
		{
			rightBezier.enabled = true;
			leftBezier.enabled = true;
			rightStraight.enabled = false;
			leftStraight.enabled = false;
			rightPointer.pointerRenderer = rightBezier;
			leftPointer.pointerRenderer = leftBezier;
		}
		else
		{
			rightBezier.enabled = false;
			leftBezier.enabled = false;
			rightStraight.enabled = true;
			leftStraight.enabled = true;
			rightPointer.pointerRenderer = rightStraight;
			leftPointer.pointerRenderer = leftStraight;
		}

		rightPointer.pointerRenderer.enabled = true;
		leftPointer.pointerRenderer.enabled = true;
		rightPointer.enabled = true;
		leftPointer.enabled = true;
	}

    void Update ()
    {
        Debug.Log(leftBezier + " " + leftStraight + " " + rightStraight + " " + rightBezier);

        if (leftBezier == null || leftStraight == null || rightBezier == null || rightStraight == null)
        {
            
        }
    }
}