using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Utils
{
	[Serializable]
	public class Probability
	{
		public static Probability Resolve(Probability[] probabilities)
		{
			Probability probability = ResolveByGrade(probabilities, ProbabilityGrade.VeryHigh);
			if(probability != null)
			{
				return probability;
			}
			probability = ResolveByGrade(probabilities, ProbabilityGrade.High);
			if(probability != null)
			{
				return probability;
			}
			probability = ResolveByGrade(probabilities, ProbabilityGrade.Medium);
			if(probability != null)
			{
				return probability;
			}
			probability = ResolveByGrade(probabilities, ProbabilityGrade.Low);
			if(probability != null)
			{
				return probability;
			}
			probability = ResolveByGrade(probabilities, ProbabilityGrade.VeryLow);
			if(probability != null)
			{
				return probability;
			}
			return null;
		}

		public static Probability ResolveByGrade(Probability[] probabilities, ProbabilityGrade grade)
		{
			Probability[] gradeProbabilities = ByGrade(probabilities, grade);
			if(gradeProbabilities.Length > 0)
			{
				if(UnityEngine.Random.Range(0f, 10f) < (float)grade)
				{
					return gradeProbabilities[UnityEngine.Random.Range(0, gradeProbabilities.Length)];
				}
			}
			return null;
		}

		public static Probability[] ByGrade(Probability[] probabilities, ProbabilityGrade grade)
		{
			return probabilities.Where((Probability probability) => probability.grade == grade).ToArray();
		}
		public ProbabilityGrade grade;
	}

	public enum ProbabilityGrade
	{
		VeryLow = 1,
		Low = 2,
		Medium = 3,
		High = 5,
		VeryHigh = 7
	}
	public enum KnownTags
	{
		Player,
		Enemies,
	}
	public enum PartAnimationParameters
	{
		IsMoving,
		PointingX,
		PointingY,
		MovingSpeed,
		Attack,
		AttackingSpeed
	}
	public enum EffectAnimationParameters
	{
		Speed
	}
	public static class KnownGameObjects
	{
		public static readonly string MainCamera = "Main Camera";
	}
	public static class Scenes
	{
		public static readonly int MainMenu = 0;
		public static readonly int TestLevel = 1;
		public static readonly int Level1 = 2;
		public static void OpenScene(int sceneIndex)
		{
			SceneManager.LoadScene(sceneIndex);
		}
		public static void ReloadScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
