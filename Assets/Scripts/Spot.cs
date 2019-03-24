using UnityEngine;

public class Spot : Item {

    public void OnAnim(string anim) {
        GetComponent<Animation>().Play(anim);
    }
}
