using UnityEngine;
using System.Collections;

using DaikonForge.Tween;

#if !FREE_VERSION

public class TestSplineTween : MonoBehaviour
{

	public SplineObject Spline;

	public float Duration = 5f;
	public EasingType Easing = EasingType.Linear;
	public TweenLoopType Loop = TweenLoopType.Loop;

	void Start()
	{

		this.TweenPath( Spline.Spline )
			.SetDuration( Duration )
			.SetEasing( TweenEasingFunctions.GetFunction( Easing ) )
			.SetLoopType( Loop )
			.SetTimeScaleIndependent( true )
			.Play();

	}

}

#endif
