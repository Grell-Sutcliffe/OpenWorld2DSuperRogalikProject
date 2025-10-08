using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CheckKeyInputScript : MonoBehaviour
{
    MainController mainController;
    InputAction wasd;

    // public GameObject playerArrow;

    char W = 'W';
    char A = 'A';
    char S = 'S';
    char D = 'D';

    bool w;
    bool a;
    bool s;
    bool d;

    int side = 0;

    private void OnGUI()
    {
        var e = Event.current;
        if (e == null) return;

        if (e.type == EventType.KeyDown)
        {
            Debug.Log($"Pressed: {e.keyCode}");

            if (e.keyCode == KeyCode.W) w = true;
            if (e.keyCode == KeyCode.A) a = true;
            if (e.keyCode == KeyCode.S) s = true;
            if (e.keyCode == KeyCode.D) d = true;
        }

        if (e.type == EventType.KeyUp)
        {
            Debug.Log($"Released: {e.keyCode}");

            if (e.keyCode == KeyCode.W) w = false;
            if (e.keyCode == KeyCode.A) a = false;
            if (e.keyCode == KeyCode.S) s = false;
            if (e.keyCode == KeyCode.D) d = false;
        }

        RotateArrow();
    }

    void RotateArrow()
    {
        if (w && d && !a && !s) side = 7;  // up right
        else if (w && a && !d && !s) side = 1;  // up left
        else if (s && d && !w && !a) side = 5;  // down right
        else if (s && a && !w && !d) side = 3;  // down left
        else if (s && !w) side = 4;  // down
        else if (d && !a) side = 6;  // right
        else if (a && !d) side = 2;  // left
        else if (w && !s) side = 0;  // up

        int angle = 45 * side;
        //playerArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        var rt = transform as RectTransform;
        if (rt != null) rt.localEulerAngles = new Vector3(0, 0, angle);
        else transform.rotation = Quaternion.Euler(0, 0, angle);

        /*
        
        1 0 7
        2   6
        3 4 5

        */
    }
}
