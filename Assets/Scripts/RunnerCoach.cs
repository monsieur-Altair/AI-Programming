using System.Collections.Generic;
using UnityEngine;


public class RunnerCoach:MonoBehaviour
{
    private struct Achievement
    {
        public Behaviours.Runner Runner;
        public int CurrentPointIndex;

        public Achievement(Behaviours.Runner runner, int index)
        {
            this.Runner = runner;
            this.CurrentPointIndex = index;
        }
    }
    
    [SerializeField]private List<Behaviours.Runner> _runners;
    private Dictionary<int, Achievement> _allAchievements;
    [SerializeField] private Trip _trip;
    
    void Awake()
    {
        _trip = GetComponent<Trip>();
        _allAchievements = new Dictionary<int, Achievement>();
        foreach (var runner in _runners)
        {
            Debug.Log(runner!=null);
            Debug.Log(runner.Index);
            runner.Approaching += SetNewPoint;
            _allAchievements.Add(runner.Index,new Achievement(runner,-1));
            runner.SetTarget(_trip.GetStartPoint());
        }
    }

    private void SetNewPoint(int runnerIndex)
    {
        var achievement=_allAchievements[runnerIndex];
        var newPoint = _trip.GetNewPoint(achievement.CurrentPointIndex);
        achievement.Runner.SetTarget(newPoint);
        var newPointIndex= ++achievement.CurrentPointIndex;
        if (newPointIndex > _trip.Count()-1)
            achievement.CurrentPointIndex = 0;
        _allAchievements[runnerIndex] = achievement;
    }
}