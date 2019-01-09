using System; 
using UnityEngine;

public class SpotSelector : Selector {
    public Transform[] positions;
    public void ChangeSpot(int id)
    {
        try
        {
            gaze.player.transform.position = positions[id].position;
        }
        catch(Exception e)
        {
            Debug.Log(String.Format(
                "Spot with such id not found({0})", id));
        }
    }
    public override void OnClickItem(int id)
    {
        ChangeSpot(id);
    }
}
