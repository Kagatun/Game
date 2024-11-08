using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIntermediary : Wave
{
    [SerializeField] private List<VisualPoint> _visualPoints;

    public void ActiveVisualPoint()
    {
        if (_visualPoints != null)
        {
            foreach (var visualPoint in _visualPoints)
                if (visualPoint.IsActive == false)
                    visualPoint.StartAnimationVisualPoint();
        }
    }

    public void MakeStatusStartTrue()
    {
        if (_visualPoints != null)
            foreach (var visualPoint in _visualPoints)
                visualPoint.MakeActiveStatus();
    }
}
