using System; 
using UnityEngine;

public class SpotSelector : Selector {
    public Spot[] positions;

    public void ChangeSpot(int id)
    {
        try
        {
            for(int i = 0; i < positions.Length; i++) {
                if(positions[i].id.Equals(id)) {
                    gaze.player.transform.position = positions[i].transform.position;
                    break;
                }
            }
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
